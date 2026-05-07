

type AuthLayoutProps = {
  children: React.ReactNode
}

// layout global untuk feature auth, bisa digunakan untuk sign in, sign up, forgot password, dll

export function AuthLayout({ children }: AuthLayoutProps) {
  return (
    <div className='container grid h-svh max-w-none items-center justify-center'>
      <div className='mx-auto flex w-full flex-col justify-center space-y-2 py-8 sm:p-8'>
        <div className='mb-4 flex items-center justify-center'>
          
          <h1 className='text-xl font-medium'></h1>
        </div>
        {children}
      </div>
    </div>
  )
}
