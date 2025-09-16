import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ShoesView from '../views/ShoesView.vue'
import ClothesView from '@/views/ClothesView.vue'
import BrandsView from '@/views/BrandsView.vue'

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
      path: '/brands',
      name: 'brands',
      component: BrandsView,
      meta: { title: 'XWEAR — Бренды' }
    }
  ],
})

router.afterEach((to) => {
  // Устанавливаем title из meta, если есть, иначе дефолтный
  document.title = to.meta.title || 'Мой сайт'
})

export default router
