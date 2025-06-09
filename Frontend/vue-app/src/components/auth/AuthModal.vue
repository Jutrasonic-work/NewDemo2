<script setup lang="ts">
import { ref } from 'vue';
import { XIcon, UserIcon, KeyIcon, MailIcon } from 'lucide-vue-next';
import { useAuthStore } from '../../stores/auth';

const authStore = useAuthStore();
const isLogin = ref(true);
const email = ref('');
const password = ref('');
const isLoading = ref(false);
const errorMessage = ref('');

const toggleForm = () => {
  isLogin.value = !isLogin.value;
  errorMessage.value = '';
};

const handleSubmit = async () => {
  errorMessage.value = '';
  isLoading.value = true;
  
  try {
    if (isLogin.value) {
      const success = authStore.login(email.value, password.value);
      if (!success) {
        errorMessage.value = 'Identifiants incorrects. Veuillez réessayer.';
      }
    } else {
      const success = authStore.register(email.value, password.value);
      if (!success) {
        errorMessage.value = 'L\'inscription a échoué. Veuillez vérifier vos informations.';
      }
    }
  } catch (error) {
    errorMessage.value = 'Une erreur est survenue. Veuillez réessayer.';
  } finally {
    isLoading.value = false;
  }
};

const closeModal = () => {
  authStore.closeModal();
  email.value = '';
  password.value = '';
  errorMessage.value = '';
};
</script>

<template>
  <div 
    v-if="authStore.isModalOpen"
    class="fixed inset-0 z-50 overflow-y-auto"
  >
    <!-- Backdrop -->
    <div 
      class="fixed inset-0 bg-black bg-opacity-50 transition-opacity"
      @click="closeModal"
    ></div>
    
    <!-- Modal -->
    <div class="flex items-center justify-center min-h-screen p-4">
      <div 
        class="relative bg-white dark:bg-gray-800 rounded-lg shadow-xl max-w-md w-full p-6 mx-auto"
      >
        <!-- Close button -->
        <button 
          @click="closeModal"
          class="absolute top-4 right-4 p-2 rounded-full hover:bg-gray-100 dark:hover:bg-gray-700 transition-colors"
        >
          <XIcon class="w-5 h-5 text-gray-500 dark:text-gray-400" />
        </button>
        
        <!-- Header -->
        <div class="text-center mb-6">
          <UserIcon class="w-12 h-12 mx-auto text-grocery-green-500 mb-2" />
          <h2 class="text-2xl font-bold text-gray-900 dark:text-white">
            {{ isLogin ? 'Connexion' : 'Créer un compte' }}
          </h2>
        </div>
        
        <!-- Form -->
        <form @submit.prevent="handleSubmit">
          <!-- Error message -->
          <div 
            v-if="errorMessage"
            class="bg-red-100 text-red-800 dark:bg-red-800 dark:text-red-100 p-3 rounded-md mb-4 text-sm"
          >
            {{ errorMessage }}
          </div>
          
          <!-- Email field -->
          <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Email
            </label>
            <div class="relative">
              <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <MailIcon class="h-5 w-5 text-gray-400" />
              </div>
              <input
                id="email"
                v-model="email"
                type="email"
                required
                class="block w-full pl-10 pr-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                placeholder="email@exemple.com"
              />
            </div>
          </div>
          
          <!-- Password field -->
          <div class="mb-6">
            <label for="password" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Mot de passe
            </label>
            <div class="relative">
              <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <KeyIcon class="h-5 w-5 text-gray-400" />
              </div>
              <input
                id="password"
                v-model="password"
                type="password"
                required
                class="block w-full pl-10 pr-3 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
                placeholder="••••••••"
              />
            </div>
          </div>
          
          <!-- Submit button -->
          <button
            type="submit"
            :disabled="isLoading"
            class="w-full btn-primary flex justify-center items-center"
          >
            <span v-if="isLoading" class="animate-spin mr-2">
              ⟳
            </span>
            <span>{{ isLogin ? 'Se connecter' : 'S\'inscrire' }}</span>
          </button>
        </form>
        
        <!-- Switch form -->
        <div class="mt-4 text-center text-sm text-gray-600 dark:text-gray-400">
          <p>
            {{ isLogin ? 'Pas encore de compte ?' : 'Déjà un compte ?' }}
            <button 
              @click="toggleForm" 
              class="text-grocery-green-500 hover:text-grocery-green-600 font-medium"
            >
              {{ isLogin ? 'Créer un compte' : 'Se connecter' }}
            </button>
          </p>
        </div>
      </div>
    </div>
  </div>
</template>