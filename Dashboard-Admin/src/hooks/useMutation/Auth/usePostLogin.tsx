import { useMutation } from "@tanstack/react-query"
import { api } from "@/config/api"

export const usePostLogin = () => {
  return useMutation({
    mutationFn: (payload: { email: string; password: string }) =>
      api.post("/auth/login", payload),
  })
}