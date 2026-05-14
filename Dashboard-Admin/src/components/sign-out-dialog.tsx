import { useNavigate, useLocation } from '@tanstack/react-router'
import { useAuthStore } from '@/stores/auth-store'
import { ConfirmDialog } from '@/components/confirm-dialog'
import { usePostLogout } from '@/hooks/useMutation/Auth/usePostLogout'

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

  const clearUser = useAuthStore((state) => state.clearUser)

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
      title='Sign out'
      desc='Are you sure you want to sign out? You will need to sign in again to access your account.'
      confirmText='Sign out'
      destructive
      handleConfirm={handleSignOut}
      className='sm:max-w-sm'
    />
  )
}