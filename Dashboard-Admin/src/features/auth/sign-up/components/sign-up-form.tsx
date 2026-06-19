import { useState } from 'react'
import { z } from 'zod'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { Loader2, UserPlus } from 'lucide-react'
import { toast } from 'sonner'
import {  cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
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

import { usePostRegister } from '@/hooks/useMutation/Auth/usePostRegister'
import {  useNavigate } from '@tanstack/react-router'
import { Role } from '@/Type'
import { GoogleLogin } from '@react-oauth/google'


const formSchema = z
  .object({
    email: z.email({
      error: (iss) =>
        iss.input === '' ? 'Please enter your email.' : undefined,
    }),
    passwordHash: z
      .string()
      .min(1, 'Please enter your password.')
      .min(7, 'Password must be at least 7 characters long.'),
    name: z
      .string()
      .min(1, 'Please enter your name.')
      .min(3, 'Name must be at least 3 characters long.'),
    PhoneNumber: z
      .string()
      .min(1, 'Please enter your phone number.')
      .min(12, 'Phone number must be at least 12 characters long.'),
    role: z.nativeEnum(Role),

  })

export function SignUpForm({
  className,
  ...props
}: React.HTMLAttributes<HTMLFormElement>) {
  const [isLoading, setIsLoading] = useState(false)
  const navigate = useNavigate()

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      email: '',
      passwordHash: '',
      name: '',
      PhoneNumber: '',
      role: Role.user,
    },
  })

  const registerMutation = usePostRegister()

  async function onSubmit(data: z.infer<typeof formSchema>) {
    setIsLoading(true)
  
    try {
      const response = await toast.promise(
        registerMutation.mutateAsync({
          email: data.email,
          passwordHash: data.passwordHash,
          name: data.name,
          phoneNumber: data.PhoneNumber,
          role: data.role,
        }),
        {
          loading: 'Creating account...',
  
          success: (response) => {
            return response.message || `Account created for ${data.email}.`
          },
  
          error: (error: any) => {
            return (
              error?.response?.data?.message ||
              'Failed to create account.'
            )
          },
        }
      )
  
      console.log(response)
  
      // redirect setelah sukses
      navigate({
        to: '/sign-in',
      })
    } finally {
      setIsLoading(false)
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
          name='name'
          render={({ field }) => (
            <FormItem>
              <FormLabel>Name</FormLabel>
              <FormControl>
                <Input placeholder='Alex' {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
          <FormField
            control={form.control}
            name='PhoneNumber'
            render={({ field }) => (
              <FormItem>
                <FormLabel>Phone Number</FormLabel>
                <FormControl>
                  <Input placeholder='0812-3456-7890' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />

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
            <FormItem>
              <FormLabel>Password</FormLabel>
              <FormControl>
                <PasswordInput placeholder='********' {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
          {/* <FormField
            control={form.control}
            name='role'
            render={({ field }) => (
              <FormItem>
                <FormLabel>Role</FormLabel>
                <FormControl>
                  <select
                    className='w-full rounded-md border border-input bg-background px-3 py-2 text-sm ring-offset-background file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:cursor-not-allowed disabled:opacity-50'
                    {...field}
                  >
                    <option value={Role.user}>User</option>
                    <option value={Role.admin}>Admin</option>
                  </select>
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          /> */}
       
        <Button className='mt-2 bg-[#FFD500]' disabled={isLoading}>
          {isLoading ? <Loader2 className='animate-spin' /> : <UserPlus />}
          Create Account
        </Button>

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
            onSuccess={(credentialResponse) => {
              console.log(credentialResponse);

            }}
            onError={() => {
              console.log('Login Failed');
            }}
          />
        </div>
      </form>
    </Form>
  )
}
