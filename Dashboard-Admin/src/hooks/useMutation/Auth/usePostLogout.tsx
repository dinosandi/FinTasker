import { useMutation, useQueryClient } from "@tanstack/react-query"
import { useNavigate } from "@tanstack/react-router"
import { api } from "@/config/api"
import { useAuthStore } from "@/stores/auth-store"

export const usePostLogout = () => {
  const queryClient = useQueryClient()
  const navigate = useNavigate()
  const clearUser = useAuthStore((s) => s.reset)

  return useMutation({
    mutationFn: () => api.post("/auth/logout"),

    onSuccess: () => {
      clearUser()
      queryClient.clear() // bersihkan semua cache
      navigate({ to: "/sign-in", replace: true })
    },

    onError: () => {
      // Tetap logout di FE meski BE error
      clearUser()
      queryClient.clear()
      navigate({ to: "/sign-in", replace: true })
    },
  })
}