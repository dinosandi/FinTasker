import { useMutation, useQueryClient } from "@tanstack/react-query"
import { useNavigate } from "@tanstack/react-router"
import { api } from "@/config/api"
import { useAuthStore } from "@/stores/auth-store"

export const usePostLogout = () => {
  const queryClient = useQueryClient()
  const navigate = useNavigate()
  const clearUser = useAuthStore((s) => s.clearUser)

  return useMutation({
    mutationFn: () => api.post("/Auth/logout"),

    onSuccess: () => {
      clearUser()
      queryClient.clear() 
      navigate({ to: "/sign-in", replace: true })
    },

    onError: () => {
      clearUser()
      queryClient.clear()
      navigate({ to: "/sign-in", replace: true })
    },
  })
}