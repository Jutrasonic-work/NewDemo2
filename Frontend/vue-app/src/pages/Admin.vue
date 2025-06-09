<script setup lang="ts">
import { ref, computed } from 'vue';
import { useProductStore } from '../stores/product';
import { PlusIcon, SaveIcon, TrashIcon, LayoutGridIcon, TagsIcon } from 'lucide-vue-next';
import { RouterLink } from 'vue-router';

const productStore = useProductStore();
const categories = productStore.categories;

// État pour le formulaire de catégorie
const newCategory = ref({
  id: '',
  name: '',
  icon: '📦',
  gradient: 'from-blue-400 to-blue-600'
});

// État pour le formulaire de produit
const newProduct = ref({
  id: '',
  name: '',
  description: '',
  price: 0,
  image: '',
  category: '',
  stock: 0
});

// État pour l'édition
const editingProduct = ref<string | null>(null);
const editingCategory = ref<string | null>(null);

// Computed pour les gradients disponibles
const availableGradients = [
  { id: 'blue', value: 'from-blue-400 to-blue-600', label: 'Bleu' },
  { id: 'green', value: 'from-green-400 to-green-600', label: 'Vert' },
  { id: 'purple', value: 'from-purple-400 to-pink-500', label: 'Violet' },
  { id: 'orange', value: 'from-orange-400 to-red-500', label: 'Orange' },
  { id: 'teal', value: 'from-teal-400 to-teal-600', label: 'Turquoise' }
];

// Computed pour les emojis disponibles
const availableIcons = ['📦', '🛒', '🥑', '🥖', '🥩', '🥛', '🍎', '🍅', '🥕', '🧀'];

// Méthodes pour les catégories
const addCategory = () => {
  if (!newCategory.value.name || !newCategory.value.id) return;
  
  productStore.addCategory({
    id: newCategory.value.id,
    name: newCategory.value.name,
    icon: newCategory.value.icon,
    gradient: newCategory.value.gradient
  });
  
  // Réinitialiser le formulaire
  newCategory.value = {
    id: '',
    name: '',
    icon: '📦',
    gradient: 'from-blue-400 to-blue-600'
  };
};

const deleteCategory = (categoryId: string) => {
  productStore.deleteCategory(categoryId);
};

// Méthodes pour les produits
const addProduct = () => {
  if (!newProduct.value.name || !newProduct.value.category) return;
  
  productStore.addProduct({
    ...newProduct.value,
    id: newProduct.value.id || Date.now().toString()
  });
  
  // Réinitialiser le formulaire
  newProduct.value = {
    id: '',
    name: '',
    description: '',
    price: 0,
    image: '',
    category: '',
    stock: 0
  };
};

const deleteProduct = (productId: string) => {
  productStore.deleteProduct(productId);
};
</script>

<template>
  <div class="container-custom py-8">
    <h1 class="text-3xl font-bold text-gray-900 dark:text-white mb-8">Administration</h1>
    
    <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
      <!-- Gestion des produits -->
      <RouterLink 
        to="/admin/products"
        class="block p-6 bg-white dark:bg-gray-800 rounded-lg shadow-md hover:shadow-lg transition-shadow"
      >
        <div class="flex items-center space-x-4">
          <div class="p-3 bg-grocery-green-100 dark:bg-grocery-green-900 rounded-lg">
            <LayoutGridIcon class="w-6 h-6 text-grocery-green-600 dark:text-grocery-green-400" />
          </div>
          <div>
            <h2 class="text-xl font-semibold text-gray-900 dark:text-white">Gestion des produits</h2>
            <p class="text-gray-600 dark:text-gray-400 mt-1">Ajouter, modifier ou supprimer des produits</p>
          </div>
        </div>
      </RouterLink>
      
      <!-- Gestion des catégories -->
      <RouterLink 
        to="/admin/categories"
        class="block p-6 bg-white dark:bg-gray-800 rounded-lg shadow-md hover:shadow-lg transition-shadow"
      >
        <div class="flex items-center space-x-4">
          <div class="p-3 bg-grocery-orange-100 dark:bg-grocery-orange-900 rounded-lg">
            <TagsIcon class="w-6 h-6 text-grocery-orange-600 dark:text-grocery-orange-400" />
          </div>
          <div>
            <h2 class="text-xl font-semibold text-gray-900 dark:text-white">Gestion des catégories</h2>
            <p class="text-gray-600 dark:text-gray-400 mt-1">Organiser les catégories de produits</p>
          </div>
        </div>
      </RouterLink>
    </div>
  </div>
</template> 