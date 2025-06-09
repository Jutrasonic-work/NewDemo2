<script setup lang="ts">
import { useToastStore } from '../../stores/toast';
import { XIcon, CheckCircleIcon, AlertCircleIcon, InfoIcon } from 'lucide-vue-next';

const toastStore = useToastStore();

const getIcon = (type: string) => {
  switch (type) {
    case 'success':
      return CheckCircleIcon;
    case 'error':
      return AlertCircleIcon;
    case 'info':
    default:
      return InfoIcon;
  }
};

const getToastClasses = (type: string) => {
  const baseClasses = 'flex items-center p-4 mb-3 rounded-lg shadow-lg';
  
  switch (type) {
    case 'success':
      return `${baseClasses} bg-grocery-green-100 text-grocery-green-800 dark:bg-grocery-green-800 dark:text-grocery-green-100`;
    case 'error':
      return `${baseClasses} bg-red-100 text-red-800 dark:bg-red-800 dark:text-red-100`;
    case 'info':
    default:
      return `${baseClasses} bg-blue-100 text-blue-800 dark:bg-blue-800 dark:text-blue-100`;
  }
};
</script>

<template>
  <div class="fixed bottom-5 right-5 z-50 max-w-md">
    <transition-group name="fade">
      <div 
        v-for="toast in toastStore.toasts" 
        :key="toast.id" 
        :class="getToastClasses(toast.type)"
      >
        <component 
          :is="getIcon(toast.type)" 
          class="w-5 h-5 mr-2" 
        />
        <p class="flex-grow">{{ toast.message }}</p>
        <button 
          @click="toastStore.removeToast(toast.id)" 
          class="ml-2 text-gray-500 hover:text-gray-700 dark:hover:text-gray-300"
        >
          <XIcon class="w-4 h-4" />
        </button>
      </div>
    </transition-group>
  </div>
</template>