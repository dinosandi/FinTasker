'use client'

import { useState } from 'react'
import { Button } from '@/components/ui/button'
import { Plus } from 'lucide-react'
import { TasksAddForm } from './tasks-add-form'

export function TasksPrimaryButtons() {
  const [addOpen, setAddOpen] = useState(false)

  return (
    <>
      <div className='flex items-center gap-2'>
     
        <Button
          size='sm'
          className='space-x-1 bg-[#ffd500] hover:bg-[#e6bf00] text-black'
          onClick={() => setAddOpen(true)}
        >
          <Plus size={18} />
          Task
        </Button>
      </div>

      <TasksAddForm open={addOpen} onOpenChange={setAddOpen} />
    </>
  )
}