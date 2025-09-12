<script setup>
    import SliderElement from '../components/SliderElement.vue'
    import GoodCardElement from '../components/GoodCardElement.vue'
</script>

<template>
    <SliderElement/>
    <div class="goods-container">
      <div class="title-goods">
        <h2>ОБУВЬ</h2>
        <a>БОЛЬШЕ ТОВАРОВ <svg width="6" height="11" viewBox="0 0 6 11" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path fill-rule="evenodd" clip-rule="evenodd"
              d="M5.36529 6.06549C5.67771 5.75307 5.67771 5.24654 5.36529 4.93412L1.36529 0.934119C1.05288 0.6217 0.546343 0.6217 0.233924 0.934119C-0.0784959 1.24654 -0.0784959 1.75307 0.233924 2.06549L3.66824 5.4998L0.233924 8.93412C-0.0784955 9.24654 -0.0784955 9.75307 0.233924 10.0655C0.546343 10.3779 1.05288 10.3779 1.36529 10.0655L5.36529 6.06549Z"
              fill="#fff" />
          </svg>
        </a>
      </div>
      <div class="cards-goods" style="margin-top: 10px;">
            <GoodCardElement/>
            <GoodCardElement/>
            <GoodCardElement/>
            <GoodCardElement/>
      </div>
    </div>

    <div class="goods-container">
      <div class="title-goods">
        <h2>ОДЕЖДА</h2>
        <a>БОЛЬШЕ ТОВАРОВ <svg width="6" height="11" viewBox="0 0 6 11" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path fill-rule="evenodd" clip-rule="evenodd"
              d="M5.36529 6.06549C5.67771 5.75307 5.67771 5.24654 5.36529 4.93412L1.36529 0.934119C1.05288 0.6217 0.546343 0.6217 0.233924 0.934119C-0.0784959 1.24654 -0.0784959 1.75307 0.233924 2.06549L3.66824 5.4998L0.233924 8.93412C-0.0784955 9.24654 -0.0784955 9.75307 0.233924 10.0655C0.546343 10.3779 1.05288 10.3779 1.36529 10.0655L5.36529 6.06549Z"
              fill="#fff" />
          </svg>
        </a>
      </div>
      <div class="cards-goods" style="margin-top: 10px;">
            <GoodCardElement/>
            <GoodCardElement/>
            <GoodCardElement/>
            <GoodCardElement/>
        
      </div>
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
  transition: all 0.6s ease;
  color: white;
  cursor: pointer;

  &:hover {
    color: #72cdfa;
    transition: all 0.6s ease;
  }
}

.cards-goods {
  display: flex;
  gap: 20px;
}
</style>