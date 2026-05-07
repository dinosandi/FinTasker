import { useMutation } from '@tanstack/react-query'
import { api } from '@/config/api'
import { AuthLogin } from '@/Type'

export const usePostLogin = () => {
  return useMutation({
    mutationKey: ['login'],

    mutationFn: async (data: AuthLogin) => {
      const response = await api.post('/Auth/login', data)

      return response.data
    },
  })
}