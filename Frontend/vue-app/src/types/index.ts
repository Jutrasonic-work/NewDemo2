export interface Product {
  id: string;
  name: string;
  price: number;
  category: string;
  unit: string;
  inStock: boolean;
  description: string;
  image: string;
  rating: number;
}

export interface CartItem {
  product: Product;
  quantity: number;
}

export interface Category {
  id: string;
  name: string;
  icon: string;
  gradient: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  role: string;
}

export interface Toast {
  id: string;
  message: string;
  type: 'success' | 'error' | 'info';
  duration?: number;
}

export interface AuthState {
  user: User | null;
  isAuthenticated: boolean;
}

export interface CartState {
  items: CartItem[];
}

export interface ThemeState {
  isDark: boolean;
}

export * from './Category';
export * from './Product';