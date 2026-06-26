import { createFileRoute } from '@tanstack/react-router'
import { Music } from '@/features/music' 

export const Route = createFileRoute('/_authenticated/music/')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    
  <div className="flex flex-col items-center justify-center py-16">
  <Music />
  </div>
  )
}
