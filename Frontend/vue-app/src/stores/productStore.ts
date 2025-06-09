import { defineStore } from 'pinia';
import { productService, type Product } from '../services/productService';

export const useProductStore = defineStore('product', {
    state: () => ({
        products: [] as Product[],
        loading: false,
        error: null as string | null
    }),

    actions: {
        async fetchProducts() {
            this.loading = true;
            this.error = null;
            try {
                this.products = await productService.getAll();
            } catch (error) {
                this.error = 'Erreur lors du chargement des produits';
                console.error(error);
            } finally {
                this.loading = false;
            }
        },

        async createProduct(product: Omit<Product, 'id'>) {
            this.loading = true;
            this.error = null;
            try {
                const id = await productService.create(product);
                await this.fetchProducts(); // Recharger la liste
                return id;
            } catch (error) {
                this.error = 'Erreur lors de la création du produit';
                console.error(error);
                throw error;
            } finally {
                this.loading = false;
            }
        },

        async updateProduct(id: number, product: Omit<Product, 'id'>) {
            this.loading = true;
            this.error = null;
            try {
                await productService.update(id, product);
                await this.fetchProducts(); // Recharger la liste
            } catch (error) {
                this.error = 'Erreur lors de la mise à jour du produit';
                console.error(error);
                throw error;
            } finally {
                this.loading = false;
            }
        },

        async deleteProduct(id: number) {
            this.loading = true;
            this.error = null;
            try {
                await productService.delete(id);
                await this.fetchProducts(); // Recharger la liste
            } catch (error) {
                this.error = 'Erreur lors de la suppression du produit';
                console.error(error);
                throw error;
            } finally {
                this.loading = false;
            }
        }
    }
}); 