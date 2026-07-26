'use client'

import { Row } from '@tanstack/react-table'
import { MoreHorizontal, Trash2, Eye, SquarePen } from 'lucide-react'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuShortcut,
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
      <DropdownMenuContent align='end' className='w-44'>
        <DropdownMenuItem
          className='gap-2 text-sm'
          onClick={(e) => {
            e.stopPropagation()
            setCurrentTask(row.original)
            setOpen('edit')
          }}
        >

          Details
          <DropdownMenuShortcut>
            <Eye size={16} />
          </DropdownMenuShortcut>
        </DropdownMenuItem>
        <DropdownMenuItem
          className='gap-2 text-sm'
          onClick={(e) => {
            e.stopPropagation()
            setCurrentTask(row.original)
            setOpen('edit')
          }}
        >
          Edit
          <DropdownMenuShortcut>
            <SquarePen size={16} />
          </DropdownMenuShortcut>
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
          Delete
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  )
}