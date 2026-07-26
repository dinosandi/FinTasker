import { type ImgHTMLAttributes } from 'react'
import { cn } from '@/lib/utils'
import LogoFintasker from '@/assets/image/Logo.png'

interface LogoProps extends ImgHTMLAttributes<HTMLImageElement> {
  className?: string
}

export function Logo({ className, ...props }: LogoProps) {
  return (
    <img
      id='FinTasker-Logo'
      src={LogoFintasker}
      alt='FinTasker Logo'
      className={cn('max-h-32 w-auto', className)}
      {...props}
    />
  )
}