import { createFileRoute, redirect } from "@tanstack/react-router"
import { AuthenticatedLayout } from "@/components/layout/authenticated-layout"
import { useAuthStore } from "@/stores/auth-store"

export const Route = createFileRoute("/_authenticated")({
  beforeLoad: ({ location }) => {
    const { isAuthenticated } = useAuthStore.getState()
    console.log('AUTH BEFORELOAD:', isAuthenticated)

    if (!isAuthenticated) {
      throw redirect({
        to: "/sign-in",
        search: { redirect: location.href },
        replace: true,
      })
    }
  },
  component: () => <AuthenticatedLayout />,
})