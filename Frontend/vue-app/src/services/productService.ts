import api from './api';

export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    unit: string;
    isAvailable: boolean;
    imageUrl: string;
    rating: number;
}

export const productService = {
    async getAll(): Promise<Product[]> {
        const response = await api.get<Product[]>('/catalog/products');
        return response.data;
    },

    async getById(id: number): Promise<Product> {
        const response = await api.get<Product>(`/catalog/products/${id}/details`);
        return response.data;
    },

    async create(product: Omit<Product, 'id'>): Promise<number> {
        const response = await api.post<number>('/admin/products/create', product);
        return response.data;
    },

    async update(id: number, product: Omit<Product, 'id'>): Promise<void> {
        await api.put(`/admin/products/${id}/update`, product);
    },

    async delete(id: number): Promise<void> {
        await api.delete(`/admin/products/${id}/delete`);
    }
}; 