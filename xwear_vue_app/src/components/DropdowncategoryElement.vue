<template>
  <div class="dropdown-container">
    <div class="dropdown">
      <!-- Кнопка-триггер -->
      <button 
        class="dropdown-toggle" 
        @click="isOpen = !isOpen"
      >
        КАТЕГОРИИ
        <span class="arrow" :class="{ rotated: isOpen }">▼</span>
      </button>

      <ul v-if="isOpen" class="dropdown-menu">
        <li class="dropdown-item" >Кроссовки</li>
        <li class="dropdown-item" >Кеды</li>
        <li class="dropdown-item" >Лофферы</li>
        <li class="dropdown-item" >Сандалии</li>
        <li class="dropdown-item" >Шлепанцы</li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Dropdown',
  data() {
    return {
      isOpen: false
    }
  }
}
</script>

<style scoped>
.dropdown-container {
background: none;
  width: 220px;
  border:none;
}

.dropdown-toggle {
  width: 100%;
  padding: 14px 16px;
  background: none;
  border: none;
  cursor: pointer;
  font-weight: bold;
  font-size: 14px;
  text-transform: uppercase;
  color: White;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.arrow {
  transition: transform 0.2s ease;
}

.arrow.rotated {
  transform: rotate(180deg);
}

.dropdown-menu {
  list-style: none;
  margin: 0;
  padding: 0;
  background: none;
  border: none;
  overflow-y: auto;
}

.dropdown-item {
  padding: 12px 16px;
  cursor: pointer;
  transition: background 0.2s;
}

.dropdown-item:hover {
  background-color: #f5f5f5;
  color: black;
}

.dropdown-item:last-child {
  border-bottom: 1px solid white;
}
</style>