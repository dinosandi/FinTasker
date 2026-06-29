'use client'

import { Button } from '@/components/ui/button'
import { Plus, Upload } from 'lucide-react'
import { useTasks } from './tasks-provider'

export function TasksPrimaryButtons() {
  const { setOpen } = useTasks()

  return (
    <div className='flex items-center gap-2'>
      <Button
        variant='outline'
        size='sm'
        className='h-8 gap-1.5 text-xs'
        onClick={() => setOpen('import')}
      >
        <Upload size={13} />
        Import
      </Button>
      <Button
        size='sm'
        className='h-8 gap-1.5 text-xs'
        onClick={() => setOpen('create')}
      >
        <Plus size={13} />
        Create Task
      </Button>
    </div>
  )
}