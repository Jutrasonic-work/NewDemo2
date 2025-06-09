import api from './api';

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
}

export interface AuthResponse {
    token: string;
    refreshToken: string;
    userId: number;
    email: string;
    firstName: string;
    lastName: string;
}

export const authService = {
    async login(request: LoginRequest): Promise<AuthResponse> {
        const response = await api.post<AuthResponse>('/auth/login', request);
        return response.data;
    },

    async register(request: RegisterRequest): Promise<AuthResponse> {
        const response = await api.post<AuthResponse>('/auth/register', request);
        return response.data;
    },

    async refreshToken(refreshToken: string): Promise<AuthResponse> {
        const response = await api.post<AuthResponse>('/auth/refresh-token', { refreshToken });
        return response.data;
    },

    async logout(): Promise<void> {
        await api.post('/auth/logout');
    }
}; 