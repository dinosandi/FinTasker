import { Skeleton } from '@/components/ui/skeleton'
import { Separator } from '@/components/ui/separator'

export function EditProjectSkeleton() {
  return (
    <div className='space-y-0'>
      {/* preview bar */}
      <div className='mb-6 flex items-center gap-3 rounded-md border border-border bg-muted/40 px-4 py-3'>
        <Skeleton className='h-8 w-8 rounded' />
        <div className='flex-1 space-y-1.5'>
          <Skeleton className='h-4 w-40' />
          <Skeleton className='h-3 w-24' />
        </div>
        <Skeleton className='h-5 w-16 rounded-full' />
      </div>

      {/* section */}
      <Skeleton className='mb-4 h-3 w-12' />

      <div className='space-y-5 py-4'>
        <div className='space-y-2'>
          <Skeleton className='h-3.5 w-10' />
          <Skeleton className='h-9 w-full rounded-md' />
        </div>
        <div className='space-y-2'>
          <Skeleton className='h-3.5 w-20' />
          <Skeleton className='h-[90px] w-full rounded-md' />
        </div>
        <div className='grid grid-cols-2 gap-4'>
          {[0, 1].map((i) => (
            <div key={i} className='space-y-2'>
              <Skeleton className='h-3.5 w-14' />
              <Skeleton className='h-9 w-full rounded-md' />
            </div>
          ))}
        </div>
        <div className='grid grid-cols-2 gap-4'>
          {[0, 1].map((i) => (
            <div key={i} className='space-y-2'>
              <Skeleton className='h-3.5 w-16' />
              <Skeleton className='h-9 w-full rounded-md' />
            </div>
          ))}
        </div>
      </div>

      <Separator />

      <div className='mt-5 space-y-3 py-4'>
        <Skeleton className='h-3 w-24' />
        <div className='flex gap-2'>
          {Array.from({ length: 12 }).map((_, i) => (
            <Skeleton key={i} className='h-7 w-7 rounded-full' />
          ))}
        </div>
      </div>

      <Separator />

      <div className='flex justify-end gap-2 pt-6'>
        <Skeleton className='h-9 w-20 rounded-md' />
        <Skeleton className='h-9 w-28 rounded-md' />
      </div>
    </div>
  )
}