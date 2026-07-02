'use client'

import { format } from 'date-fns'
import { ColumnDef } from '@tanstack/react-table'
import { AlertTriangle, CalendarClock } from 'lucide-react'
import { cn } from '@/lib/utils'
import { Checkbox } from '@/components/ui/checkbox'
import { TASK_STATUS, TASK_PRIORITY } from '../data/data'
import { Task } from '../data/shema'
import { DataTableRowActions } from './data-table-row-actions'

function StatusBadge({ status }: { status: string }) {
  const found = TASK_STATUS.find((s) => s.value === status)
  if (!found)
    return <span className='text-xs text-muted-foreground'>{status}</span>
  const Icon = found.icon
  return (
    <span
      className='inline-flex items-center gap-1.5 rounded-full px-2.5 py-1 text-xs font-medium'
      style={{ color: found.color, backgroundColor: found.bgColor }}
    >
      <Icon size={12} strokeWidth={2.5} />
      {found.label}
    </span>
  )
}

function PriorityBadge({ priority }: { priority?: string }) {
  if (!priority) return null
  const found = TASK_PRIORITY.find((p) => p.value === priority)
  if (!found)
    return <span className='text-xs text-muted-foreground'>{priority}</span>
  const Icon = found.icon
  return (
    <span
      className='inline-flex items-center gap-1 rounded px-2 py-0.5 text-xs font-medium'
      style={{ color: found.color, backgroundColor: found.bgColor }}
    >
      <Icon size={11} strokeWidth={2.5} />
      {found.label}
    </span>
  )
}

function DueDateCell({ dueDate, status }: { dueDate: string; status: string }) {
  if (!dueDate) return <span className='text-xs text-muted-foreground'>—</span>

  const due = new Date(dueDate)
  const now = new Date()
  const isOverdue =
    due < now && status !== 'Completed' && status !== 'Cancelled'
  const isDueSoon =
    !isOverdue &&
    due.getTime() - now.getTime() < 2 * 24 * 60 * 60 * 1000 &&
    status !== 'Completed' &&
    status !== 'Cancelled'

  return (
    <span
      className={cn(
        'inline-flex items-center gap-1 text-xs',
        isOverdue && 'font-semibold text-red-600',
        isDueSoon && 'font-medium text-amber-600',
        !isOverdue && !isDueSoon && 'text-muted-foreground'
      )}
    >
      {(isOverdue || isDueSoon) && <AlertTriangle size={11} />}
      {format(due, 'dd MMM yyyy')}
    </span>
  )
}

export const tasksColumns: ColumnDef<Task>[] = [
  {
    id: 'select',
    header: ({ table }) => (
      <Checkbox
        checked={
          table.getIsAllPageRowsSelected() ||
          (table.getIsSomePageRowsSelected() && 'indeterminate')
        }
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label='Select all'
        className='translate-y-[1px]'
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label='Select row'
        className='translate-y-[1px]'
        onClick={(e) => e.stopPropagation()}
      />
    ),
    enableSorting: false,
    enableHiding: false,
    size: 40,
  },
  {
    accessorKey: 'title',
    header: 'Task',
    cell: ({ row }) => (
      <div className='flex max-w-[320px] flex-col gap-0.5'>
        <span className='truncate text-sm leading-snug font-medium text-foreground'>
          {row.getValue('title')}
        </span>
      </div>
    ),
    enableSorting: true,
  },
  {
    accessorKey: 'description',
    header: 'Description',
    cell: ({ row }) => (
      <span className='truncate text-xs leading-snug text-muted-foreground'>
        {row.getValue('description')}
      </span>
    ),
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => <StatusBadge status={row.getValue('status')} />,
    enableSorting: true,
    filterFn: (row, id, value) => value.includes(row.getValue(id)),
  },
  {
    accessorKey: 'priority',
    header: 'Priority',
    cell: ({ row }) => <PriorityBadge priority={row.original.priority} />,
    enableSorting: true,
  },
  {
    accessorKey: 'dueDate',
    header: () => (
      <span className='inline-flex items-center gap-1.5'>
        <CalendarClock size={13} className='text-muted-foreground' />
        Due Date
      </span>
    ),
    cell: ({ row }) => (
      <DueDateCell
        dueDate={row.getValue('dueDate')}
        status={row.getValue('status')}
      />
    ),
    enableSorting: true,
  },
  {
    accessorKey: 'estimatedMinutes',
    header: 'Estimate',
    cell: ({ row }) => {
      const mins: number = row.getValue('estimatedMinutes')
      if (!mins) return <span className='text-xs text-muted-foreground'>—</span>
      const h = Math.floor(mins / 60)
      const m = mins % 60
      return (
        <span className='text-xs text-muted-foreground tabular-nums'>
          {h > 0 ? `${h}h ` : ''}
          {m > 0 ? `${m}m` : ''}
        </span>
      )
    },
    enableSorting: true,
  },
  {
    accessorKey: 'createdAt',
    header: 'Created',
    cell: ({ row }) => {
      const date = row.getValue('createdAt') as string
      return (
        <span className='text-xs text-muted-foreground tabular-nums'>
          {date ? format(new Date(date), 'dd MMM yyyy') : '—'}
        </span>
      )
    },
    enableSorting: true,
  },
  {
    accessorKey: 'completedAt',
    header: 'Completed',
    cell: ({ row }) => {
      const date = row.getValue('completedAt') as string
      return (
        <span className='text-xs text-muted-foreground tabular-nums'>
          {date ? format(new Date(date), 'dd MMM yyyy') : '—'}
        </span>
      )
    },
    enableSorting: true,
  },
  {
    id: 'actions',
    cell: ({ row }) => <DataTableRowActions row={row} />,
    size: 40,
  },
]
