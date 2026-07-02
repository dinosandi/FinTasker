import Logo from '@/assets/image/Logo.png'
import { useSidebar } from '@/components/ui/sidebar'

export function AppLogo() {
  const { state } = useSidebar()

  const collapsed = state === 'collapsed'

  return (
    <div className='flex items-center gap-3 px-3 py-2'>
      <img
        src={Logo}
        alt='FinTasker'
        className='h-10 w-10 object-contain'
      />

      {!collapsed && (
        <div className='flex flex-col leading-none'>
          <span className='text-base font-bold'>
            FinTasker
          </span>

          <span className='text-xs text-muted-foreground'>
            Project Management
          </span>
        </div>
      )}
    </div>
  )
}