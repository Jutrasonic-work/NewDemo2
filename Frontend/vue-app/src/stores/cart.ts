import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { useAuthStore } from './auth'

const API_URL = 'http://localhost:5000/api'

interface CartItem {
  id: number
  productId: number
  name: string
  price: number
  quantity: number
}

export const useCartStore = defineStore('cart', () => {
  const items = ref<CartItem[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)
  const authStore = useAuthStore()

  const total = computed(() => {
    return items.value.reduce((sum, item) => sum + item.price * item.quantity, 0)
  })

  const itemCount = computed(() => {
    return items.value.reduce((sum, item) => sum + item.quantity, 0)
  })

  const fetchCart = async () => {
    if (!authStore.user) return

    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/cart`, {
        headers: {
          'Authorization': `Bearer ${authStore.token}`,
          'X-User-Id': authStore.user.id.toString(),
        },
      })
      if (!response.ok) throw new Error('Erreur lors de la récupération du panier')
      items.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const addItem = async (productId: number, quantity: number = 1) => {
    if (!authStore.user) return

    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/cart/items`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${authStore.token}`,
          'X-User-Id': authStore.user.id.toString(),
        },
        body: JSON.stringify({ productId, quantity }),
      })
      if (!response.ok) throw new Error('Erreur lors de l\'ajout au panier')
      await fetchCart()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const updateItem = async (itemId: number, quantity: number) => {
    if (!authStore.user) return

    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/cart/items/${itemId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${authStore.token}`,
          'X-User-Id': authStore.user.id.toString(),
        },
        body: JSON.stringify({ quantity }),
      })
      if (!response.ok) throw new Error('Erreur lors de la mise à jour du panier')
      await fetchCart()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const removeItem = async (itemId: number) => {
    if (!authStore.user) return

    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/cart/items/${itemId}`, {
        method: 'DELETE',
        headers: {
          'Authorization': `Bearer ${authStore.token}`,
          'X-User-Id': authStore.user.id.toString(),
        },
      })
      if (!response.ok) throw new Error('Erreur lors de la suppression du produit')
      await fetchCart()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  return {
    items,
    loading,
    error,
    total,
    itemCount,
    fetchCart,
    addItem,
    updateItem,
    removeItem,
  }
})