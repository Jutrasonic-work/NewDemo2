import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { Toast } from '../types'

export const useToastStore = defineStore('toast', () => {
  const toasts = ref<Toast[]>([])

  function showToast(message: string, type: 'success' | 'error' | 'info' = 'info', duration = 3000) {
    const id = Date.now().toString()
    
    const toast: Toast = {
      id,
      message,
      type,
      duration
    }
    
    toasts.value.push(toast)
    
    if (duration > 0) {
      setTimeout(() => {
        removeToast(id)
      }, duration)
    }
    
    return id
  }
  
  function removeToast(id: string) {
    const index = toasts.value.findIndex(toast => toast.id === id)
    if (index !== -1) {
      toasts.value.splice(index, 1)
    }
  }
  
  return {
    toasts,
    showToast,
    removeToast
  }
})