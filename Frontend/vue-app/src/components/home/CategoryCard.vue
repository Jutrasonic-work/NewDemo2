<script setup lang="ts">
import { computed } from 'vue';
import { RouterLink } from 'vue-router';
import type { Category } from '../../types';

const props = defineProps<{
  category: Category;
}>();

const gradientClass = computed(() => {
  return `bg-gradient-to-br ${props.category.gradient}`;
});
</script>

<template>
  <RouterLink :to="`/categories/${category.id}`" class="block">
    <div 
      :class="gradientClass"
      class="rounded-xl p-6 text-white shadow-md hover:shadow-lg transform hover:scale-105 transition-all duration-300"
    >
      <div class="flex flex-col items-center text-center">
        <span class="text-4xl mb-2">{{ category.icon }}</span>
        <h3 class="text-lg font-bold mb-1">{{ category.name }}</h3>
        <p class="text-sm opacity-90">Découvrir</p>
      </div>
    </div>
  </RouterLink>
</template>