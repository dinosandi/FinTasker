import { useQuery, queryOptions } from "@tanstack/react-query"
import { api } from "@/config/api"

export interface UserMe {
  Name: string
  Email: string
  Role: string
  authProvider: string
  avatarUrl: string | null
}

interface ApiResponse<T> {
  success: boolean
  message: string
  data: T
}

export const fetchMe = async (): Promise<UserMe> => {
  const res = await api.get<ApiResponse<UserMe>>("/auth/me")
  return res.data.data
}

export const meQueryOptions = () =>
  queryOptions({
    queryKey: ["auth", "me"] as const,
    queryFn: fetchMe,
    retry: false,
    staleTime: 5 * 60 * 1000,
  })

export const useMe = () => useQuery(meQueryOptions())