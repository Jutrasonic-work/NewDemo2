<script setup lang="ts">
import { ref, computed } from 'vue';
import { RouterLink, useRoute } from 'vue-router';
import { 
  ShoppingCartIcon, 
  UserIcon, 
  SunIcon, 
  MoonIcon, 
  MenuIcon, 
  XIcon 
} from 'lucide-vue-next';
import { useThemeStore } from '../../stores/theme';
import { useCartStore } from '../../stores/cart';
import { useAuthStore } from '../../stores/auth';
import CartDrawer from '../cart/CartDrawer.vue';
import AuthModal from '../auth/AuthModal.vue';

const route = useRoute();
const themeStore = useThemeStore();
const cartStore = useCartStore();
const authStore = useAuthStore();

const isMenuOpen = ref(false);
const isCartOpen = ref(false);

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const toggleCart = () => {
  isCartOpen.value = !isCartOpen.value;
};

const openAuthModal = () => {
  authStore.openModal();
};

const logout = () => {
  authStore.logout();
};

const navItems = [
  { name: 'Accueil', path: '/' },
  { name: 'Produits', path: '/products' },
  { 
    name: 'Administration', 
    path: '/admin',
    children: [
      { name: 'Tableau de bord', path: '/admin' },
      { name: 'Produits', path: '/admin/products' },
      { name: 'Catégories', path: '/admin/categories' }
    ]
  },
  { name: 'Contact', path: '/contact' }
];

const isActive = (path: string) => {
  if (path === '/' && route.path === '/') {
    return true;
  }
  return path !== '/' && route.path.startsWith(path);
};
</script>

<template>
  <header class="sticky top-0 bg-white dark:bg-gray-800 shadow-sm z-40 transition-colors duration-200">
    <div class="container-custom">
      <div class="flex items-center justify-between h-16">
        <!-- Logo -->
        <RouterLink to="/" class="flex items-center">
          <span class="text-2xl font-bold">
            <span class="text-grocery-green-500">Fresh</span><span class="text-grocery-orange-500">Market</span>
            <span class="text-grocery-orange-500">🛒</span>
          </span>
        </RouterLink>

        <!-- Desktop navigation -->
        <nav class="hidden md:flex items-center space-x-6">
          <ul class="flex space-x-6">
            <li v-for="item in navItems" :key="item.path" class="relative group">
              <RouterLink 
                :to="item.path" 
                class="text-gray-700 dark:text-gray-300 hover:text-grocery-green-500 dark:hover:text-grocery-green-400 font-medium transition-colors"
                :class="{ 'text-grocery-green-500 dark:text-grocery-green-400': isActive(item.path) }"
              >
                {{ item.name }}
              </RouterLink>
              
              <!-- Sous-menu -->
              <div 
                v-if="item.children"
                class="absolute left-0 mt-2 w-48 bg-white dark:bg-gray-800 rounded-md shadow-lg py-1 opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all duration-200"
              >
                <RouterLink 
                  v-for="child in item.children" 
                  :key="child.path"
                  :to="child.path"
                  class="block px-4 py-2 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700"
                  :class="{ 'bg-gray-100 dark:bg-gray-700': isActive(child.path) }"
                >
                  {{ child.name }}
                </RouterLink>
              </div>
            </li>
          </ul>
        </nav>

        <!-- Action buttons -->
        <div class="flex items-center space-x-3">
          <!-- Theme toggle -->
          <button 
            @click="themeStore.toggleTheme" 
            class="p-2 rounded-full hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
            aria-label="Basculer le thème"
          >
            <SunIcon v-if="themeStore.isDark" class="w-5 h-5 text-yellow-400" />
            <MoonIcon v-else class="w-5 h-5 text-gray-700" />
          </button>

          <!-- Cart button -->
          <button 
            @click="toggleCart" 
            class="p-2 rounded-full hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors relative"
            aria-label="Voir le panier"
          >
            <ShoppingCartIcon class="w-5 h-5 text-gray-700 dark:text-gray-300" />
            <span 
              v-if="cartStore.totalItems > 0" 
              class="absolute -top-1 -right-1 bg-grocery-orange-500 text-white text-xs font-bold rounded-full w-5 h-5 flex items-center justify-center"
            >
              {{ cartStore.totalItems }}
            </span>
          </button>

          <!-- Auth button -->
          <div v-if="authStore.isAuthenticated" class="relative">
            <button 
              class="flex items-center space-x-2 p-2 rounded-full hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
            >
              <UserIcon class="w-5 h-5 text-gray-700 dark:text-gray-300" />
              <span class="hidden sm:inline text-sm font-medium text-gray-700 dark:text-gray-300">
                {{ authStore.user?.name }}
              </span>
            </button>
            <div class="absolute right-0 mt-2 w-48 py-2 bg-white dark:bg-gray-800 rounded-md shadow-xl z-50">
              <button 
                @click="logout" 
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700"
              >
                Déconnexion
              </button>
            </div>
          </div>
          <button 
            v-else 
            @click="openAuthModal" 
            class="hidden sm:block py-2 px-4 rounded-lg bg-gradient-to-r from-grocery-green-400 to-grocery-green-500 text-white hover:from-grocery-green-500 hover:to-grocery-green-600 transition-colors"
          >
            Connexion
          </button>

          <!-- Mobile menu button -->
          <button 
            @click="toggleMenu" 
            class="p-2 rounded-md md:hidden hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
            aria-label="Menu"
          >
            <MenuIcon v-if="!isMenuOpen" class="w-6 h-6 text-gray-700 dark:text-gray-300" />
            <XIcon v-else class="w-6 h-6 text-gray-700 dark:text-gray-300" />
          </button>
        </div>
      </div>

      <!-- Mobile navigation -->
      <div 
        v-if="isMenuOpen" 
        class="md:hidden"
      >
        <div class="px-2 pt-2 pb-3 space-y-1">
          <template v-for="item in navItems" :key="item.path">
            <RouterLink 
              :to="item.path"
              @click="isMenuOpen = false"
              class="block px-3 py-2 rounded-md text-base font-medium transition-colors"
              :class="[
                isActive(item.path) 
                  ? 'bg-gradient-to-r from-grocery-green-400 to-grocery-green-500 text-white' 
                  : 'text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700'
              ]"
            >
              {{ item.name }}
            </RouterLink>
            
            <!-- Sous-menu mobile -->
            <template v-if="item.children">
              <RouterLink 
                v-for="child in item.children"
                :key="child.path"
                :to="child.path"
                @click="isMenuOpen = false"
                class="block px-3 py-2 pl-6 rounded-md text-sm font-medium transition-colors text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700"
                :class="{ 'bg-gray-100 dark:bg-gray-700': isActive(child.path) }"
              >
                {{ child.name }}
              </RouterLink>
            </template>
          </template>
          
          <button 
            v-if="!authStore.isAuthenticated"
            @click="openAuthModal(); isMenuOpen = false;"
            class="block w-full text-left px-3 py-2 rounded-md text-base font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
          >
            Connexion
          </button>
        </div>
      </div>
    </div>

    <!-- Cart drawer -->
    <CartDrawer :is-open="isCartOpen" @close="isCartOpen = false" />

    <!-- Auth modal -->
    <AuthModal />
  </header>
</template>