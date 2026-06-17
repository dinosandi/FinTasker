import { useMutation } from "@tanstack/react-query"
import { api } from "@/config/api"


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
  return useMutation({
    mutationFn: (payload: LoginPayload) =>
      api.post<ApiResponse<null>>("/auth/login", payload),
    onSuccess: async () => {
      await syncAuthContext()
    },
  })
}

function syncAuthContext() {
  throw new Error("Function not implemented.")
}
