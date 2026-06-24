import {  Plus } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { useProjects } from './projects-provider'

export function ProjectsPrimaryButtons() {
  const { setOpen } = useProjects()
  return (
    <div className='flex gap-2'>
      <Button className='space-x-1 bg-[#ffd500] hover:bg-[#e6bf00] text-black' onClick={() => setOpen('create')}>
        <span>Create</span> <Plus size={18} />
      </Button>
    </div>
  )
}
