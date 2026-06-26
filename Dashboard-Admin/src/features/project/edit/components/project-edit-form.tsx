'use client'

import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'
import { format } from 'date-fns'
import { CalendarIcon, Loader2 } from 'lucide-react'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Calendar } from '@/components/ui/calendar'
import { Separator } from '@/components/ui/separator'
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { cn } from '@/lib/utils'

import { statusField } from '../../data/data' 
import { ColorPicker } from './color-picker' 
import { ProjectKeyBadge } from './project-key-badge' 
import type { Project } from '../../data/schema'
import type { UpdateProjectRequest } from '@/hooks/useMutation/Projects/usePutProject' 

const editProjectFormSchema = z
  .object({
    name: z
      .string()
      .min(2, 'Name must be at least 2 characters')
      .max(80, 'Name must be 80 characters or less'),
    description: z
      .string()
      .max(350, 'Description cannot exceed 350 characters')
      .optional(),
    status: z.string().nonempty('Status is required'),
    color: z.string().min(1, 'Select a project color'),
    startDate: z.string().min(1, 'Start date is required'),
    endDate: z.string().min(1, 'End date is required'),
  })
  .refine(
    (data) => {
      if (!data.startDate || !data.endDate) return true
      return new Date(data.startDate) <= new Date(data.endDate)
    },
    { message: 'End date must be after start date', path: ['endDate'] }
  )

type EditProjectFormValues = z.infer<typeof editProjectFormSchema>

interface EditProjectFormProps {
  project: Project
  onSubmit: (data: UpdateProjectRequest) => void
  isPending: boolean
  onCancel: () => void
}

export function EditProjectForm({
  project,
  onSubmit,
  isPending,
  onCancel,
}: EditProjectFormProps) {
  const form = useForm<EditProjectFormValues>({
    resolver: zodResolver(editProjectFormSchema),
    defaultValues: {
      name:        project.name        ?? '',
      description: project.description ?? '',
      status:      project.status      ?? '0',
      color:       project.color       ?? '#0052CC',
      startDate:   project.startDate   ?? '',
      endDate:     project.endDate     ?? '',
    },
  })

  const watchedName   = form.watch('name')
  const watchedColor  = form.watch('color')
  const watchedStatus = form.watch('status')

  const currentStatus = statusField.find((s) => s.value === watchedStatus)

  function handleSubmit(values: EditProjectFormValues) {
    onSubmit({
      id:          project.id,
      name:        values.name,
      description: values.description ?? '',
      status:      Number(values.status),
      color:       values.color,
      startDate:   values.startDate,
      endDate:     values.endDate,
    })
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(handleSubmit)} className='space-y-0'>

        <div className='mb-6 flex items-center gap-3 rounded-md border border-border bg-muted/40 px-4 py-3'>
          <ProjectKeyBadge
            projectKey={watchedName}
            color={watchedColor}
          />
          <div className='min-w-0 flex-1'>
            <p className='truncate text-sm font-semibold leading-tight text-foreground'>
              {watchedName || 'Project name'}
            </p>
          </div>

          {currentStatus && (
            <span
              className='shrink-0 inline-flex items-center gap-1.5 rounded-full px-2.5 py-1 text-xs font-semibold'
              style={{
                color:           currentStatus.color,
                backgroundColor: currentStatus.bgColor,
              }}
            >
              <currentStatus.icon size={12} />
              {currentStatus.label}
            </span>
          )}
        </div>

        <SectionLabel text='Details' />

        <div className='space-y-5 py-4'>

          <FormField
            control={form.control}
            name='name'
            render={({ field }) => (
              <FormItem>
                <FormLabel>
                  Name <span className='text-destructive'>*</span>
                </FormLabel>
                <FormControl>
                  <Input placeholder='e.g. FinTasker Web App' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name='description'
            render={({ field }) => (
              <FormItem>
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea
                    placeholder='Add a short description of what this project is about…'
                    className='min-h-[90px] resize-none'
                    {...field}
                  />
                </FormControl>
                <div className='flex items-start justify-between'>
                  <FormMessage />
                  <span className='ml-auto text-xs text-muted-foreground'>
                    {field.value?.length ?? 0}/350
                  </span>
                </div>
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name='status'
            render={({ field }) => (
              <FormItem>
                <FormLabel>
                  Status <span className='text-destructive'>*</span>
                </FormLabel>
                <Select onValueChange={field.onChange} defaultValue={field.value}>
                  <FormControl>
                    <SelectTrigger>
                      <SelectValue placeholder='Select status' />
                    </SelectTrigger>
                  </FormControl>
                  <SelectContent>
                    {statusField.map((s) => (
                      <SelectItem key={s.value} value={s.value}>
                        <span className='flex items-center gap-2'>
                          <span
                            className='inline-flex h-5 w-5 items-center justify-center rounded-full'
                            style={{ backgroundColor: s.bgColor }}
                          >
                            <s.icon size={11} color={s.color} />
                          </span>
                          {s.label}
                        </span>
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />

          <div className='grid grid-cols-2 gap-4'>
            <FormField
              control={form.control}
              name='startDate'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>
                    Start date <span className='text-destructive'>*</span>
                  </FormLabel>
                  <Popover>
                    <PopoverTrigger asChild>
                      <FormControl>
                        <Button
                          variant='outline'
                          className={cn(
                            'w-full justify-start text-left font-normal',
                            !field.value && 'text-muted-foreground'
                          )}
                        >
                          <CalendarIcon className='mr-2 h-4 w-4 shrink-0' />
                          {field.value
                            ? format(new Date(field.value), 'MMM d, yyyy')
                            : 'Pick a date'}
                        </Button>
                      </FormControl>
                    </PopoverTrigger>
                    <PopoverContent className='w-auto p-0' align='start'>
                      <Calendar
                        mode='single'
                        selected={field.value ? new Date(field.value) : undefined}
                        onSelect={(date) =>
                          field.onChange(date ? date.toISOString() : '')
                        }
                        initialFocus
                      />
                    </PopoverContent>
                  </Popover>
                  <FormMessage />
                </FormItem>
              )}
            />

            <FormField
              control={form.control}
              name='endDate'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>
                    End date <span className='text-destructive'>*</span>
                  </FormLabel>
                  <Popover>
                    <PopoverTrigger asChild>
                      <FormControl>
                        <Button
                          variant='outline'
                          className={cn(
                            'w-full justify-start text-left font-normal',
                            !field.value && 'text-muted-foreground'
                          )}
                        >
                          <CalendarIcon className='mr-2 h-4 w-4 shrink-0' />
                          {field.value
                            ? format(new Date(field.value), 'MMM d, yyyy')
                            : 'Pick a date'}
                        </Button>
                      </FormControl>
                    </PopoverTrigger>
                    <PopoverContent className='w-auto p-0' align='start'>
                      <Calendar
                        mode='single'
                        selected={field.value ? new Date(field.value) : undefined}
                        onSelect={(date) =>
                          field.onChange(date ? date.toISOString() : '')
                        }
                        disabled={(date) => {
                          const start = form.getValues('startDate')
                          return start ? date < new Date(start) : false
                        }}
                        initialFocus
                      />
                    </PopoverContent>
                  </Popover>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
        </div>

        <Separator />

        <SectionLabel text='Appearance' className='mt-5' />

        <div className='py-4'>
          <FormField
            control={form.control}
            name='color'
            render={({ field }) => (
              <FormItem>
                <FormLabel>Project color</FormLabel>
                <FormDescription className='text-xs'>
                  Shown on project avatars and labels across the board.
                </FormDescription>
                <FormControl>
                  <ColorPicker value={field.value} onChange={field.onChange} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <Separator />

        <div className='flex justify-end gap-2 pt-6'>
          <Button
            type='button'
            variant='ghost'
            onClick={onCancel}
            disabled={isPending}
          >
            Cancel
          </Button>
          <Button
            type='submit'
            disabled={isPending}
            className='min-w-[110px] bg-[#ffd500] hover:bg-[#e6bf00] text-black'
          >
            {isPending ? (
              <>
                <Loader2 className='mr-2 h-4 w-4 animate-spin' />
                Saving…
              </>
            ) : (
              'Save changes'
            )}
          </Button>
        </div>
      </form>
    </Form>
  )
}

function SectionLabel({ text, className }: { text: string; className?: string }) {
  return (
    <p
      className={cn(
        'text-[11px] font-semibold uppercase tracking-widest text-muted-foreground',
        className
      )}
    >
      {text}
    </p>
  )
}