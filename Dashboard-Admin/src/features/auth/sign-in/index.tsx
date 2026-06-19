import { useSearch } from '@tanstack/react-router'
import { Logo } from '@/assets/logo'
import { Card, CardContent, CardHeader } from '@/components/ui/card'
import { AuthLayout } from '../auth-layout'
import { UserAuthForm } from './components/user-auth-form'

export function SignIn() {
  const search = useSearch({ from: '/(auth)/sign-in' })
  const redirectTo = (search as { redirect?: string }).redirect ?? '/'

  return (
    <AuthLayout>
      <Card className='max-w-sm gap-4'>
        <CardHeader>
          <Logo className='mx-auto h-14 w-auto md:h-32 lg:h-40' />
        </CardHeader>
        <CardContent>
          <UserAuthForm redirectTo={redirectTo} />
        </CardContent>
      </Card>
    </AuthLayout>
  )
}
