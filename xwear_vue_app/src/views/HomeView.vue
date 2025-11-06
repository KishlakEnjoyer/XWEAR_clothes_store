<script setup>
import SliderElement from '../components/SliderElement.vue';
import GoodCardElement from '../components/GoodCardElement.vue';
import FooterElement from '@/components/FooterElement.vue';
import { Swiper, SwiperSlide } from 'swiper/vue';
import 'swiper/css';
import { ref, onMounted } from 'vue';
import axios from 'axios'; // Импортируем axios

// Состояния для данных и загрузки
const allGoods = ref([]); // Хранит все товары
const shoes = ref([]); // Товары категории "Обувь"
const clothes = ref([]); // Товары категории "Одежда"
const accessories = ref([]); // Товары категории "Аксессуары"
const loading = ref(true); // Индикатор загрузки
const error = ref(null); // Сообщение об ошибке

// Функция для получения данных с API
async function fetchGoods() {
  loading.value = true;
  error.value = null;

  try {
    // Используем axios для GET-запроса
    const response = await axios.get('http://localhost:5289/api/Good/GetAllGoods');

    // axios автоматически парсит JSON, данные находятся в response.data
    allGoods.value = response.data;

    // Фильтрация товаров по категориям
    // Теперь обращаемся к item.category.name
    shoes.value = allGoods.value.filter(item => item.category?.name?.toLowerCase().includes('обувь'));
    clothes.value = allGoods.value.filter(item => item.category?.name?.toLowerCase().includes('одежда'));
    accessories.value = allGoods.value.filter(item => item.category?.name?.toLowerCase().includes('аксессуар'));
  } catch (err) {
    console.error("Ошибка при получении товаров:", err);
    // Обработка ошибки axios
    if (err.response) {
      // Сервер ответил с кодом ошибки (4xx, 5xx)
      error.value = `Ошибка API: ${err.response.status} ${err.response.statusText}`;
    } else if (err.request) {
      // Запрос был отправлен, но не получен ответ (например, проблема с сетью)
      error.value = 'Не удалось получить ответ от сервера.';
    } else {
      // Ошибка при настройке запроса
      error.value = err.message || 'Произошла ошибка при загрузке товаров.';
    }
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  fetchGoods();
});
</script>

<template>
  <SliderElement />

  <!-- Индикатор загрузки -->
  <div v-if="loading" class="loading">Загрузка товаров...</div>
  <!-- Сообщение об ошибке -->
  <div v-else-if="error" class="error">Ошибка: {{ error }}</div>

  <!-- Обувь -->
  <div v-else class="goods-container">
    <div class="title-goods">
      <h2>ОБУВЬ</h2>
      <a href="#">
        БОЛЬШЕ ТОВАРОВ
        <svg width="6" height="11" viewBox="0 0 6 11" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fill-rule="evenodd" clip-rule="evenodd"
            d="M5.36529 6.06549C5.67771 5.75307 5.67771 5.24654 5.36529 4.93412L1.36529 0.934119C1.05288 0.6217 0.546343 0.6217 0.233924 0.934119C-0.0784959 1.24654 -0.0784959 1.75307 0.233924 2.06549L3.66824 5.4998L0.233924 8.93412C-0.0784955 9.24654 -0.0784955 9.75307 0.233924 10.0655C0.546343 10.3779 1.05288 10.3779 1.36529 10.0655L5.36529 6.06549Z"
            fill="#fff" />
        </svg>
      </a>
    </div>
    <Swiper
      :slidesPerView="5"
      :spaceBetween="10"
      :breakpoints="{
        640: { slidesPerView: 2 },
        768: { slidesPerView: 3 },
        1024: { slidesPerView: 4 },
      }"
      class="swiper-container"
    >
      <!-- Рендерим слайды только если есть товары -->
      <SwiperSlide v-for="good in shoes" :key="good.article"> <!-- Обновлено: good.article -->
        <GoodCardElement :article="good.article" /> <!-- Обновлено: good.article -->
      </SwiperSlide>
      <!-- Если товаров нет -->
      <SwiperSlide v-if="shoes.length === 0">
        <p>Нет товаров в этой категории.</p>
      </SwiperSlide>
    </Swiper>
  </div>

  <!-- Одежда -->
  <div class="goods-container">
    <div class="title-goods">
      <h2>ОДЕЖДА</h2>
      <a href="#">
        БОЛЬШЕ ТОВАРОВ
        <svg width="6" height="11" viewBox="0 0 6 11" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fill-rule="evenodd" clip-rule="evenodd"
            d="M5.36529 6.06549C5.67771 5.75307 5.67771 5.24654 5.36529 4.93412L1.36529 0.934119C1.05288 0.6217 0.546343 0.6217 0.233924 0.934119C-0.0784959 1.24654 -0.0784959 1.75307 0.233924 2.06549L3.66824 5.4998L0.233924 8.93412C-0.0784955 9.24654 -0.0784955 9.75307 0.233924 10.0655C0.546343 10.3779 1.05288 10.3779 1.36529 10.0655L5.36529 6.06549Z"
            fill="#fff" />
        </svg>
      </a>
    </div>
    <Swiper
      :slidesPerView="5"
      :spaceBetween="10"
      :breakpoints="{
        640: { slidesPerView: 2 },
        768: { slidesPerView: 3 },
        1024: { slidesPerView: 4 },
      }"
      class="swiper-container"
    >
      <SwiperSlide v-for="good in clothes" :key="good.article"> <!-- Обновлено: good.article -->
        <GoodCardElement :article="good.article" /> <!-- Обновлено: good.article -->
      </SwiperSlide>
      <SwiperSlide v-if="clothes.length === 0">
        <p>Нет товаров в этой категории.</p>
      </SwiperSlide>
    </Swiper>
  </div>

  <!-- Аксессуары -->
  <div class="goods-container">
    <div class="title-goods">
      <h2>АКСЕССУАРЫ</h2>
      <a href="#">
        БОЛЬШЕ ТОВАРОВ
        <svg width="6" height="11" viewBox="0 0 6 11" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path fill-rule="evenodd" clip-rule="evenodd"
            d="M5.36529 6.06549C5.67771 5.75307 5.67771 5.24654 5.36529 4.93412L1.36529 0.934119C1.05288 0.6217 0.546343 0.6217 0.233924 0.934119C-0.0784959 1.24654 -0.0784959 1.75307 0.233924 2.06549L3.66824 5.4998L0.233924 8.93412C-0.0784955 9.24654 -0.0784955 9.75307 0.233924 10.0655C0.546343 10.3779 1.05288 10.3779 1.36529 10.0655L5.36529 6.06549Z"
            fill="#fff" />
        </svg>
      </a>
    </div>
    <Swiper
      :slidesPerView="5"
      :spaceBetween="10"
      :breakpoints="{
        640: { slidesPerView: 2 },
        768: { slidesPerView: 3 },
        1024: { slidesPerView: 4 },
      }"
      class="swiper-container"
    >
      <SwiperSlide v-for="good in accessories" :key="good.article"> <!-- Обновлено: good.article -->
        <GoodCardElement :article="good.article" /> <!-- Обновлено: good.article -->
      </SwiperSlide>
      <SwiperSlide v-if="accessories.length === 0">
        <p>Нет товаров в этой категории.</p>
      </SwiperSlide>
    </Swiper>
  </div>
</template>

<style scoped>
.goods-container {
  margin: 30px auto;
  width: 85%;
}

.title-goods {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.title-goods h2 {
  color: white;
}

.title-goods a {
  text-decoration: underline;
  text-underline-offset: 5px;
  transition: all 0.6s ease;
  color: white;
  cursor: pointer;
}

.title-goods a:hover {
  color: #72cdfa;
}

/* Стили для Swiper */
.swiper-container {
  margin-top: 10px;
  padding: 20px 0;
}

.loading, .error {
  text-align: center;
  color: white;
  margin-top: 20px;
}
</style>