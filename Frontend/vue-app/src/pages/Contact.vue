<script setup lang="ts">
import { ref } from 'vue';
import { MailIcon, PhoneIcon, MapPinIcon, SendIcon } from 'lucide-vue-next';
import { useToastStore } from '../stores/toast';

const toastStore = useToastStore();

const name = ref('');
const email = ref('');
const subject = ref('');
const message = ref('');
const isSubmitting = ref(false);

const handleSubmit = () => {
  isSubmitting.value = true;
  
  // Simulate form submission
  setTimeout(() => {
    toastStore.showToast('Votre message a bien été envoyé !', 'success');
    name.value = '';
    email.value = '';
    subject.value = '';
    message.value = '';
    isSubmitting.value = false;
  }, 1000);
};
</script>

<template>
  <div class="container-custom py-8">
    <h1 class="text-3xl font-bold text-gray-900 dark:text-white mb-8">Contactez-nous</h1>
    
    <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
      <!-- Contact form -->
      <div class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
        <h2 class="text-2xl font-semibold text-gray-900 dark:text-white mb-6">Envoyez-nous un message</h2>
        
        <form @submit.prevent="handleSubmit">
          <div class="mb-4">
            <label for="name" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Nom
            </label>
            <input
              id="name"
              v-model="name"
              type="text"
              required
              class="block w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
              placeholder="Votre nom"
            />
          </div>
          
          <div class="mb-4">
            <label for="email" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Email
            </label>
            <input
              id="email"
              v-model="email"
              type="email"
              required
              class="block w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
              placeholder="votre.email@exemple.com"
            />
          </div>
          
          <div class="mb-4">
            <label for="subject" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Sujet
            </label>
            <input
              id="subject"
              v-model="subject"
              type="text"
              required
              class="block w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
              placeholder="Sujet de votre message"
            />
          </div>
          
          <div class="mb-6">
            <label for="message" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
              Message
            </label>
            <textarea
              id="message"
              v-model="message"
              rows="5"
              required
              class="block w-full px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-md shadow-sm placeholder-gray-400 focus:outline-none focus:ring-grocery-green-500 focus:border-grocery-green-500 dark:bg-gray-700 dark:text-white"
              placeholder="Votre message..."
            ></textarea>
          </div>
          
          <button
            type="submit"
            :disabled="isSubmitting"
            class="w-full btn-primary flex justify-center items-center"
          >
            <SendIcon v-if="!isSubmitting" class="w-4 h-4 mr-2" />
            <span v-if="isSubmitting" class="animate-spin mr-2">
              ⟳
            </span>
            <span>{{ isSubmitting ? 'Envoi en cours...' : 'Envoyer le message' }}</span>
          </button>
        </form>
      </div>
      
      <!-- Contact info -->
      <div>
        <div class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6 mb-6">
          <h2 class="text-2xl font-semibold text-gray-900 dark:text-white mb-6">Nos coordonnées</h2>
          
          <div class="space-y-4">
            <div class="flex items-start">
              <div class="flex-shrink-0 h-10 w-10 rounded-full bg-grocery-green-100 dark:bg-grocery-green-800 flex items-center justify-center">
                <MailIcon class="h-5 w-5 text-grocery-green-500" />
              </div>
              <div class="ml-4">
                <h3 class="text-lg font-medium text-gray-900 dark:text-white">Email</h3>
                <p class="text-gray-600 dark:text-gray-400">contact@freshmarket.fr</p>
              </div>
            </div>
            
            <div class="flex items-start">
              <div class="flex-shrink-0 h-10 w-10 rounded-full bg-grocery-orange-100 dark:bg-grocery-orange-800 flex items-center justify-center">
                <PhoneIcon class="h-5 w-5 text-grocery-orange-500" />
              </div>
              <div class="ml-4">
                <h3 class="text-lg font-medium text-gray-900 dark:text-white">Téléphone</h3>
                <p class="text-gray-600 dark:text-gray-400">+33 1 23 45 67 89</p>
              </div>
            </div>
            
            <div class="flex items-start">
              <div class="flex-shrink-0 h-10 w-10 rounded-full bg-blue-100 dark:bg-blue-800 flex items-center justify-center">
                <MapPinIcon class="h-5 w-5 text-blue-500" />
              </div>
              <div class="ml-4">
                <h3 class="text-lg font-medium text-gray-900 dark:text-white">Adresse</h3>
                <p class="text-gray-600 dark:text-gray-400">
                  123 Avenue des Champs-Élysées<br>
                  75008 Paris, France
                </p>
              </div>
            </div>
          </div>
        </div>
        
        <div class="bg-white dark:bg-gray-800 rounded-lg shadow-md p-6">
          <h2 class="text-2xl font-semibold text-gray-900 dark:text-white mb-6">Horaires d'ouverture</h2>
          
          <div class="space-y-2">
            <div class="flex justify-between">
              <span class="text-gray-600 dark:text-gray-400">Lundi - Vendredi</span>
              <span class="font-medium text-gray-900 dark:text-white">8h00 - 20h00</span>
            </div>
            <div class="flex justify-between">
              <span class="text-gray-600 dark:text-gray-400">Samedi</span>
              <span class="font-medium text-gray-900 dark:text-white">9h00 - 19h00</span>
            </div>
            <div class="flex justify-between">
              <span class="text-gray-600 dark:text-gray-400">Dimanche</span>
              <span class="font-medium text-gray-900 dark:text-white">10h00 - 18h00</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>