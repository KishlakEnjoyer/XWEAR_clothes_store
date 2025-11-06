<script setup>
import GoodCardElement from "../components/GoodCardElement.vue";
import DropdowncategoryElement from "@/components/DropdowncategoryElement.vue";
import { RouterLink } from "vue-router";
import { ref, onMounted, computed } from 'vue';
import axios from 'axios';

// --- Состояния ---
const allShoes = ref([]); // Хранит все товары "Обувь" после загрузки
const loading = ref(true);
const error = ref(null);
const searchQuery = ref(''); // Для поиска
const selectedSubcategory = ref(''); // Для фильтрации по подкатегории
const sortOption = ref('default'); // Для сортировки

// --- Вычисляемые свойства для фильтрации/поиска/сортировки ---
const filteredAndSearchedGoods = computed(() => {
  let result = allShoes.value;

  // 1. Поиск по названию бренда или модели (если ввод есть)
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase();
    result = result.filter(item => {
      // Используем строчный регистр, как в API: brand.name
      const brandName = item.brand?.name?.toLowerCase() || '';
      const modelName = item.model?.name?.toLowerCase() || '';
      return brandName.includes(query) || modelName.includes(query);
    });
  }

  // 2. Фильтрация по подкатегории (псевдо-фильтрация)
  // Так как API не возвращает подкатегорию, мы можем эмулировать фильтрацию
  // вручную, добавив подкатегорию в список подкатегорий в DropdowncategoryElement
  // и сопоставляя её с товаром по какому-то признаку (например, modelName).
  // ПОКА: используем выбранный элемент как ключевое слово для фильтрации по модели.
  if (selectedSubcategory.value) {
    const subcategory = selectedSubcategory.value.toLowerCase();
    result = result.filter(item => {
      // Используем строчный регистр, как в API: model.name
      const modelName = item.model?.name?.toLowerCase() || '';
      // Простое сопоставление: если имя модели содержит слово подкатегории
      // В реальности, это должно быть связано с отдельным полем подкатегории в API
      return modelName.includes(subcategory);
    });
  }

  return result;
});

const sortedGoods = computed(() => {
  let result = [...filteredAndSearchedGoods.value]; // Создаем копию для сортировки

  // 3. Сортировка
  switch (sortOption.value) {
    case 'price_asc':
      result.sort((a, b) => {
        // Используем строчный регистр, как в API: goodSizes.price
        const priceA = a.goodSizes && a.goodSizes.length > 0 ? Math.min(...a.goodSizes.map(s => s.price)) : Infinity;
        const priceB = b.goodSizes && b.goodSizes.length > 0 ? Math.min(...b.goodSizes.map(s => s.price)) : Infinity;
        return priceA - priceB;
      });
      break;
    case 'price_desc':
      result.sort((a, b) => {
        // Используем строчный регистр, как в API: goodSizes.price
        const priceA = a.goodSizes && a.goodSizes.length > 0 ? Math.min(...a.goodSizes.map(s => s.price)) : -Infinity;
        const priceB = b.goodSizes && b.goodSizes.length > 0 ? Math.min(...b.goodSizes.map(s => s.price)) : -Infinity;
        return priceB - priceA;
      });
      break;
    case 'name_asc':
      result.sort((a, b) => {
        // Используем строчный регистр, как в API: brand.name, model.name
        const nameA = (a.brand?.name || '') + ' ' + (a.model?.name || '');
        const nameB = (b.brand?.name || '') + ' ' + (b.model?.name || '');
        return nameA.localeCompare(nameB);
      });
      break;
    case 'name_desc':
      result.sort((a, b) => {
        // Используем строчный регистр, как в API: brand.name, model.name
        const nameA = (a.brand?.name || '') + ' ' + (a.model?.name || '');
        const nameB = (b.brand?.name || '') + ' ' + (b.model?.name || '');
        return nameB.localeCompare(nameA);
      });
      break;
    case 'default':
    default:
      // article (с маленькой) - правильно
      result.sort((a, b) => (a.article || '').localeCompare(b.article || ''));
      break;
  }

  return result;
});

const totalShoesCount = computed(() => sortedGoods.value.length); 

async function fetchShoes() {
  loading.value = true;
  error.value = null;
  allShoes.value = [];

  try {
    const response = await axios.get('http://localhost:5289/api/Good/GetAllGoods');
    const allGoods = response.data;
    // Фильтруем *все* товары по основной категории "Обувь", используя строчный регистр, как в API: category.name
    allShoes.value = allGoods.filter(item => item.category?.name?.toLowerCase().includes('обувь'));
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
      <RouterLink to="/shoes" class="navlink">Обувь</RouterLink>
    </div>
    <div class="shoes-view-catalog">
      <div class="filters-sorts">
        <DropdowncategoryElement main-category="Обувь" @subcategory-selected="selectedSubcategory = $event" />
      </div>
      <div class="shoes-goods">
        <div class="shoes-upside">
          <div class="shoes-title-count">
            <h2>ОБУВЬ</h2>
            <p v-if="!loading">{{ totalShoesCount }} товаров</p>
            <p v-else>Загрузка...</p>
          </div>
          <div class="shoes-search-sort">
            <!-- Поле поиска -->
            <input
              v-model="searchQuery"
              type="text"
              placeholder="Поиск по названию..."
              class="search-input"
            />
            <!-- Селект сортировки -->
            <p class="sort_par">
              Сортировать по
              <select v-model="sortOption" class="sort_select">
                <option value="default">По умолчанию</option>
                <option value="price_asc">От дешёвых к дорогим</option>
                <option value="price_desc">От дорогих к дешёвым</option>
                <option value="name_asc">По названию (от А до Я)</option>
                <option value="name_desc">По названию (от Я до А)</option>
              </select>
            </p>
          </div>
        </div>
        <div v-if="loading" class="loading">Загрузка товаров обуви...</div>
        <div v-else-if="error" class="error">Ошибка: {{ error }}</div>
        <div v-else class="shoes-mainpart">
          <GoodCardElement
            v-for="good in sortedGoods"
            :key="good.article"
            :article="good.article"
          />
          <div v-if="sortedGoods.length === 0" class="no-goods-message">
            <p>Нет товаров, соответствующих критериям.</p>
          </div>
        </div>
        <!-- Пагинация (пока пусто) -->
        <div class="paginaion">
          <!-- Здесь будет компонент пагинации -->
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
  flex-wrap: wrap; /* На случай, если элементы не помещаются в строку */
  gap: 10px; /* Отступ между элементами */
}

.shoes-search-sort {
  display: flex;
  align-items: center;
  gap: 10px;
}

.search-input {
  padding: 8px;
  border-radius: 5px;
  background: #333; /* Темный фон для поля ввода */
  color: white;
  border: 1px solid #555;
  font-family: inherit; /* Наследуем шрифт */
  font-weight: 300;
}

.search-input::placeholder {
  color: #aaa; /* Цвет плейсхолдера */
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
  background: #333; /* Темный фон для селекта */
  color: white;
  border: 1px solid #555;
  border-radius: 5px;
  padding: 5px;
}

.sort_par {
  font-family: RF_Dewi;
  font-weight: 300; 
  display: flex; 
  align-items: center; 
  gap: 10px;
  margin: 0; /* Убираем стандартный отступ */
}

.sort_par option {
  background-color: #121212;
  color: white;
}

.sort_par option:last-child {
  border-radius: 0px 0px 10px 10px;
}

.loading, .error, .no-goods-message {
  text-align: center;
  padding: 20px 0;
  width: 100%;
  grid-column: 1 / -1; 
}
</style>