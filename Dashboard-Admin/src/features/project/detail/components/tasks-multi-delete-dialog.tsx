'use client'

import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from '@/components/ui/alert-dialog'
import { useTasks } from './tasks-provider'

export function TasksMultiDeleteDialog() {
  const { open, setOpen, selectedTasks, setSelectedTasks } = useTasks()

  return (
    <AlertDialog
      open={open === 'bulk-delete'}
      onOpenChange={(v) => {
        if (!v) {
          setOpen(null)
          setSelectedTasks([])
        }
      }}
    >
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Delete {selectedTasks.length} tasks?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. The selected tasks will be permanently deleted.
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <AlertDialogAction className='bg-destructive text-destructive-foreground hover:bg-destructive/90'>
            Delete tasks
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}