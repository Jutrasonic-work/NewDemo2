<script setup lang="ts">
import { computed } from 'vue';
import { ShoppingCartIcon, StarIcon } from 'lucide-vue-next';
import { useCartStore } from '../../stores/cart';
import type { Product } from '../../types';

const props = defineProps<{
  product: Product;
}>();

const cartStore = useCartStore();

const formattedPrice = computed(() => {
  return new Intl.NumberFormat('fr-FR', {
    style: 'currency',
    currency: 'EUR'
  }).format(props.product.price);
});

const getCategoryBadgeClass = computed(() => {
  switch (props.product.category) {
    case 'fruits':
      return 'bg-red-100 text-red-800 dark:bg-red-800 dark:text-red-100';
    case 'legumes':
      return 'bg-green-100 text-green-800 dark:bg-green-800 dark:text-green-100';
    case 'laitiers':
      return 'bg-blue-100 text-blue-800 dark:bg-blue-800 dark:text-blue-100';
    case 'boulangerie':
      return 'bg-yellow-100 text-yellow-800 dark:bg-yellow-800 dark:text-yellow-100';
    default:
      return 'bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-100';
  }
});

const categoryLabel = computed(() => {
  switch (props.product.category) {
    case 'fruits':
      return 'Fruits';
    case 'legumes':
      return 'Légumes';
    case 'laitiers':
      return 'Produits laitiers';
    case 'boulangerie':
      return 'Boulangerie';
    default:
      return props.product.category;
  }
});

const addToCart = () => {
  cartStore.addToCart(props.product);
};

const getRatingStars = computed(() => {
  const rating = props.product.rating || 4;
  const fullStars = Math.floor(rating);
  const hasHalfStar = rating % 1 >= 0.5;
  const emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);
  
  return {
    full: fullStars,
    half: hasHalfStar ? 1 : 0,
    empty: emptyStars
  };
});
</script>

<template>
  <div class="card overflow-hidden hover:shadow-lg transform hover:scale-[1.02] transition-all duration-300">
    <!-- Product image -->
    <div class="relative h-48 bg-gray-200 dark:bg-gray-700 overflow-hidden">
      <img 
        :src="product.image || 'https://via.placeholder.com/300x200'" 
        :alt="product.name"
        class="w-full h-full object-cover transition-transform duration-500 hover:scale-110"
      >
      
      <!-- Category badge -->
      <span 
        :class="getCategoryBadgeClass"
        class="absolute top-2 left-2 text-xs font-semibold px-2 py-1 rounded-full"
      >
        {{ categoryLabel }}
      </span>
      
      <!-- Stock badge -->
      <span 
        v-if="!product.inStock"
        class="absolute top-2 right-2 bg-red-500 text-white text-xs font-semibold px-2 py-1 rounded-full"
      >
        Rupture
      </span>
    </div>
    
    <!-- Product info -->
    <div class="p-4">
      <div class="flex items-center space-x-1 mb-1">
        <StarIcon 
          v-for="i in getRatingStars.full" 
          :key="`full-${i}`" 
          class="w-4 h-4 text-yellow-400 fill-current" 
        />
        <StarIcon 
          v-for="i in getRatingStars.half" 
          :key="`half-${i}`" 
          class="w-4 h-4 text-yellow-400" 
        />
        <StarIcon 
          v-for="i in getRatingStars.empty" 
          :key="`empty-${i}`" 
          class="w-4 h-4 text-gray-300 dark:text-gray-600" 
        />
      </div>
      
      <h3 class="text-lg font-medium text-gray-900 dark:text-white mb-1">{{ product.name }}</h3>
      
      <p class="text-sm text-gray-600 dark:text-gray-400 mb-2">
        {{ product.description || `${product.name} frais de haute qualité.` }}
      </p>
      
      <div class="flex justify-between items-center mt-3">
        <div>
          <p class="text-lg font-bold text-gray-900 dark:text-white">{{ formattedPrice }}</p>
          <p class="text-xs text-gray-500 dark:text-gray-400">{{ product.unit }}</p>
        </div>
        
        <button 
          @click="addToCart"
          :disabled="!product.inStock"
          class="btn-secondary flex items-center space-x-1 py-1.5 px-3 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <ShoppingCartIcon class="w-4 h-4" />
          <span class="text-sm">Ajouter</span>
        </button>
      </div>
    </div>
  </div>
</template>