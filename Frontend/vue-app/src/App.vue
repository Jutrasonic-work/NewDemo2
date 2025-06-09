<script setup lang="ts">
import { onMounted, watch } from 'vue';
import { RouterView } from 'vue-router';
import TheHeader from './components/layout/TheHeader.vue';
import TheFooter from './components/layout/TheFooter.vue';
import ToastContainer from './components/ui/ToastContainer.vue';
import { useThemeStore } from './stores/theme';
import { useCartStore } from './stores/cart';
import { useAuthStore } from './stores/auth';

// Initialize stores
const themeStore = useThemeStore();
const cartStore = useCartStore();
const authStore = useAuthStore();

// Apply theme on mount
onMounted(() => {
  themeStore.initTheme();
});

// Watch theme changes
watch(
  () => themeStore.isDark,
  (isDark) => {
    if (isDark) {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
  },
  { immediate: true }
);
</script>

<template>
  <div class="min-h-screen flex flex-col bg-gray-50 dark:bg-gray-900 transition-colors duration-200">
    <TheHeader />
    <main class="flex-grow">
      <RouterView />
    </main>
    <TheFooter />
    <ToastContainer />
  </div>
</template>