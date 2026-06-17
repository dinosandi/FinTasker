import { createFileRoute, redirect } from "@tanstack/react-router"
import { useEffect } from "react"
import { useAuthStore } from "@/stores/auth-store"
import { AuthenticatedLayout } from "@/components/layout/authenticated-layout"

export const Route = createFileRoute("/_authenticated")({
  // 1. Manfaatkan context yang sudah di-fetch oleh komponen App di main.tsx
  beforeLoad: ({ context, location }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({
        to: "/sign-in",
        search: {
          redirect: location.href,
        },
        replace: true,
      })
    }
  },
  component: RouteComponent,
})

function RouteComponent() {
  const { auth } = Route.useRouteContext()
  const setUser = useAuthStore((s) => s.setUser)

  useEffect(() => {
    if (auth.user) {
      setUser(auth.user)
    }
  }, [auth.user, setUser])

  return <AuthenticatedLayout />
}