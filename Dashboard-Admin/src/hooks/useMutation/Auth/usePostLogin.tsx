import { useMutation, useQueryClient } from "@tanstack/react-query"
import { useNavigate } from "@tanstack/react-router"
import { api } from "@/config/api"
import { useAuthStore } from "@/stores/auth-store"

interface LoginPayload {
  email: string
  password: string
}

interface ApiResponse<T> {
  success: boolean
  message: string
  data: T
}

export const usePostLogin = () => {
  const queryClient = useQueryClient()
  const navigate = useNavigate()
  const clearUser = useAuthStore((s) => s.clearUser)

  return useMutation({
    mutationFn: (payload: LoginPayload) =>
      api.post<ApiResponse<null>>("/Auth/login", payload),

    onSuccess: async () => {
      // Invalidate cache /me agar di-fetch ulang dengan cookie baru
      await queryClient.invalidateQueries({ queryKey: ["auth", "me"] })
      navigate({ to: "/", replace: true })
    },

    onError: () => {
      clearUser()
    },
  })
}