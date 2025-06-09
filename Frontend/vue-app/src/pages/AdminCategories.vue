<script setup lang="ts">
import { ref, computed } from 'vue';
import { useProductStore } from '../stores/product';
import { PlusIcon, TrashIcon, XIcon, PencilIcon } from 'lucide-vue-next';

const productStore = useProductStore();
const categories = productStore.categories;
const isModalOpen = ref(false);
const isEditing = ref(false);

// État pour le formulaire de catégorie
const newCategory = ref({
  id: '',
  name: '',
  icon: '📦',
  gradient: 'from-blue-400 to-blue-600'
});

// Fonction pour générer un GUID
const generateGuid = () => {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0;
    const v = c === 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
};

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

const modalTitle = computed(() => {
  return isEditing.value ? 'Modifier la catégorie' : 'Ajouter une nouvelle catégorie';
});

const openModal = (category = null) => {
  isModalOpen.value = true;
  isEditing.value = !!category;
  
  if (category) {
    // Mode édition : on copie les valeurs de la catégorie
    newCategory.value = { ...category };
  } else {
    // Mode création : on réinitialise le formulaire
    newCategory.value = {
      id: '',
      name: '',
      icon: '📦',
      gradient: 'from-blue-400 to-blue-600'
    };
  }
};

const closeModal = () => {
  isModalOpen.value = false;
  isEditing.value = false;
};

// Méthodes pour les catégories
const saveCategory = () => {
  if (!newCategory.value.name) return;
  
  if (isEditing.value) {
    productStore.updateCategory(newCategory.value);
  } else {
    productStore.addCategory({
      ...newCategory.value,
      id: generateGuid()
    });
  }
  
  closeModal();
};

const deleteCategory = (categoryId: string) => {
  if (confirm('Êtes-vous sûr de vouloir supprimer cette catégorie ? Les produits associés seront marqués comme "non catégorisés".')) {
    productStore.deleteCategory(categoryId);
  }
};
</script>

<template>
  <div class="container-custom py-8">
    <div class="flex items-center justify-between mb-8">
      <h1 class="text-3xl font-bold text-gray-900 dark:text-white">Gestion des catégories</h1>
      <div class="flex space-x-4">
        <button
          @click="openModal()"
          class="inline-flex items-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-grocery-green-600 hover:bg-grocery-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-grocery-green-500"
        >
          <PlusIcon class="h-5 w-5 mr-2" />
          Nouvelle catégorie
        </button>
        <RouterLink 
          to="/admin/products" 
          class="inline-flex items-center px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm text-sm font-medium text-gray-700 dark:text-gray-300 bg-white dark:bg-gray-800 hover:bg-gray-50 dark:hover:bg-gray-700"
        >
          Gérer les produits
        </RouterLink>
      </div>
    </div>
    
    <!-- Liste des catégories -->
    <div class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
          <thead>
            <tr>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Icône</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">ID</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Nom</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Style</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Actions</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-gray-200 dark:divide-gray-700">
            <tr v-for="category in categories" :key="category.id">
              <td class="px-6 py-4 whitespace-nowrap text-2xl">{{ category.icon }}</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">{{ category.id }}</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">{{ category.name }}</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">
                <div :class="`w-20 h-6 rounded bg-gradient-to-r ${category.gradient}`"></div>
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">
                <div class="flex space-x-2">
                  <button
                    @click="openModal(category)"
                    class="text-blue-600 hover:text-blue-900 dark:hover:text-blue-400"
                  >
                    <PencilIcon class="h-5 w-5" />
                  </button>
                  <button
                    @click="deleteCategory(category.id)"
                    class="text-red-600 hover:text-red-900 dark:hover:text-red-400"
                  >
                    <TrashIcon class="h-5 w-5" />
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- Modal d'ajout/modification de catégorie -->
    <div v-if="isModalOpen" class="fixed inset-0 z-50 overflow-y-auto" aria-labelledby="modal-title" role="dialog" aria-modal="true">
      <div class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
        <!-- Overlay -->
        <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" aria-hidden="true" @click="closeModal"></div>

        <!-- Modal -->
        <div class="inline-block align-bottom bg-white dark:bg-gray-800 rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">
          <div class="absolute top-0 right-0 pt-4 pr-4">
            <button
              @click="closeModal"
              class="text-gray-400 hover:text-gray-500 focus:outline-none"
            >
              <XIcon class="h-6 w-6" />
            </button>
          </div>
          
          <div class="px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
            <h3 class="text-lg font-medium leading-6 text-gray-900 dark:text-white mb-4">
              {{ modalTitle }}
            </h3>
            
            <div class="space-y-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Nom</label>
                <input
                  v-model="newCategory.name"
                  type="text"
                  placeholder="Nom de la catégorie"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                />
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Icône</label>
                <select
                  v-model="newCategory.icon"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                >
                  <option v-for="icon in availableIcons" :key="icon" :value="icon">
                    {{ icon }}
                  </option>
                </select>
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Style</label>
                <select
                  v-model="newCategory.gradient"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                >
                  <option v-for="gradient in availableGradients" :key="gradient.id" :value="gradient.value">
                    {{ gradient.label }}
                  </option>
                </select>
              </div>
            </div>
          </div>
          
          <div class="px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
            <button
              @click="saveCategory"
              class="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-grocery-green-600 text-base font-medium text-white hover:bg-grocery-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-grocery-green-500 sm:ml-3 sm:w-auto sm:text-sm"
            >
              {{ isEditing ? 'Modifier' : 'Ajouter' }}
            </button>
            <button
              @click="closeModal"
              class="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-grocery-green-500 sm:mt-0 sm:ml-3 sm:w-auto sm:text-sm"
            >
              Annuler
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template> 