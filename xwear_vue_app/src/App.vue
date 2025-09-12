<script setup>
  import { RouterLink, RouterView } from 'vue-router';

    import HeaderElement from './components/HeaderElement.vue'
</script>

<template>
    <HeaderElement/>
    <RouterView/>
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
