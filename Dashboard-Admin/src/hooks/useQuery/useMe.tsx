import { useQuery } from "@tanstack/react-query"
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

// Fungsi ini dipanggil di beforeLoad (di luar React)
// sehingga harus berupa plain async function, bukan hook
export const fetchMe = async (): Promise<UserMe> => {
  const res = await api.get<ApiResponse<UserMe>>("/Auth/me")
  return res.data.data
}

// Hook ini dipakai di dalam komponen React
export const useMe = () =>
  useQuery({
    queryKey: ["auth", "me"],
    queryFn: fetchMe,
    retry: false,
    staleTime: 5 * 60 * 1000, // 5 menit — tidak perlu re-fetch terus
  })