import { z } from 'zod'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useQueryClient } from '@tanstack/react-query'
import { Link } from '@tanstack/react-router'
import { useNavigate } from '@tanstack/react-router'
import { api } from '@/config/api'
import { GoogleLogin } from '@react-oauth/google'
import { Loader2, LogIn } from 'lucide-react'
import { toast } from 'sonner'
import { useAuthStore } from '@/stores/auth-store'
import { cn } from '@/lib/utils'
import { usePostLogin } from '@/hooks/useMutation/Auth/usePostLogin'
import { usePostLoginGoogle } from '@/hooks/useMutation/Auth/usePostLoginGoogle'
import { ME_QUERY_KEY } from '@/hooks/useQuery/useMe'
import { Button } from '@/components/ui/button'
import { CardDescription } from '@/components/ui/card'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { PasswordInput } from '@/components/password-input'

const formSchema = z.object({
  email: z.email({
    error: (iss) => (iss.input === '' ? 'Please enter your email.' : undefined),
  }),
  passwordHash: z
    .string()
    .min(1, 'Please enter your password.')
    .min(7, 'Password must be at least 7 characters long.'),
})

interface UserAuthFormProps extends React.HTMLAttributes<HTMLFormElement> {
  redirectTo?: string
}

export function UserAuthForm({
  className,
  redirectTo,
  ...props
}: UserAuthFormProps) {
  const navigate = useNavigate()
  const loginGoogleMutation = usePostLoginGoogle()
  const queryClient = useQueryClient()
  const loginMutation = usePostLogin()

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: { email: '', passwordHash: '' },
  })

  async function onSubmit(data: z.infer<typeof formSchema>) {
    try {
      console.log('[1] mutateAsync start')
      await loginMutation.mutateAsync({
        email: data.email,
        password: data.passwordHash,
      })
      console.log('[2] mutateAsync done')

      queryClient.removeQueries({ queryKey: ME_QUERY_KEY })
      const user = await queryClient.fetchQuery({
        queryKey: ME_QUERY_KEY,
        queryFn: async () => {
          const res = await api.get('/auth/me')
          return res.data.data
        },
        staleTime: 0,
      })

      console.log('[3] /me fetched, invalidating router')

      useAuthStore.getState().setUser(user)

      console.log('[4] router invalidated, navigating to:', redirectTo || '/')

      toast.success('Login successful')

      await navigate({ to: redirectTo || '/', replace: true }) // ← ini yang hilang
      console.log('[5] navigate called')
    } catch (error: any) {
      console.error('[ERROR] onSubmit catch:', error)
      useAuthStore.getState().reset()
      toast.error(error?.response?.data?.message || error.message)
    }
  }
  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className={cn('grid gap-3', className)}
        {...props}
      >
        <FormField
          control={form.control}
          name='email'
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input placeholder='name@example.com' {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name='passwordHash'
          render={({ field }) => (
            <FormItem className='relative'>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <PasswordInput placeholder='********' {...field} />
              </FormControl>
              <FormMessage />
              <Link
                to='/forgot-password'
                className='absolute inset-e-0 -top-0.5 text-sm font-medium text-muted-foreground hover:opacity-75'
              >
                Forgot password?
              </Link>
            </FormItem>
          )}
        />
        <Button
          className='mt-2 bg-[#FFD500] hover:bg-[#1d346a]'
          disabled={loginMutation.isPending}
        >
          {loginMutation.isPending ? (
            <Loader2 className='animate-spin' />
          ) : (
            <LogIn />
          )}
          Log In
        </Button>
        <CardDescription>
          Enter your email and password below to log into{' '}
          <br className='max-sm:hidden' /> Don&apos;t have an account?{' '}
          <Link
            to='/sign-up'
            className='text-nowrap underline underline-offset-4 hover:text-primary'
          >
            Sign Up
          </Link>
        </CardDescription>
        <div className='relative my-2'>
          <div className='absolute inset-0 flex items-center'>
            <span className='w-full border-t' />
          </div>
          <div className='relative flex justify-center text-xs uppercase'>
            <span className='bg-background px-2 text-muted-foreground'>
              Or continue with
            </span>
          </div>
        </div>

        <div className='grid grid-cols-1 gap-2'>
          <GoogleLogin
            onSuccess={async (credentialResponse) => {
              try {
                await loginGoogleMutation.mutateAsync({
                  idToken: credentialResponse.credential!,
                })

                queryClient.removeQueries({ queryKey: ME_QUERY_KEY })
                const user = await queryClient.fetchQuery({
                  queryKey: ME_QUERY_KEY,
                  queryFn: async () => {
                    const res = await api.get('/auth/me')
                    return res.data.data
                  },
                  staleTime: 0,
                })

                useAuthStore.getState().setUser(user)

                toast.success('Login successful')
                await navigate({ to: redirectTo || '/', replace: true })
              } catch (error) {
                useAuthStore.getState().reset()
                toast.error('Login failed')
              }
            }}
          />
        </div>
      </form>
    </Form>
  )
}
