import { useNavigate, useLocation } from '@tanstack/react-router'
import { useAuthStore } from '@/stores/auth-store'
import { ConfirmDialog } from '@/components/confirm-dialog'
import { usePostLogout } from '@/hooks/useMutation/Auth/usePostLogout'
import Logout from "@/assets/image/Logout.svg"

interface SignOutDialogProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onConfirm?: () => void
  isLoading?: boolean
}

export function SignOutDialog({
  open,
  onOpenChange,
}: SignOutDialogProps) {
  const navigate = useNavigate()
  const location = useLocation()
  const logout = usePostLogout()

  const clearUser = useAuthStore((state) => state.reset)

  const handleSignOut = async () => {
    try {
      await logout.mutateAsync()
      clearUser()
  
      // Jika pengguna logout 
      navigate({
        to: '/sign-in',
        search: location.search, // Pertahankan query params
        replace: true,
      })
  
    } catch (error) {
      console.error(error)
    }
  }

  return (
    <ConfirmDialog
    open={open}
    onOpenChange={onOpenChange}
    title="Sign Out"
    desc="Are you sure you want to sign out? You will need to sign in again to access your account."
    confirmText="Sign Out"
    handleConfirm={handleSignOut}
    className="sm:max-w-md"
    confirmClassName="
      bg-[#FFD500]
      text-black
      hover:bg-[#e6c000]
    "
  >
    <div className="flex justify-center py-2">
      <img
        src={Logout}
        alt="Logout"
        className="h-45 w-auto"
      />
    </div>
  </ConfirmDialog>
    
  )
}