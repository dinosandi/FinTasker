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
import { useBulkDeleteTasks } from '@/hooks/useMutation/Tasks/useBulkDeleteTasks'
import Deleted from '@/assets/image/Deleted.svg'


export function TasksMultiDeleteDialog() {
  const { open, setOpen, selectedTasks, setSelectedTasks } = useTasks()
  const { mutate: bulkDeleteTasks, isPending } = useBulkDeleteTasks()

  const handleDelete = () => {
    const ids = selectedTasks.map((task) => task.id) // sesuaikan field id
    bulkDeleteTasks(ids, {
      onSuccess: () => {
        setOpen(null)
        setSelectedTasks([])
      },
    })
  }
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
<AlertDialogContent className="max-w-md">
  <AlertDialogHeader>
    <AlertDialogTitle className="text-left">
      Delete {selectedTasks.length} tasks?
    </AlertDialogTitle>

    <AlertDialogDescription asChild>
      <div className="flex flex-col items-center gap-4 text-center">
        <img
          src={Deleted}
          alt="Delete Tasks"
          className="h-45 w-auto"
        />

        <p>
          This action cannot be undone. The selected tasks will be
          permanently deleted.
        </p>
      </div>
    </AlertDialogDescription>
  </AlertDialogHeader>

  <AlertDialogFooter>
    <AlertDialogCancel disabled={isPending}>
      Cancel
    </AlertDialogCancel>

    <AlertDialogAction
      className="bg-[#FFD500] text-black hover:bg-[#FFD500]/90"
      onClick={handleDelete}
      disabled={isPending}
    >
      {isPending ? "Deleting..." : "Delete tasks"}
    </AlertDialogAction>
  </AlertDialogFooter>
</AlertDialogContent>
    </AlertDialog>
  )
}