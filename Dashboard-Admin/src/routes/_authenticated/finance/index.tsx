import { createFileRoute } from '@tanstack/react-router'
import { Finance } from '@/features/finance'

export const Route = createFileRoute('/_authenticated/finance/')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    
  <div className="flex flex-col items-center justify-center py-16">
  <Finance />
  </div>
  )
}
