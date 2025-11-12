<script setup>
import { ref, onMounted, computed, watch } from "vue";
import { useRoute } from "vue-router";
import axios from 'axios';



const route = useRoute();

const good = ref(null);
const sizesMsv = ref(null);

const targetGoodArticle = route.params.articleGood;
onMounted(() => {
    fetchGoodInfo();
});

async function fetchGoodInfo() {
  try {
    const url = "http://localhost:5289/api/Good/GetGoodById?goodId=" + targetGoodArticle;
    console.log(url);
    const response = await axios.get(
      url
    );
    console.log(response);

    good.value = response.data;
    console.log(good);
  } catch (err) {
    console.error("Ошибка при получении инфы о товаре:", err);
  } 
}
</script>

<template>
    <div class="box">
        <img
        :src="good?.image?.path"
        alt="product"
      />
    <div class="info-box">
        <div class="main-info">
        <p id="title-product">{{ good?.subcategory?.name }} {{ good?.brand?.name }} {{ good?.model?.name }}</p>
        <p>Артикул: {{ good?.article }}</p>
        <p>Бренд: {{ good?.brand?.name }}</p>
        <p>Модель: {{ good?.model?.name }}</p>
        
    </div>
    <div class="sizes"  style="display: flex; color: white;">
        <div class="size-element" v-for="size in good?.goodSizes" >
            <p style="text-align: center;">{{ size.size }}</p>
            <p>{{ size.price }} р.</p>
        </div>
    </div>
    </div>
    </div>
</template>

<style scoped>
p {
    font-size: 80;
    color: white;
}

.box {
    display: flex;
}

.size-element {
    padding: 10px;
    
    max-height:max-content;
    background-color: black;
    margin: 10px; 
    border-radius: 20px;
    transition: all .6s ease;
    &:hover {
        background-color: gray;
        transition: all .6s ease;
    }
}

img {
    height: 500px;
    max-height: 100%;
    min-height: 0;
    width: 500px;
    margin:30px;
    border-radius: 20px;
    object-fit:contain;
}

#title-product {
    font-weight: bold;
    font-size: 40px;
}

.info-box {
    display: flex;
    flex-direction: column;
    margin: 30px;
    gap: 20px;
}

.main-info {
    display: flex;
    flex-direction: column;
    gap: 20px;
}
</style>
