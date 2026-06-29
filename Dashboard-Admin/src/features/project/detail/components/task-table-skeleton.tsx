import { Skeleton } from '@/components/ui/skeleton'

export function TasksTableSkeleton() {
  return (
    <div className='w-full space-y-3'>
      {/* Toolbar skeleton */}
      <div className='flex items-center gap-2'>
        <Skeleton className='h-8 w-48 rounded-md' />
        <Skeleton className='h-8 w-20 rounded-full' />
        <Skeleton className='h-8 w-20 rounded-full' />
        <Skeleton className='h-8 w-20 rounded-full' />
        <Skeleton className='ml-auto h-8 w-24 rounded-md' />
      </div>

      {/* Table skeleton */}
      <div className='overflow-hidden rounded-md border border-border'>
        {/* Header */}
        <div className='flex items-center gap-4 bg-muted/40 px-3 py-2.5'>
          <Skeleton className='h-4 w-4 rounded' />
          <Skeleton className='h-3 w-32 rounded' />
          <Skeleton className='ml-auto h-3 w-16 rounded' />
          <Skeleton className='h-3 w-16 rounded' />
          <Skeleton className='h-3 w-20 rounded' />
          <Skeleton className='h-3 w-14 rounded' />
          <Skeleton className='h-3 w-14 rounded' />
        </div>
        {/* Rows */}
        {Array.from({ length: 8 }).map((_, i) => (
          <div
            key={i}
            className='flex items-center gap-4 border-t border-border/60 px-3 py-3'
          >
            <Skeleton className='h-4 w-4 rounded' />
            <div className='flex flex-col gap-1 flex-1'>
              <Skeleton className='h-3.5 w-48 rounded' />
              <Skeleton className='h-3 w-64 rounded' />
            </div>
            <Skeleton className='h-6 w-20 rounded-full' />
            <Skeleton className='h-5 w-14 rounded' />
            <Skeleton className='h-3 w-24 rounded' />
            <Skeleton className='h-3 w-10 rounded' />
            <Skeleton className='h-3 w-20 rounded' />
            <Skeleton className='h-6 w-6 rounded' />
          </div>
        ))}
      </div>

      {/* Pagination skeleton */}
      <div className='flex items-center justify-between px-1'>
        <Skeleton className='h-7 w-32 rounded-md' />
        <div className='flex items-center gap-1'>
          <Skeleton className='h-7 w-7 rounded' />
          <Skeleton className='h-7 w-7 rounded' />
          <Skeleton className='h-7 w-7 rounded' />
          <Skeleton className='h-7 w-7 rounded' />
          <Skeleton className='h-7 w-7 rounded' />
        </div>
      </div>
    </div>
  )
}