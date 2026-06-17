import { createFileRoute, redirect } from "@tanstack/react-router"
import { useMe } from "@/hooks/useQuery/useMe"
import { SignIn } from "@/features/auth/sign-in"

export const Route = createFileRoute("/(auth)/sign-in")({
  beforeLoad: async ({ context }) => {
    const isLoggedIn = await context.queryClient
      .ensureQueryData({ queryKey: ["auth", "me"], queryFn: useMe })
      .then(() => true)
      .catch(() => false)

    if (isLoggedIn) {
      throw redirect({ to: "/", replace: true })
    }
  },
  component: SignIn,
})