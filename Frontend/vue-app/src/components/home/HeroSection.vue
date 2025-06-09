<script setup lang="ts">
</script>

<template>
  <div class="relative bg-gradient-to-r from-grocery-green-500 to-grocery-orange-500 text-white py-16 rounded-3xl overflow-hidden">
    <!-- Background pattern -->
    <div class="absolute inset-0 opacity-10">
      <div class="absolute -top-24 -left-24 w-64 h-64 rounded-full bg-white"></div>
      <div class="absolute top-1/4 right-1/4 w-96 h-96 rounded-full bg-white"></div>
      <div class="absolute -bottom-24 -right-24 w-64 h-64 rounded-full bg-white"></div>
    </div>
    
    <div class="container-custom relative z-10">
      <div class="max-w-2xl mx-auto text-center">
        <h1 class="text-4xl md:text-5xl font-bold mb-4 drop-shadow-md">
          Produits frais, livrés chez vous
        </h1>
        <p class="text-lg md:text-xl opacity-90 mb-8">
          Découvrez notre sélection de produits frais et de qualité, livrés directement à votre porte.
        </p>
        <div class="flex flex-col sm:flex-row justify-center gap-4">
          <RouterLink to="/products" class="btn bg-white text-grocery-green-600 hover:bg-gray-100 font-semibold px-6 py-3">
            Découvrir nos produits
          </RouterLink>
          <RouterLink to="/categories/all" class="btn bg-grocery-green-700 hover:bg-grocery-green-800 text-white font-semibold px-6 py-3">
            Parcourir les catégories
          </RouterLink>
        </div>
      </div>
    </div>
  </div>
</template>