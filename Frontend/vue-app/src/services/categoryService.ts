import api from './api';

export interface Category {
    id: number;
    name: string;
    description: string;
    imageUrl: string;
}

export const categoryService = {
    async getAll(): Promise<Category[]> {
        const response = await api.get<Category[]>('/catalog/categories');
        return response.data;
    },

    async getById(id: number): Promise<Category> {
        const response = await api.get<Category>(`/catalog/categories/${id}/details`);
        return response.data;
    },

    async create(category: Omit<Category, 'id'>): Promise<number> {
        const response = await api.post<number>('/admin/categories/create', category);
        return response.data;
    },

    async update(id: number, category: Omit<Category, 'id'>): Promise<void> {
        await api.put(`/admin/categories/${id}/update`, category);
    },

    async delete(id: number): Promise<void> {
        await api.delete(`/admin/categories/${id}/delete`);
    }
}; 