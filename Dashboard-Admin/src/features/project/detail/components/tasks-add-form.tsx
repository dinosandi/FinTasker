'use client'

import { useState } from 'react'
import { useForm, Controller } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useParams } from '@tanstack/react-router'
import { TaskStatus, TaskPriority } from '@/Type'
import { Loader2, ListPlus } from 'lucide-react'
import { toast } from 'sonner'
import ClearChange from '@/assets/image/ClearChange.svg'
import { cn } from '@/lib/utils'
import { usePostTask } from '@/hooks/useMutation/Tasks/usePostTasks'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Separator } from '@/components/ui/separator'
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
  SheetDescription,
  SheetFooter,
  SheetClose,
} from '@/components/ui/sheet'
import { Textarea } from '@/components/ui/textarea'
import { ConfirmDialog } from '@/components/confirm-dialog'
import { TASK_STATUS, TASK_PRIORITY } from '../data/data'
import {
  taskFormSchema,
  TaskFormValues,
  TASK_FORM_DEFAULTS,
} from '../data/shema'

const STATUS_TO_ENUM: Record<TaskFormValues['status'], number> = {
  ToDo: TaskStatus.ToDo,
  InProgress: TaskStatus.InProgress,
  Review: TaskStatus.Review,
  Completed: TaskStatus.Completed,
  Cancelled: TaskStatus.Cancelled,
}

const PRIORITY_TO_ENUM: Record<TaskFormValues['priority'], number> = {
  Low: TaskPriority.Low,
  Medium: TaskPriority.Medium,
  High: TaskPriority.High,
  Critical: TaskPriority.Critical,
}

interface TasksAddFormProps {
  open: boolean
  onOpenChange: (open: boolean) => void
  onSuccess?: () => void
}

export function TasksAddForm({
  open,
  onOpenChange,
  onSuccess,
}: TasksAddFormProps) {
  const { projectId } = useParams({
    from: '/_authenticated/projects/$projectId/',
  })
  const { mutate, isPending } = usePostTask()
  const [showDiscardDialog, setShowDiscardDialog] = useState(false)

  const {
    control,
    register,
    handleSubmit,
    reset,
    watch,
    formState: { errors, isDirty },
  } = useForm<TaskFormValues>({
    resolver: zodResolver(taskFormSchema),
    defaultValues: TASK_FORM_DEFAULTS,
    mode: 'onBlur',
  })

  const watchedStatus = watch('status')
  const watchedPriority = watch('priority')
  const watchedTitle = watch('title')

  const handleClose = (next: boolean) => {
    if (next) {
      onOpenChange(true)
      return
    }

    if (isDirty && !isPending) {
      setShowDiscardDialog(true)
      return
    }

    reset(TASK_FORM_DEFAULTS)
    onOpenChange(false)
  }
  const handleDiscard = () => {
    reset(TASK_FORM_DEFAULTS)
    setShowDiscardDialog(false)
    onOpenChange(false)
  }

const onSubmit = (values: TaskFormValues) => {
  mutate(
    {
      projectId,
      title: values.title.trim(),
      description: values.description?.trim() ?? '',
      status: STATUS_TO_ENUM[values.status],
      priority: PRIORITY_TO_ENUM[values.priority],
      dueDate: values.dueDate
        ? new Date(values.dueDate).toISOString()
        : '',
      completedAt: '',
      estimed_Minutes: values.estimatedMinutes ?? 0,
    },
    {
      onSuccess: () => {
        toast.success('Task created', {
          description: `"${values.title}" has been added to the project.`,
        })

        reset(TASK_FORM_DEFAULTS)
        onOpenChange(false)
        onSuccess?.()
      },

      onError: (error) => {
        const message =
          error.response?.data?.errors?.[0] ??
          error.response?.data?.message ??
          error.message ??
          'Failed to create task.'

        toast.error('Failed to create task', {
          description: message,
        })
      },
    }
  )
}

  const statusMeta = TASK_STATUS.find((s) => s.value === watchedStatus)
  const priorityMeta = TASK_PRIORITY.find((p) => p.value === watchedPriority)

  return (
    <Sheet open={open} onOpenChange={handleClose}>
      <ConfirmDialog
        open={showDiscardDialog}
        onOpenChange={setShowDiscardDialog}
        title='Discard changes?'
        desc='You have unsaved changes. If you leave now, your changes will be lost.'
        cancelBtnText='Keep editing'
        confirmText='Discard'
        handleConfirm={handleDiscard}
        destructive
      >
        <div className='flex justify-center py-2'>
          <img
            src={ClearChange}
            alt='Discard changes'
            className='h-40 w-auto'
          />
        </div>
      </ConfirmDialog>
      <SheetContent
        side='right'
        className='flex w-full flex-col gap-0 p-0 sm:max-w-[560px]'
      >
        {/* Header */}
        <SheetHeader className='gap-1.5 border-b border-border px-6 py-4'>
          <div className='flex items-center gap-2'>
            <div className='flex h-7 w-7 items-center justify-center rounded bg-primary/10'>
              <ListPlus size={15} className='text-primary' />
            </div>
            <SheetTitle className='text-base'>Create Task</SheetTitle>
          </div>
          <SheetDescription className='text-xs'>
            Add a new task to this project. Fields marked with{' '}
            <span className='text-destructive'>*</span> are required.
          </SheetDescription>
        </SheetHeader>

        {/* Body */}
        <form
          onSubmit={handleSubmit(onSubmit)}
          className='flex flex-1 flex-col overflow-hidden'
        >
          <div className='flex-1 space-y-6 overflow-y-auto px-6 py-5'>
            {/* Title */}
            <div className='flex flex-col gap-1.5'>
              <Label
                htmlFor='title'
                className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'
              >
                Title <span className='text-destructive'>*</span>
              </Label>
              <Input
                id='title'
                placeholder='e.g. Implement login page validation'
                className={cn(
                  'h-10 text-sm',
                  errors.title &&
                    'border-destructive focus-visible:ring-destructive/30'
                )}
                {...register('title')}
                autoFocus
              />
              <div className='flex items-center justify-between'>
                {errors.title ? (
                  <p className='text-xs text-destructive'>
                    {errors.title.message}
                  </p>
                ) : (
                  <span />
                )}
                <span className='text-[11px] text-muted-foreground tabular-nums'>
                  {watchedTitle?.length ?? 0}/200
                </span>
              </div>
            </div>

            {/* Description */}
            <div className='flex flex-col gap-1.5'>
              <Label
                htmlFor='description'
                className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'
              >
                Description
              </Label>
              <Textarea
                id='description'
                placeholder='Add more context, acceptance criteria, or notes…'
                rows={5}
                maxLength={350}
                className={cn(
                  'resize-y text-sm',
                  errors.description &&
                    'border-destructive focus-visible:ring-destructive/30'
                )}
                {...register('description')}
              />
              {errors.description && (
                <p className='text-xs text-destructive'>
                  {errors.description.message}
                </p>
              )}
            </div>

            <Separator />

            {/* Status & Priority */}
            <div className='grid grid-cols-2 gap-4'>
              <div className='flex flex-col gap-1.5'>
                <Label className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'>
                  Status <span className='text-destructive'>*</span>
                </Label>
                <Controller
                  control={control}
                  name='status'
                  render={({ field }) => (
                    <Select value={field.value} onValueChange={field.onChange}>
                      <SelectTrigger className='h-10 text-sm'>
                        <SelectValue>
                          {statusMeta && (
                            <span className='inline-flex items-center gap-1.5'>
                              <statusMeta.icon
                                size={13}
                                style={{ color: statusMeta.color }}
                              />
                              {statusMeta.label}
                            </span>
                          )}
                        </SelectValue>
                      </SelectTrigger>
                      <SelectContent>
                        {TASK_STATUS.map((s) => (
                          <SelectItem
                            key={s.value}
                            value={s.value}
                            className='text-sm'
                          >
                            <span className='inline-flex items-center gap-1.5'>
                              <s.icon size={13} style={{ color: s.color }} />
                              {s.label}
                            </span>
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  )}
                />
                {errors.status && (
                  <p className='text-xs text-destructive'>
                    {errors.status.message}
                  </p>
                )}
              </div>

              <div className='flex flex-col gap-1.5'>
                <Label className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'>
                  Priority <span className='text-destructive'>*</span>
                </Label>
                <Controller
                  control={control}
                  name='priority'
                  render={({ field }) => (
                    <Select value={field.value} onValueChange={field.onChange}>
                      <SelectTrigger className='h-10 text-sm'>
                        <SelectValue>
                          {priorityMeta && (
                            <span className='inline-flex items-center gap-1.5'>
                              <priorityMeta.icon
                                size={13}
                                style={{ color: priorityMeta.color }}
                              />
                              {priorityMeta.label}
                            </span>
                          )}
                        </SelectValue>
                      </SelectTrigger>
                      <SelectContent>
                        {TASK_PRIORITY.map((p) => (
                          <SelectItem
                            key={p.value}
                            value={p.value}
                            className='text-sm'
                          >
                            <span className='inline-flex items-center gap-1.5'>
                              <p.icon size={13} style={{ color: p.color }} />
                              {p.label}
                            </span>
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  )}
                />
                {errors.priority && (
                  <p className='text-xs text-destructive'>
                    {errors.priority.message}
                  </p>
                )}
              </div>
            </div>

            {/* Due date & Estimate */}
            <div className='grid grid-cols-2 gap-4'>
              <div className='flex flex-col gap-1.5'>
                <Label
                  htmlFor='dueDate'
                  className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'
                >
                  Due Date <span className='text-destructive'>*</span>
                </Label>
                <Input
                  id='dueDate'
                  type='date'
                  className={cn(
                    'h-10 text-sm',
                    errors.dueDate &&
                      'border-destructive focus-visible:ring-destructive/30'
                  )}
                  {...register('dueDate')}
                />
                {errors.dueDate && (
                  <p className='text-xs text-destructive'>
                    {errors.dueDate.message}
                  </p>
                )}
              </div>

              <div className='flex flex-col gap-1.5'>
                <Label
                  htmlFor='estimatedMinutes'
                  className='text-xs font-semibold tracking-wide text-muted-foreground uppercase'
                >
                  Estimate (minutes)
                </Label>
                <Input
                  id='estimatedMinutes'
                  type='number'
                  min={0}
                  placeholder='e.g. 60'
                  className={cn(
                    'h-10 text-sm',
                    errors.estimatedMinutes &&
                      'border-destructive focus-visible:ring-destructive/30'
                  )}
                  {...register('estimatedMinutes', { valueAsNumber: true })}
                />
                {errors.estimatedMinutes && (
                  <p className='text-xs text-destructive'>
                    {errors.estimatedMinutes.message}
                  </p>
                )}
              </div>
            </div>
          </div>

          {/* Footer */}
          <SheetFooter className='flex-row items-center justify-end gap-2 border-t border-border px-6 py-4'>
            <SheetClose asChild>
              <Button
                type='button'
                variant='outline'
                size='sm'
                disabled={isPending}
                onClick={() => handleClose(false)}
              >
                Cancel
              </Button>
            </SheetClose>
            <Button
              type='submit'
              size='sm'
              disabled={isPending}
              className='gap-1.5 bg-[#FFD500] text-black hover:bg-[#FFD500]/90 disabled:bg-[#FFD500]/50 disabled:text-black/50'
            >
              {isPending ? (
                <>
                  <Loader2 size={14} className='animate-spin' />
                  Creating…
                </>
              ) : (
                'Create task'
              )}
            </Button>
          </SheetFooter>
        </form>
      </SheetContent>
    </Sheet>
  )
}
