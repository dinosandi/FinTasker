'use client'

import { Row } from '@tanstack/react-table'
import { MoreHorizontal, Pencil, Trash2, Eye } from 'lucide-react'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { Task } from '../data/shema' 
import { useTasks } from './tasks-provider'

interface DataTableRowActionsProps {
  row: Row<Task>
}

export function DataTableRowActions({ row }: DataTableRowActionsProps) {
  const { setOpen, setCurrentTask } = useTasks()

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button
          variant='ghost'
          className='flex h-7 w-7 p-0 data-[state=open]:bg-muted'
          onClick={(e) => e.stopPropagation()}
        >
          <MoreHorizontal size={15} />

        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent align='end' className='w-[160px]'>
        <DropdownMenuItem
          className='gap-2 text-sm'
          onClick={(e) => {
            e.stopPropagation()
            setCurrentTask(row.original)
            setOpen('edit')
          }}
        >
          <Eye size={14} className='text-muted-foreground' />
          View details
        </DropdownMenuItem>
        <DropdownMenuItem
          className='gap-2 text-sm'
          onClick={(e) => {
            e.stopPropagation()
            setCurrentTask(row.original)
            setOpen('edit')
          }}
        >
          <Pencil size={14} className='text-muted-foreground' />
          Edit task
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem
          className='gap-2 text-sm text-destructive focus:text-destructive'
          onClick={(e) => {
            e.stopPropagation()
            setCurrentTask(row.original)
            setOpen('delete')
          }}
        >
          <Trash2 size={14} />
          Delete task
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  )
}