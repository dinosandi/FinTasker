// src/features/project/detail/components/tasks-delete-dialog.tsx
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
import { useDeleteTask } from '@/hooks/useMutation/Tasks/useDeleteTasks'
import { useTasks } from './tasks-provider'
import Deleted from '@/assets/image/Deleted.svg'


export function TasksDeleteDialog() {
  const { open, setOpen, currentTask, setCurrentTask } = useTasks()
  const { mutate: deleteTask, isPending } = useDeleteTask()

  const handleOpenChange = (v: boolean) => {
    if (!v) {
      setOpen(null)
      setCurrentTask(null)
    }
  }

  const handleDelete = () => {
    if (!currentTask) return
    deleteTask(currentTask.id, {
      onSuccess: () => {
        setOpen(null)
        setCurrentTask(null)
      },
    })
  }

  return (
<AlertDialog open={open === 'delete'} onOpenChange={handleOpenChange}>
  <AlertDialogContent className="max-w-md rounded-xl">
    <AlertDialogHeader>
      <AlertDialogTitle className="text-left text-xl font-semibold">
        Delete this task?
      </AlertDialogTitle>

      <AlertDialogDescription asChild>
        <div className="flex flex-col items-center gap-4 text-center">
          <img
            src={Deleted}
            alt="Delete Task"
            className="h-44 w-auto"
          />

          <div className="text-sm leading-6 text-muted-foreground">
            You are about to delete
            <strong className="text-foreground">
              {' '}
              {currentTask?.title ?? 'this task'}
            </strong>
            .
            <br />
            This action cannot be undone.
          </div>
        </div>
      </AlertDialogDescription>
    </AlertDialogHeader>

    <AlertDialogFooter className="mt-2">
      <AlertDialogCancel disabled={isPending}>
        Cancel
      </AlertDialogCancel>

      <AlertDialogAction
        className="bg-[#FFD500] text-black hover:bg-[#E6C000] disabled:opacity-50"
        onClick={(e) => {
          e.preventDefault()
          handleDelete()
        }}
        disabled={isPending}
      >
        {isPending ? 'Deleting...' : 'Delete Task'}
      </AlertDialogAction>
    </AlertDialogFooter>
  </AlertDialogContent>
</AlertDialog>
  )
}