import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { User } from '../types'
import { useToastStore } from './toast'

const API_URL = 'http://localhost:5000/api'

interface LoginResponse {
  id: number
  username: string
  email: string
  role: string
  token: string
}

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null)
  const token = ref<string | null>(null)
  const loading = ref(false)
  const error = ref<string | null>(null)
  const isAuthenticated = ref(false)
  const isModalOpen = ref(false)
  const toastStore = useToastStore()

  const login = async (username: string, password: string) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      })

      if (!response.ok) {
        throw new Error('Identifiants invalides')
      }

      const data: LoginResponse = await response.json()
      user.value = {
        id: data.id,
        username: data.username,
        email: data.email,
        role: data.role,
      }
      token.value = data.token
      localStorage.setItem('token', data.token)
      isAuthenticated.value = true
      isModalOpen.value = false
      toastStore.showToast(`Bienvenue, ${user.value.username}!`, 'success')
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
      throw e
    } finally {
      loading.value = false
    }
  }

  const logout = async () => {
    loading.value = true
    error.value = null
    try {
      await fetch(`${API_URL}/auth/logout`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${token.value}`,
        },
      })
      user.value = null
      token.value = null
      localStorage.removeItem('token')
      isAuthenticated.value = false
      toastStore.showToast('Vous êtes déconnecté', 'info')
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const checkAuth = async () => {
    const storedToken = localStorage.getItem('token')
    if (!storedToken) return

    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/auth/me`, {
        headers: {
          'Authorization': `Bearer ${storedToken}`,
        },
      })

      if (!response.ok) {
        throw new Error('Session expirée')
      }

      const data = await response.json()
      user.value = data
      token.value = storedToken
      isAuthenticated.value = true
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
      user.value = null
      token.value = null
      localStorage.removeItem('token')
      isAuthenticated.value = false
    } finally {
      loading.value = false
    }
  }

  function register(email: string, password: string) {
    // Simulate registration - in a real app this would create a user in the backend
    if (email && password.length >= 6) {
      user.value = {
        id: '1',
        email,
        name: email.split('@')[0]
      }
      isAuthenticated.value = true
      isModalOpen.value = false
      toastStore.showToast(`Compte créé avec succès! Bienvenue, ${user.value.name}!`, 'success')
      return true
    }
    toastStore.showToast('Veuillez fournir un email valide et un mot de passe d\'au moins 6 caractères', 'error')
    return false
  }

  function openModal() {
    isModalOpen.value = true
  }

  function closeModal() {
    isModalOpen.value = false
  }

  return {
    user,
    token,
    loading,
    error,
    isAuthenticated,
    isModalOpen,
    login,
    logout,
    register,
    openModal,
    closeModal,
    checkAuth,
  }
}, {
  persist: {
    key: 'auth-data',
    storage: localStorage,
    paths: ['user', 'isAuthenticated']
  }
})