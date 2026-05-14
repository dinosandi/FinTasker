import { createFileRoute, redirect } from "@tanstack/react-router"
import { useEffect } from "react"
import { fetchMe } from "@/hooks/useQuery/useMe"
import { useAuthStore } from "@/stores/auth-store"
import {AuthenticatedLayout} from "@/components/layout/authenticated-layout"
import type { UserMe } from "@/hooks/useQuery/useMe"

interface AuthContext {
  user: UserMe    
}

export const Route = createFileRoute("/_authenticated")({
  beforeLoad: async (): Promise<AuthContext> => {
    try {
      const user = await fetchMe()
      return { user }
    } catch {
      throw redirect({
        to: "/sign-in",
        replace: true,
      })
    }
  },

  component: RouteComponent,
})

function RouteComponent() {
  const { user } = Route.useRouteContext()
  const setUser = useAuthStore((s) => s.setUser)

  useEffect(() => {
    setUser(user)
  }, [user, setUser])

  return <AuthenticatedLayout />
}