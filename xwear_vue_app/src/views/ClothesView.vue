<script setup>
import GoodCardElement from "../components/GoodCardElement.vue";
import DropdowncategoryElement from "@/components/DropdowncategoryElement.vue";
import { RouterLink } from "vue-router";
import { ref, onMounted } from 'vue';
import axios from 'axios';

const shoesGoods = ref([]); 
const loading = ref(true);
const error = ref(null);
const totalShoesCount = ref(0);

async function fetchShoes() {
  loading.value = true;
  error.value = null;
  shoesGoods.value = [];

  try {
   
    const response = await axios.get('http://localhost:5289/api/Good/GetAllGoods');
    const allGoods = response.data;
    shoesGoods.value = allGoods.filter(item => item.category?.name?.toLowerCase().includes('одежда'));
    totalShoesCount.value = shoesGoods.value.length;

  } catch (err) {
    console.error("Ошибка при получении товаров обуви:", err);
    if (err.response) {
      error.value = `Ошибка API: ${err.response.status} ${err.response.statusText}`;
    } else if (err.request) {
      error.value = 'Не удалось получить ответ от сервера.';
    } else {
      error.value = err.message || 'Произошла ошибка при загрузке товаров.';
    }
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchShoes();
});
</script>

<template>
  <div class="shoes-view-container">
    <div class="navigation-links">
      <RouterLink to="/" class="navlink">Главная</RouterLink>
      <span> / </span>
      <RouterLink to="/shoes" class="navlink">Одежда</RouterLink>
    </div>
    <div class="shoes-view-catalog">
      <div class="filters-sorts">
        <DropdowncategoryElement />
      </div>
      <div class="shoes-goods">
        <div class="shoes-upside">
          <div class="shoes-title-count">
            <h2>ОДЕЖДА</h2>
            <p v-if="!loading">{{ totalShoesCount }} товаров</p>
            <p v-else>Загрузка...</p>
          </div>
          <div class="shoes-sort">
            <p class="sort_par">
              Сортировать по
              <select class="sort_select">
                <option>От дешёвых к дорогим</option>
                <option>От дорогих к дешёвым</option>
                <option>По названию (от А до Я)</option>
                <option>По названию (от Я до А)</option>
              </select>
            </p>
          </div>
        </div>
        <div v-if="loading" class="loading">Загрузка товаров обуви...</div>
        <div v-else-if="error" class="error">Ошибка: {{ error }}</div>
        <div v-else class="shoes-mainpart">
          <GoodCardElement
            v-for="good in shoesGoods"
            :key="good.article"
            :article="good.article"
          />
          <div v-if="shoesGoods.length === 0" class="no-goods-message">
            <p>Нет товаров в категории "Обувь".</p>
          </div>
        </div>
        <div class="paginaion">
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
* {
  color: white;
}

.shoes-view-container {
  margin: 15px auto;
  width: 85%;
}

.navigation-links {
  display: flex;
  padding: 10px;
}

.navigation-links .navlink {
  margin: 0px 5px;
  transition: all 0.2s ease;
  &:hover {
    color: #72cdfa;
    transition: all 0.2s ease;
  }
}

.shoes-goods {
  width: 100%;
}

.shoes-view-catalog {
  display: flex;
  margin-top: 20px;
  gap: 30px;
}

.filters-sorts {
  border-radius: 10px;
}

.shoes-upside {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.shoes-mainpart {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 16px;
  row-gap: 25px;
  padding: 16px 0;
}

.shoes-mainpart > * {
  justify-self: center;
}

.sort_select {
  font-family: RF_Dewi;
  font-weight: 300;
  background: none;
  border-radius: 5px;
  padding: 5px;
}

.sort_par {
  font-family: RF_Dewi;
  font-weight: 300; 
  display: flex; 
  align-items: center; 
  gap: 10px;
}

.sort_par option {
  background-color: #121212;
}

.sort_par option:last-child {
  border-radius: 0px 0px 10px 10px;
}

/* Дополнительные стили для загрузки и ошибки */
.loading, .error, .no-goods-message {
  text-align: center;
  padding: 20px 0;
  width: 100%;
  grid-column: 1 / -1; /* Растягиваем на всю ширину сетки */
}
</style>