<template>
    <div class="container mx-auto px-4 py-8">
        <div v-if="store.loading" class="text-center">
            <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900 mx-auto"></div>
            <p class="mt-4">Chargement des produits...</p>
        </div>

        <div v-else-if="store.error" class="text-center text-red-600">
            {{ store.error }}
        </div>

        <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            <div v-for="product in store.products" :key="product.id" 
                 class="bg-white rounded-lg shadow-md overflow-hidden">
                <img :src="product.imageUrl" :alt="product.name" 
                     class="w-full h-48 object-cover">
                <div class="p-4">
                    <h3 class="text-lg font-semibold">{{ product.name }}</h3>
                    <p class="text-gray-600 mt-2">{{ product.description }}</p>
                    <div class="mt-4 flex justify-between items-center">
                        <span class="text-xl font-bold">{{ product.price }}€</span>
                        <span class="text-sm text-gray-500">{{ product.unit }}</span>
                    </div>
                    <div class="mt-4 flex justify-between items-center">
                        <span class="text-sm text-gray-500">Note: {{ product.rating }}/5</span>
                        <button v-if="product.isAvailable" 
                                class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
                            Ajouter au panier
                        </button>
                        <span v-else class="text-red-500">Non disponible</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { useProductStore } from '../stores/productStore';

const store = useProductStore();

onMounted(() => {
    store.fetchProducts();
});
</script> 