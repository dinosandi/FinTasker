'use client'

import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
} from '@/components/ui/sheet'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Textarea } from '@/components/ui/textarea'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { useTasks } from './tasks-provider'
import { TASK_STATUS, TASK_PRIORITY } from '../data/data'

export function TasksMutateDrawer() {
  const { open, setOpen, currentTask } = useTasks()
  const isEdit = open === 'edit'
  const isOpen = open === 'create' || open === 'edit'

  return (
    <Sheet open={isOpen} onOpenChange={(v) => !v && setOpen(null)}>
      <SheetContent className='sm:max-w-[480px] overflow-y-auto'>
        <SheetHeader className='mb-6'>
          <SheetTitle>{isEdit ? 'Edit Task' : 'Create Task'}</SheetTitle>
          <SheetDescription>
            {isEdit
              ? 'Update the task details below.'
              : 'Fill in the details to create a new task.'}
          </SheetDescription>
        </SheetHeader>

        <div className='flex flex-col gap-5'>
          <div className='flex flex-col gap-1.5'>
            <Label htmlFor='title'>Title <span className='text-destructive'>*</span></Label>
            <Input
              id='title'
              placeholder='Task title'
              defaultValue={currentTask?.title}
              className='h-9'
            />
          </div>

          <div className='flex flex-col gap-1.5'>
            <Label htmlFor='description'>Description</Label>
            <Textarea
              id='description'
              placeholder='Describe the task…'
              defaultValue={currentTask?.description}
              rows={3}
              className='resize-none text-sm'
            />
          </div>

          <div className='grid grid-cols-2 gap-4'>
            <div className='flex flex-col gap-1.5'>
              <Label>Status</Label>
              <Select defaultValue={currentTask?.status ?? 'ToDo'}>
                <SelectTrigger className='h-9 text-sm'>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  {TASK_STATUS.map((s) => (
                    <SelectItem key={s.value} value={s.value} className='text-sm'>
                      {s.label}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>

            <div className='flex flex-col gap-1.5'>
              <Label>Priority</Label>
              <Select defaultValue={currentTask?.priority ?? 'Medium'}>
                <SelectTrigger className='h-9 text-sm'>
                  <SelectValue />
                </SelectTrigger>
                <SelectContent>
                  {TASK_PRIORITY.map((p) => (
                    <SelectItem key={p.value} value={p.value} className='text-sm'>
                      {p.label}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
          </div>

          <div className='grid grid-cols-2 gap-4'>
            <div className='flex flex-col gap-1.5'>
              <Label htmlFor='dueDate'>Due Date</Label>
              <Input
                id='dueDate'
                type='date'
                defaultValue={currentTask?.dueDate?.slice(0, 10)}
                className='h-9 text-sm'
              />
            </div>

            <div className='flex flex-col gap-1.5'>
              <Label htmlFor='estimate'>Estimate (minutes)</Label>
              <Input
                id='estimate'
                type='number'
                placeholder='e.g. 60'
                defaultValue={currentTask?.estimatedMinutes}
                className='h-9 text-sm'
              />
            </div>
          </div>

          <div className='flex justify-end gap-2 pt-2'>
            <Button variant='outline' size='sm' onClick={() => setOpen(null)}>
              Cancel
            </Button>
            <Button size='sm'>{isEdit ? 'Save changes' : 'Create task'}</Button>
          </div>
        </div>
      </SheetContent>
    </Sheet>
  )
}