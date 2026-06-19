import { create } from "zustand"
import type { UserMe } from "@/hooks/useQuery/useMe"

interface AuthState {
  user: UserMe | null
  isAuthenticated: boolean
  setUser: (user: UserMe | null) => void
  reset: () => void
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  isAuthenticated: false,
  setUser: (user) => set({ user, isAuthenticated: !!user }),
  reset: () => set({ user: null, isAuthenticated: false }),
}))
