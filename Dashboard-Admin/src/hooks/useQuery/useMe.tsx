import { useQuery } from "@tanstack/react-query"
import { api } from "@/config/api"

export interface UserMe {
  id: string
  email: string
  name: string
  avatar: string
  provider: string
  role: string
}

interface ApiResponse<T> {
  success: boolean
  data: T
}

export const ME_QUERY_KEY = ["me"] 

export const useMe = () => {
  return useQuery({
    queryKey: ME_QUERY_KEY,
    queryFn: async () => {
      const res = await api.get<ApiResponse<UserMe>>("/auth/me")
      return res.data.data
    },
    retry: false,          
    staleTime: 5 * 60 * 1000, 
  })
}