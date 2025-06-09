<script setup lang="ts">
import { useProductStore } from '../../stores/product';

const productStore = useProductStore();
const categories = productStore.categories;

const currentYear = new Date().getFullYear();
</script>

<template>
  <footer class="bg-gray-100 dark:bg-gray-800 pt-12 pb-6 mt-12 transition-colors duration-200">
    <div class="container-custom">
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
        <!-- Company info -->
        <div>
          <h3 class="text-lg font-bold mb-4 text-gray-900 dark:text-white">
            <span class="text-grocery-green-500">Fresh</span><span class="text-grocery-orange-500">Market</span>
          </h3>
          <p class="text-gray-600 dark:text-gray-400 mb-4">
            Votre épicerie en ligne pour des produits frais et de qualité, livrés directement chez vous.
          </p>
        </div>

        <!-- Navigation -->
        <div>
          <h3 class="text-lg font-bold mb-4 text-gray-900 dark:text-white">Navigation</h3>
          <ul class="space-y-2">
            <li>
              <RouterLink to="/" class="text-gray-600 dark:text-gray-400 hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">
                Accueil
              </RouterLink>
            </li>
            <li>
              <RouterLink to="/products" class="text-gray-600 dark:text-gray-400 hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">
                Produits
              </RouterLink>
            </li>
            <li>
              <RouterLink to="/contact" class="text-gray-600 dark:text-gray-400 hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">
                Contact
              </RouterLink>
            </li>
          </ul>
        </div>

        <!-- Categories -->
        <div>
          <h3 class="text-lg font-bold mb-4 text-gray-900 dark:text-white">Catégories</h3>
          <ul class="space-y-2">
            <li v-for="category in categories" :key="category.id">
              <RouterLink :to="`/categories/${category.id}`" class="text-gray-600 dark:text-gray-400 hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">
                {{ category.icon }} {{ category.name }}
              </RouterLink>
            </li>
          </ul>
        </div>

        <!-- Contact -->
        <div>
          <h3 class="text-lg font-bold mb-4 text-gray-900 dark:text-white">Contact</h3>
          <ul class="space-y-2 text-gray-600 dark:text-gray-400">
            <li>📞 01 23 45 67 89</li>
            <li>✉️ contact@freshmarket.fr</li>
            <li>🏠 123 Avenue des Champs-Élysées, 75008 Paris</li>
          </ul>
        </div>
      </div>

      <div class="border-t border-gray-200 dark:border-gray-700 mt-8 pt-8 text-center text-gray-600 dark:text-gray-400">
        <p>© {{ currentYear }} FreshMarket. Tous droits réservés.</p>
        <p class="mt-2">
          <RouterLink to="/" class="hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">Mentions légales</RouterLink> | 
          <RouterLink to="/" class="hover:text-grocery-green-500 dark:hover:text-grocery-green-400 transition-colors">Politique de confidentialité</RouterLink>
        </p>
      </div>
    </div>
  </footer>
</template>