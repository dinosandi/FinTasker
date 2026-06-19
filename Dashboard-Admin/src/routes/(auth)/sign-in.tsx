import { createFileRoute, redirect } from "@tanstack/react-router"
import { SignIn } from "@/features/auth/sign-in"
import { useAuthStore } from "@/stores/auth-store"

export const Route = createFileRoute("/(auth)/sign-in")({
  beforeLoad: () => {
    const { isAuthenticated } = useAuthStore.getState()
    console.log('SIGNIN BEFORELOAD isAuthenticated:', isAuthenticated)

    if (isAuthenticated) {
      throw redirect({ to: "/", replace: true })
    }
  },
  component: SignIn,
})