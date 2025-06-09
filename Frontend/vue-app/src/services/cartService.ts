import api from './api';

export interface CartItem {
    id: number;
    productId: number;
    quantity: number;
    product: {
        id: number;
        name: string;
        price: number;
        imageUrl: string;
    };
}

export interface Cart {
    items: CartItem[];
    total: number;
}

export const cartService = {
    async getCart(): Promise<Cart> {
        const response = await api.get<Cart>('/cart');
        return response.data;
    },

    async addItem(productId: number, quantity: number): Promise<void> {
        await api.post('/cart/items/add', { productId, quantity });
    },

    async updateItem(id: number, quantity: number): Promise<void> {
        await api.put(`/cart/items/${id}/update`, { quantity });
    },

    async removeItem(id: number): Promise<void> {
        await api.delete(`/cart/items/${id}/delete`);
    },

    async clearCart(): Promise<void> {
        await api.delete('/cart/clear');
    }
}; 