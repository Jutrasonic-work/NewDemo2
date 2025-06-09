<script setup lang="ts">
import { ref, computed } from 'vue';
import { useProductStore } from '../stores/product';
import { PlusIcon, TrashIcon, XIcon, PencilIcon } from 'lucide-vue-next';

const productStore = useProductStore();
const categories = productStore.categories;
const isModalOpen = ref(false);
const isEditing = ref(false);

const DEFAULT_PRODUCT_IMAGE = 'https://dummyimage.com/200x200/e0e0e0/ffffff.jpg&text=Produit';

// Fonction pour générer un GUID
const generateGuid = () => {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    const r = Math.random() * 16 | 0;
    const v = c === 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
};

// Liste des unités disponibles
const availableUnits = [
  { id: 'piece', value: 'pièce', label: 'Pièce' },
  { id: 'kg', value: 'kg', label: 'Kilogramme' },
  { id: 'g', value: 'g', label: 'Gramme' },
  { id: 'l', value: 'L', label: 'Litre' },
  { id: 'ml', value: 'mL', label: 'Millilitre' },
  { id: 'pack', value: 'pack', label: 'Pack' },
  { id: 'box', value: 'boîte', label: 'Boîte' }
];

// État pour le formulaire de produit
const newProduct = ref({
  id: '',
  name: '',
  description: '',
  price: 0,
  image: '',
  category: '',
  stock: 0,
  unit: '',
  inStock: true
});

const modalTitle = computed(() => {
  return isEditing.value ? 'Modifier le produit' : 'Ajouter un nouveau produit';
});

const openModal = (product = null) => {
  isModalOpen.value = true;
  isEditing.value = !!product;
  
  if (product) {
    // Mode édition : on copie les valeurs du produit
    newProduct.value = { ...product };
  } else {
    // Mode création : on réinitialise le formulaire
    newProduct.value = {
      id: '',
      name: '',
      description: '',
      price: 0,
      image: '',
      category: '',
      stock: 0,
      unit: '',
      inStock: true
    };
  }
};

const closeModal = () => {
  isModalOpen.value = false;
  isEditing.value = false;
};

// Méthodes pour les produits
const saveProduct = () => {
  if (!newProduct.value.name || !newProduct.value.category) return;
  
  if (isEditing.value) {
    productStore.updateProduct(newProduct.value);
  } else {
    productStore.addProduct({
      ...newProduct.value,
      id: generateGuid()
    });
  }
  
  closeModal();
};

const deleteProduct = (productId: string) => {
  if (confirm('Êtes-vous sûr de vouloir supprimer ce produit ?')) {
    productStore.deleteProduct(productId);
  }
};

// Computed pour l'image du produit
const getProductImage = (imageUrl: string) => {
  return imageUrl || DEFAULT_PRODUCT_IMAGE;
};
</script>

<template>
  <div class="container-custom py-8">
    <div class="flex items-center justify-between mb-8">
      <h1 class="text-3xl font-bold text-gray-900 dark:text-white">Gestion des produits</h1>
      <div class="flex space-x-4">
        <button
          @click="openModal()"
          class="inline-flex items-center px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-grocery-green-600 hover:bg-grocery-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-grocery-green-500"
        >
          <PlusIcon class="h-5 w-5 mr-2" />
          Nouveau produit
        </button>
        <RouterLink 
          to="/admin/categories" 
          class="inline-flex items-center px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm text-sm font-medium text-gray-700 dark:text-gray-300 bg-white dark:bg-gray-800 hover:bg-gray-50 dark:hover:bg-gray-700"
        >
          Gérer les catégories
        </RouterLink>
      </div>
    </div>
    
    <!-- Liste des produits -->
    <div class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
      <div class="overflow-x-auto">
        <table class="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
          <thead>
            <tr>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Image</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Nom</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Prix</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Catégorie</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Stock</th>
              <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-400 uppercase tracking-wider">Actions</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-gray-200 dark:divide-gray-700">
            <tr v-for="product in productStore.products" :key="product.id">
              <td class="px-6 py-4 whitespace-nowrap">
                <img :src="getProductImage(product.image)" alt="" class="h-10 w-10 rounded-full object-cover" />
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">{{ product.name }}</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">{{ product.price.toFixed(2) }} €</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">
                {{ categories.find(c => c.id === product.category)?.name }}
              </td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">{{ product.stock }}</td>
              <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900 dark:text-white">
                <div class="flex space-x-2">
                  <button
                    @click="openModal(product)"
                    class="text-blue-600 hover:text-blue-900 dark:hover:text-blue-400"
                  >
                    <PencilIcon class="h-5 w-5" />
                  </button>
                  <button
                    @click="deleteProduct(product.id)"
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

    <!-- Modal d'ajout/modification de produit -->
    <div v-if="isModalOpen" class="fixed inset-0 z-50 overflow-y-auto" aria-labelledby="modal-title" role="dialog" aria-modal="true">
      <div class="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
        <!-- Overlay -->
        <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" aria-hidden="true" @click="closeModal"></div>

        <!-- Modal -->
        <div class="inline-block align-bottom bg-white dark:bg-gray-800 rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-2xl sm:w-full">
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
            
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Nom</label>
                <input
                  v-model="newProduct.name"
                  type="text"
                  placeholder="Nom du produit"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                />
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Description</label>
                <input
                  v-model="newProduct.description"
                  type="text"
                  placeholder="Description du produit"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                />
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Prix</label>
                <input
                  v-model.number="newProduct.price"
                  type="number"
                  min="0"
                  step="0.01"
                  placeholder="0.00"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                />
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Image URL</label>
                <div class="space-y-2">
                  <input
                    v-model="newProduct.image"
                    type="text"
                    placeholder="URL de l'image"
                    class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                  />
                  <div class="flex items-center space-x-4">
                    <img 
                      :src="getProductImage(newProduct.image)" 
                      alt="Prévisualisation" 
                      class="h-20 w-20 rounded-lg object-cover border border-gray-300 dark:border-gray-600"
                    />
                    <p class="text-sm text-gray-500 dark:text-gray-400">
                      Une image par défaut sera utilisée si aucune URL n'est fournie
                    </p>
                  </div>
                </div>
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Catégorie</label>
                <select
                  v-model="newProduct.category"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                >
                  <option value="">Sélectionner une catégorie</option>
                  <option v-for="category in categories" :key="category.id" :value="category.id">
                    {{ category.name }}
                  </option>
                </select>
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Unité</label>
                <select
                  v-model="newProduct.unit"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                >
                  <option value="">Sélectionner une unité</option>
                  <option v-for="unit in availableUnits" :key="unit.id" :value="unit.id">
                    {{ unit.label }}
                  </option>
                </select>
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Stock</label>
                <input
                  v-model.number="newProduct.stock"
                  type="number"
                  min="0"
                  placeholder="0"
                  class="block w-full px-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                />
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Disponibilité</label>
                <div class="mt-2">
                  <label class="inline-flex items-center">
                    <input
                      v-model="newProduct.inStock"
                      type="checkbox"
                      class="rounded border-gray-300 text-grocery-green-600 shadow-sm focus:border-grocery-green-500 focus:ring focus:ring-grocery-green-500 focus:ring-opacity-50"
                    />
                    <span class="ml-2 text-sm text-gray-700 dark:text-gray-300">En stock</span>
                  </label>
                </div>
              </div>
            </div>
          </div>
          
          <div class="px-4 py-3 sm:px-6 sm:flex sm:flex-row-reverse">
            <button
              @click="saveProduct"
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