import { createFileRoute, redirect } from "@tanstack/react-router"
import { fetchMe } from "@/hooks/useQuery/useMe"
import {SignIn } from "@/features/auth/sign-in"

export const Route = createFileRoute("/(auth)/sign-in")({
  beforeLoad: async () => {
    try {
      await fetchMe()
      // Kalau fetchMe berhasil → sudah login → ke dashboard
      throw redirect({ to: "/", replace: true })
    } catch (error: any) {
      // Kalau error 401 → belum login → lanjut render sign-in
      if (error?.response?.status === 401) return
      // Error lain (misal redirect) → lempar ulang
      throw error
    }
  },
  component: SignIn,
})