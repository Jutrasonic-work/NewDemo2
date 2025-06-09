import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import type { Product, Category } from '../types'

const API_URL = 'http://localhost:5000/api'

export const useProductStore = defineStore('product', () => {
  const products = ref<Product[]>([])
  const categories = ref<Category[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)

  // Getters
  const getProductById = computed(() => {
    return (id: string) => products.value.find(product => product.id === id)
  })

  const getProductsByCategory = computed(() => {
    return (category: string) => {
      if (category === 'all') return products.value
      return products.value.filter(product => product.category === category)
    }
  })

  const getFeaturedProducts = computed(() => {
    return products.value.filter(product => product.rating && product.rating >= 4.5)
  })

  // Actions pour les produits
  const fetchProducts = async () => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/products`)
      if (!response.ok) throw new Error('Erreur lors de la récupération des produits')
      products.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const addProduct = async (product: Product) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/products`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(product),
      })
      if (!response.ok) throw new Error('Erreur lors de la création du produit')
      const newProduct = await response.json()
      products.value.push(newProduct)
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const updateProduct = async (product: Product) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/products/${product.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(product),
      })
      if (!response.ok) throw new Error('Erreur lors de la mise à jour du produit')
      const index = products.value.findIndex(p => p.id === product.id)
      if (index !== -1) {
        products.value[index] = product
      }
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const deleteProduct = async (productId: string) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/products/${productId}`, {
        method: 'DELETE',
      })
      if (!response.ok) throw new Error('Erreur lors de la suppression du produit')
      products.value = products.value.filter(p => p.id !== productId)
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  // Actions pour les catégories
  const fetchCategories = async () => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/categories`)
      if (!response.ok) throw new Error('Erreur lors de la récupération des catégories')
      categories.value = await response.json()
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const addCategory = async (category: Category) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/categories`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(category),
      })
      if (!response.ok) throw new Error('Erreur lors de la création de la catégorie')
      const newCategory = await response.json()
      categories.value.push(newCategory)
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const updateCategory = async (category: Category) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/categories/${category.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(category),
      })
      if (!response.ok) throw new Error('Erreur lors de la mise à jour de la catégorie')
      const index = categories.value.findIndex(c => c.id === category.id)
      if (index !== -1) {
        categories.value[index] = category
      }
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  const deleteCategory = async (categoryId: string) => {
    loading.value = true
    error.value = null
    try {
      const response = await fetch(`${API_URL}/categories/${categoryId}`, {
        method: 'DELETE',
      })
      if (!response.ok) throw new Error('Erreur lors de la suppression de la catégorie')
      categories.value = categories.value.filter(c => c.id !== categoryId)
      products.value = products.value.map(product => {
        if (product.category === categoryId) {
          return { ...product, category: 'uncategorized' }
        }
        return product
      })
    } catch (e) {
      error.value = e instanceof Error ? e.message : 'Une erreur est survenue'
    } finally {
      loading.value = false
    }
  }

  return {
    products,
    categories,
    loading,
    error,
    getProductById,
    getProductsByCategory,
    getFeaturedProducts,
    fetchProducts,
    addProduct,
    updateProduct,
    deleteProduct,
    fetchCategories,
    addCategory,
    updateCategory,
    deleteCategory,
  }
})