<script setup>
import GoodCardElement from "../components/GoodCardElement.vue";
import DropdowncategoryElement from "@/components/DropdowncategoryElement.vue";
import { RouterLink } from "vue-router";


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
        <DropdowncategoryElement />
      </div>
      <div class="shoes-goods">
        <div class="shoes-upside">
          <div class="shoes-title-count">
            <h2>ОБУВЬ</h2>
            <p>0 товаров</p>
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
        <div class="shoes-mainpart">
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
          <GoodCardElement />
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
</style>
