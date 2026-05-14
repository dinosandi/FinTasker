import { create } from "zustand"
import type { UserMe } from "@/hooks/useQuery/useMe"

interface AuthState {
  user: UserMe | null
  isAuthenticated: boolean
  setUser: (user: UserMe) => void
  clearUser: () => void
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  isAuthenticated: false,

  setUser: (user) =>
    set({
      user,
      isAuthenticated: true,
    }),

  clearUser: () =>
    set({
      user: null,
      isAuthenticated: false,
    }),
}))