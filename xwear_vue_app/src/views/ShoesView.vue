<script setup>
    import GoodCardElement from '../components/GoodCardElement.vue'
</script>

<template>
    <div class="catalog-page">
    

    <div class="shoes-container">
      <div class="main-content">
        

        
        <div class="product-list">
          <div class="header">
            <h1>ОБУВЬ</h1>
            <p id="goods-count">0 товаров</p>

            
            <div class="sort">
              <label>Сортировать по:</label>
              <select v-model="sortOption">
                <option value="price-low">От дешевых к дорогим</option>
                <option value="price-high">От дорогих к дешевым</option>
                <option value="name">По названию</option>
              </select>
            </div>
          </div>

          
          <div class="grid">
            <GoodCardElement/>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>