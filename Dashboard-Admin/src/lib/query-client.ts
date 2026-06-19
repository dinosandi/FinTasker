import { AxiosError } from "axios"
import { QueryCache, QueryClient } from "@tanstack/react-query"
import { toast } from "sonner"

let queryClient: QueryClient | null = null

export const getQueryClient = (): QueryClient => {
  if (queryClient) return queryClient // ← sudah ada, return langsung

  // Dibuat hanya SEKALI — konfigurasi lengkap dari main.tsx dipindah ke sini
  queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        retry: (failureCount, error) => {
          if (import.meta.env.DEV) console.log({ failureCount, error })
          if (failureCount >= 0 && import.meta.env.DEV) return false
          if (failureCount > 3 && import.meta.env.PROD) return false
          return !(
            error instanceof AxiosError &&
            [401, 403].includes(error.response?.status ?? 0)
          )
        },
        refetchOnWindowFocus: import.meta.env.PROD,
        staleTime: 10 * 1000,
      },
      mutations: {
        onError: (error) => {
          // import handleServerError di sini
          import("@/lib/handle-server-error").then(({ handleServerError }) => {
            handleServerError(error)
          })
          if (error instanceof AxiosError) {
            if (error.response?.status === 304) {
              toast.error("Content not modified!")
            }
          }
        },
      },
    },
    queryCache: new QueryCache({
      onError: (error) => {
        if (error instanceof AxiosError) {
          if (error.response?.status === 401) {
            toast.error("Session expired!")
            // lazy import untuk hindari circular dependency
            import("@/stores/auth-store").then(({ useAuthStore }) => {
              useAuthStore.getState().reset()
            })
            import("@tanstack/react-router").then(({ redirect }) => {
              throw redirect({ to: "/sign-in", replace: true })
            })
          }
          if (error.response?.status === 500) {
            toast.error("Internal Server Error!")
          }
        }
      },
    }),
  })

  return queryClient
}