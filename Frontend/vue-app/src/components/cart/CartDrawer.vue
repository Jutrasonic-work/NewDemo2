<script setup lang="ts">
import { computed } from 'vue';
import { XIcon, ShoppingCartIcon, PlusIcon, MinusIcon, TrashIcon } from 'lucide-vue-next';
import { useCartStore } from '../../stores/cart';
import type { CartItem } from '../../types';

const props = defineProps<{
  isOpen: boolean;
}>();

const emit = defineEmits<{
  close: [];
}>();

const cartStore = useCartStore();

const formattedPrice = (price: number) => {
  return new Intl.NumberFormat('fr-FR', { 
    style: 'currency', 
    currency: 'EUR' 
  }).format(price);
};

const handleClose = () => {
  emit('close');
};

const handleIncrement = (item: CartItem) => {
  cartStore.updateQuantity(item.product.id, item.quantity + 1);
};

const handleDecrement = (item: CartItem) => {
  if (item.quantity > 1) {
    cartStore.updateQuantity(item.product.id, item.quantity - 1);
  } else {
    cartStore.removeFromCart(item.product.id);
  }
};

const handleRemove = (productId: string) => {
  cartStore.removeFromCart(productId);
};

const isEmpty = computed(() => cartStore.items.length === 0);
</script>

<template>
  <div 
    class="fixed inset-0 z-50 overflow-hidden"
    v-if="isOpen"
  >
    <!-- Backdrop -->
    <div 
      class="absolute inset-0 bg-black bg-opacity-50 transition-opacity"
      @click="handleClose"
    ></div>
    
    <!-- Drawer -->
    <div 
      class="absolute inset-y-0 right-0 w-full max-w-md flex"
    >
      <div 
        class="relative w-full bg-white dark:bg-gray-800 shadow-xl flex flex-col overflow-hidden transition-transform transform"
      >
        <!-- Header -->
        <div class="flex items-center justify-between p-4 border-b border-gray-200 dark:border-gray-700">
          <h2 class="text-lg font-medium text-gray-900 dark:text-white flex items-center">
            <ShoppingCartIcon class="w-5 h-5 mr-2" />
            Votre panier
          </h2>
          <button 
            @click="handleClose"
            class="p-2 rounded-full hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
          >
            <XIcon class="w-5 h-5 text-gray-500 dark:text-gray-400" />
          </button>
        </div>
        
        <!-- Content -->
        <div class="flex-1 overflow-y-auto p-4">
          <!-- Empty state -->
          <div v-if="isEmpty" class="flex flex-col items-center justify-center h-full text-gray-500 dark:text-gray-400">
            <ShoppingCartIcon class="w-16 h-16 mb-4" />
            <p class="text-lg font-medium">Votre panier est vide</p>
            <p class="mt-2 text-center">Ajoutez des produits à votre panier pour les voir apparaître ici.</p>
            <button 
              @click="handleClose"
              class="mt-6 btn-primary"
            >
              Continuer vos achats
            </button>
          </div>
          
          <!-- Cart items -->
          <ul v-else class="divide-y divide-gray-200 dark:divide-gray-700">
            <li 
              v-for="item in cartStore.items" 
              :key="item.product.id"
              class="py-4"
            >
              <div class="flex items-center">
                <div class="w-16 h-16 rounded-md overflow-hidden bg-gray-200 dark:bg-gray-700 flex-shrink-0">
                  <img 
                    :src="item.product.image" 
                    :alt="item.product.name" 
                    class="w-full h-full object-cover"
                    onerror="this.src='https://via.placeholder.com/150'"
                  >
                </div>
                
                <div class="ml-4 flex-1">
                  <div class="flex justify-between">
                    <h3 class="text-sm font-medium text-gray-900 dark:text-white">
                      {{ item.product.name }}
                    </h3>
                    <p class="text-sm font-medium text-gray-900 dark:text-white">
                      {{ formattedPrice(item.product.price * item.quantity) }}
                    </p>
                  </div>
                  
                  <p class="text-sm text-gray-500 dark:text-gray-400">
                    {{ formattedPrice(item.product.price) }} / {{ item.product.unit }}
                  </p>
                  
                  <div class="flex items-center justify-between mt-2">
                    <div class="flex items-center border border-gray-300 dark:border-gray-600 rounded-md">
                      <button 
                        @click="handleDecrement(item)"
                        class="p-1 text-gray-500 hover:text-gray-700 dark:hover:text-gray-300"
                      >
                        <MinusIcon class="w-4 h-4" />
                      </button>
                      
                      <span class="px-2 text-gray-700 dark:text-gray-300">
                        {{ item.quantity }}
                      </span>
                      
                      <button 
                        @click="handleIncrement(item)"
                        class="p-1 text-gray-500 hover:text-gray-700 dark:hover:text-gray-300"
                      >
                        <PlusIcon class="w-4 h-4" />
                      </button>
                    </div>
                    
                    <button 
                      @click="handleRemove(item.product.id)"
                      class="p-1 text-gray-500 hover:text-red-500"
                    >
                      <TrashIcon class="w-4 h-4" />
                    </button>
                  </div>
                </div>
              </div>
            </li>
          </ul>
        </div>
        
        <!-- Footer -->
        <div v-if="!isEmpty" class="border-t border-gray-200 dark:border-gray-700 p-4">
          <div class="flex justify-between mb-4">
            <span class="text-base font-medium text-gray-900 dark:text-white">Total</span>
            <span class="text-base font-medium text-gray-900 dark:text-white">
              {{ formattedPrice(cartStore.totalPrice) }}
            </span>
          </div>
          
          <button class="w-full btn-primary mb-2">
            Passer la commande
          </button>
          
          <button 
            @click="handleClose"
            class="w-full btn-outline"
          >
            Continuer vos achats
          </button>
        </div>
      </div>
    </div>
  </div>
</template>