<script setup lang="ts">
import { ref, computed, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { SearchIcon, FilterIcon } from 'lucide-vue-next';
import ProductCard from '../components/product/ProductCard.vue';
import { useProductStore } from '../stores/product';

const route = useRoute();
const router = useRouter();
const productStore = useProductStore();
const categories = productStore.categories;

const searchQuery = ref('');
const sortOption = ref('name-asc');

// Get category from route params
const currentCategory = computed(() => route.params.category as string || 'all');

const category = computed(() => {
  if (currentCategory.value === 'all') {
    return { id: 'all', name: 'Tous les produits', icon: '🛒', gradient: 'from-purple-400 to-pink-500' };
  }
  return categories.find(c => c.id === currentCategory.value) || categories[0];
});

const filteredProducts = computed(() => {
  let filtered = currentCategory.value === 'all' 
    ? [...productStore.products]
    : productStore.getProductsByCategory(currentCategory.value);
  
  // Filter by search query
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase();
    filtered = filtered.filter(product => 
      product.name.toLowerCase().includes(query) || 
      (product.description && product.description.toLowerCase().includes(query))
    );
  }
  
  // Sort products
  filtered.sort((a, b) => {
    switch (sortOption.value) {
      case 'name-asc':
        return a.name.localeCompare(b.name);
      case 'name-desc':
        return b.name.localeCompare(a.name);
      case 'price-asc':
        return a.price - b.price;
      case 'price-desc':
        return b.price - a.price;
      default:
        return 0;
    }
  });
  
  return filtered;
});

const resetFilters = () => {
  searchQuery.value = '';
  sortOption.value = 'name-asc';
  if (currentCategory.value !== 'all') {
    router.push('/products');
  }
};

// Reset search when category changes
watch(() => route.params.category, () => {
  searchQuery.value = '';
});
</script>

<template>
  <div class="container-custom py-8">
    <!-- Category header -->
    <div 
      :class="`bg-gradient-to-r ${category.gradient} text-white rounded-2xl p-8 mb-8`"
    >
      <div class="flex flex-col md:flex-row items-center justify-between">
        <div class="flex items-center mb-4 md:mb-0">
          <span class="text-4xl mr-4">{{ category.icon }}</span>
          <h1 class="text-3xl font-bold">{{ category.name }}</h1>
        </div>
        
        <!-- Search and sort -->
        <div class="w-full md:w-auto flex flex-col md:flex-row gap-4">
          <!-- Search input -->
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <SearchIcon class="h-5 w-5 text-white/70" />
            </div>
            <input
              v-model="searchQuery"
              type="text"
              placeholder="Rechercher un produit..."
              class="block w-full pl-10 pr-3 py-2 bg-white/20 backdrop-blur-sm border border-white/30 rounded-full placeholder-white/70 text-white focus:outline-none focus:ring-2 focus:ring-white/50"
            />
          </div>
          
          <!-- Sort options -->
          <select
            v-model="sortOption"
            class="block w-full md:w-48 px-3 py-2 bg-white/20 backdrop-blur-sm border border-white/30 rounded-full text-white focus:outline-none focus:ring-2 focus:ring-white/50 appearance-none"
          >
            <option value="name-asc">Nom (A-Z)</option>
            <option value="name-desc">Nom (Z-A)</option>
            <option value="price-asc">Prix (croissant)</option>
            <option value="price-desc">Prix (décroissant)</option>
          </select>
        </div>
      </div>
    </div>
    
    <!-- Category tabs -->
    <div class="flex overflow-x-auto pb-2 mb-6 no-scrollbar">
      <RouterLink 
        to="/products"
        class="flex-shrink-0 px-4 py-2 rounded-full mr-2 transition-colors whitespace-nowrap"
        :class="[
          currentCategory === 'all' 
            ? 'bg-gray-800 text-white dark:bg-white dark:text-gray-800' 
            : 'bg-gray-100 text-gray-800 hover:bg-gray-200 dark:bg-gray-700 dark:text-white dark:hover:bg-gray-600'
        ]"
      >
        🛒 Tous
      </RouterLink>
      
      <RouterLink 
        v-for="cat in categories"
        :key="cat.id"
        :to="`/products/${cat.id}`"
        class="flex-shrink-0 px-4 py-2 rounded-full mr-2 transition-colors whitespace-nowrap"
        :class="[
          currentCategory === cat.id 
            ? 'bg-gray-800 text-white dark:bg-white dark:text-gray-800' 
            : 'bg-gray-100 text-gray-800 hover:bg-gray-200 dark:bg-gray-700 dark:text-white dark:hover:bg-gray-600'
        ]"
      >
        {{ cat.icon }} {{ cat.name }}
      </RouterLink>
    </div>
    
    <!-- Active filters -->
    <div class="flex flex-wrap items-center gap-2 mb-6" v-if="searchQuery">
      <div class="bg-gray-100 dark:bg-gray-700 rounded-full px-3 py-1 text-sm flex items-center">
        <span class="mr-1">Recherche:</span>
        <span class="font-medium">{{ searchQuery }}</span>
        <button @click="searchQuery = ''" class="ml-2 text-gray-500 hover:text-gray-700 dark:hover:text-gray-300">
          &times;
        </button>
      </div>
      
      <button 
        @click="resetFilters"
        class="text-sm text-grocery-green-600 dark:text-grocery-green-400 hover:underline ml-2"
      >
        Réinitialiser les filtres
      </button>
    </div>
    
    <!-- Products grid -->
    <div v-if="filteredProducts.length > 0" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
      <ProductCard 
        v-for="product in filteredProducts" 
        :key="product.id" 
        :product="product" 
      />
    </div>
    
    <!-- Empty state -->
    <div 
      v-else 
      class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-8 text-center"
    >
      <div class="text-5xl mb-4">🔍</div>
      <h3 class="text-xl font-medium text-gray-900 dark:text-white mb-2">Aucun produit trouvé</h3>
      <p class="text-gray-600 dark:text-gray-400 mb-4">
        Aucun produit ne correspond à vos critères de recherche.
      </p>
      <button 
        @click="resetFilters"
        class="btn-primary"
      >
        Réinitialiser les filtres
      </button>
    </div>
  </div>
</template>

<style scoped>
.no-scrollbar::-webkit-scrollbar {
  display: none;
}
.no-scrollbar {
  -ms-overflow-style: none;
  scrollbar-width: none;
}
</style>