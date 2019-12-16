import { createContext } from 'react';

export const AuthContext = createContext();

export function useAuth() {
    return window.isLog;//useContext(AuthContext);
}