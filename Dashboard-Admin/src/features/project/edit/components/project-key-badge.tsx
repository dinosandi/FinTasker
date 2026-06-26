import { cn } from '@/lib/utils'

interface ProjectKeyBadgeProps {
  projectKey: string
  color: string
  className?: string
}

export function ProjectKeyBadge({ projectKey, color, className }: ProjectKeyBadgeProps) {
  return (
    <div
      className={cn(
        'inline-flex h-8 w-8 items-center justify-center rounded text-xs font-bold text-white shadow-sm',
        className
      )}
      style={{ backgroundColor: color || '#0052CC' }}
    >
      {projectKey?.slice(0, 2).toUpperCase() || 'PR'}
    </div>
  )
}