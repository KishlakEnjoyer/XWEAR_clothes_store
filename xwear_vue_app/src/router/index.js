import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ShoesView from '../views/ShoesView.vue'
import ClothesView from '@/views/ClothesView.vue'
import AccessoryView from '@/views/AccessoryView.vue'
import MoreView from '@/views/MoreView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { title: 'XWEAR — Главная' }
    },
    {
      path: '/product/:articleGood',
      name: 'product',
      component: MoreView,
      meta: { title: 'XWEAR — Продукт' }
    },
    {
      path: '/shoes',
      name: 'shoes',
      component: ShoesView,
      meta: { title: 'XWEAR — Обувь' }
    },
    {
      path: '/clothes',
      name: 'clothes',
      component: ClothesView,
      meta: { title: 'XWEAR — Одежда' }
    },
    {
      path: '/accessories',
      name: 'accessories',
      component: AccessoryView,
      meta: { title: 'XWEAR — Аксессуары' }
    }
  ],
})

router.afterEach((to) => {
  // Устанавливаем title из meta, если есть, иначе дефолтный
  document.title = to.meta.title || 'Мой сайт'
})

export default router
