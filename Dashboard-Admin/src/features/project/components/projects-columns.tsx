import { type ColumnDef } from '@tanstack/react-table'
import { DataTableColumnHeader } from '@/components/data-table'
import {statuses } from '../data/data'
import { type Project } from '../data/schema'
import { DataTableRowActions } from './data-table-row-actions'
import { CalendarDays } from 'lucide-react'
import  { DescriptionCell } from './short-description'

export const projectsColumns: ColumnDef<Project>[] = [
  
  {
    accessorKey: 'name',
    header: ({ column }) => (
      <DataTableColumnHeader
        column={column}
        title='Project'
      />
    ),
    meta: {
      className: 'w-[260px]',
    },
    cell: ({ row }) => {
      const color = row.original.color
  
      return (
        <div className='flex items-center gap-3'>
          <div
            className='h-3 w-3 rounded-full border border-border'
            style={{
              backgroundColor: color,
            }}
          />
  
          <span
            className='truncate font-medium'
            title={row.getValue('name')}
          >
            {row.getValue('name')}
          </span>
        </div>
      )
    },
  },
  {
    accessorKey: 'description',
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title='Description' />
    ),
    cell: ({ row }) => (
      <DescriptionCell
        description={row.getValue('description')}
      />
    ),
    meta: {
      className: 'min-w-[300px]',
      tdClassName: 'ps-4',
    }, 
  },
  
  {
    accessorKey: 'startDate',
    header: ({ column }) => (
      <DataTableColumnHeader
        column={column}
        title='Timeline'
      />
    ),
    meta: {
      className: 'w-[240px]',
    },
    cell: ({ row }) => {
      const start = new Date(
        row.original.startDate
      ).toLocaleDateString('id-ID', {
        day: '2-digit',
        month: 'short',
      })
    
      const end = new Date(
        row.original.endDate
      ).toLocaleDateString('id-ID', {
        day: '2-digit',
        month: 'short',
        year: 'numeric',
      })
    
      return (
        <div className='flex items-center gap-2 text-sm'>
          <CalendarDays className='size-4 text-muted-foreground' />
    
          <span>
            {start} - {end}
          </span>
        </div>
      )
    },
  },
  {
    accessorKey: 'status',
    header: ({ column }) => (
      <DataTableColumnHeader
        column={column}
        title='Status'
      />
    ),
    meta: {
      className: 'w-[160px]',
    },
    cell: ({ row }) => {
      const status = statuses.find(
        (status) => status.value === row.getValue('status')
      )
  
      if (!status) return null
  
      const Icon = status.icon
  
      return (
        <div
          className='inline-flex items-center gap-2 rounded-full px-3 py-1 text-xs font-semibold'
          style={{
            backgroundColor: status.bgColor,
            color: status.color,
          }}
        >
          <Icon className='size-3.5' />
          <span>{status.label}</span>
        </div>
      )
    },
    filterFn: (row, id, value) => {
      return value.includes(row.getValue(id))
    },
  },
  {
    id: 'actions',
    cell: ({ row }) => <DataTableRowActions row={row} />,
  },
]
