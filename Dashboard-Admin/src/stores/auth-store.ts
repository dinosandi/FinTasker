import { create } from "zustand"
import type { UserMe } from "@/hooks/useQuery/useMe"

interface AuthState {
  auth: {
    user: UserMe | null
    isAuthenticated: boolean
    setUser: (user: UserMe | null) => void
    reset: () => void
  }
}

export const useAuthStore = create<AuthState>((set) => ({
  auth: {
    user: null,
    isAuthenticated: false,
    setUser: (user) =>
      set((state) => ({ auth: { ...state.auth, user, isAuthenticated: !!user } })),
    reset: () =>
      set((state) => ({ auth: { ...state.auth, user: null, isAuthenticated: false } })),
  },
}))