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
    },
    {
      path: '/shoes',
      name: 'shoes',
      component: ShoesView,
    },
    {
      path: '/clothes',
      name: 'clothes',
      component: ClothesView,
    },
    {
      path: '/brands',
      name: 'brands',
      component: BrandsView,
    }
  ],
})

export default router
