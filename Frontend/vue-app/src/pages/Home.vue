<script setup lang="ts">
import { ref, computed } from 'vue';
import HeroSection from '../components/home/HeroSection.vue';
import CategoryCard from '../components/home/CategoryCard.vue';
import ProductCard from '../components/product/ProductCard.vue';
import { useProductStore } from '../stores/product';

const productStore = useProductStore();
const categories = productStore.categories;
const featuredProducts = productStore.getFeaturedProducts;

const allProducts = productStore.products;
const searchQuery = ref('');
const selectedCategory = ref('all');

const filteredProducts = computed(() => {
  let filtered = allProducts;
  
  // Filter by category
  if (selectedCategory.value !== 'all') {
    filtered = filtered.filter(product => product.category === selectedCategory.value);
  }
  
  // Filter by search query
  if (searchQuery.value) {
    const query = searchQuery.value.toLowerCase();
    filtered = filtered.filter(product => 
      product.name.toLowerCase().includes(query) || 
      (product.description && product.description.toLowerCase().includes(query))
    );
  }
  
  return filtered;
});
</script>

<template>
  <div>
    <!-- Hero section -->
    <div class="container-custom my-8">
      <HeroSection />
    </div>
    
    <!-- Categories section -->
    <div class="container-custom my-16">
      <h2 class="text-2xl font-bold text-gray-900 dark:text-white mb-6">Catégories</h2>
      <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
        <CategoryCard 
          v-for="category in categories" 
          :key="category.id" 
          :category="category" 
        />
      </div>
    </div>
    
    <!-- Featured products -->
    <div class="container-custom my-16">
      <h2 class="text-2xl font-bold text-gray-900 dark:text-white mb-6">Produits populaires</h2>
      <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
        <ProductCard 
          v-for="product in featuredProducts" 
          :key="product.id" 
          :product="product" 
        />
      </div>
      <div class="text-center mt-8">
        <RouterLink 
          to="/products" 
          class="btn-primary inline-block"
        >
          Voir tous les produits
        </RouterLink>
      </div>
    </div>
    
    <!-- Promo section -->
    <div class="container-custom my-16 bg-grocery-orange-100 dark:bg-grocery-orange-900 rounded-2xl p-8">
      <div class="flex flex-col md:flex-row items-center">
        <div class="md:w-1/2 mb-6 md:mb-0">
          <h2 class="text-3xl font-bold text-grocery-orange-700 dark:text-grocery-orange-300 mb-4">
            Livraison gratuite dès 50€ d'achat
          </h2>
          <p class="text-gray-700 dark:text-gray-300 mb-6">
            Profitez de notre offre spéciale de livraison gratuite pour toute commande supérieure à 50€. 
            Une occasion parfaite pour faire le plein de produits frais !
          </p>
          <RouterLink 
            to="/products" 
            class="btn-secondary inline-block"
          >
            En profiter maintenant
          </RouterLink>
        </div>
        <div class="md:w-1/2 md:pl-8 flex justify-center">
          <div class="w-64 h-64 rounded-full bg-grocery-orange-200 dark:bg-grocery-orange-800 flex items-center justify-center text-8xl">
            🚚
          </div>
        </div>
      </div>
    </div>
  </div>
</template>