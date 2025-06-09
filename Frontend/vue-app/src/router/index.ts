import { createRouter, createWebHistory } from 'vue-router'
import Home from '../pages/Home.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/products/:category?',
    name: 'products',
    component: () => import('../pages/Products.vue')
  },
  {
    path: '/admin',
    name: 'admin',
    component: () => import('../pages/Admin.vue')
  },
  {
    path: '/admin/products',
    name: 'admin-products',
    component: () => import('../pages/AdminProducts.vue')
  },
  {
    path: '/admin/categories',
    name: 'admin-categories',
    component: () => import('../pages/AdminCategories.vue')
  },
  {
    path: '/contact',
    name: 'contact',
    component: () => import('../pages/Contact.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { top: 0 }
  }
})

export default router