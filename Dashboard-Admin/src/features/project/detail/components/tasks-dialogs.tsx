import { useState } from 'react'
import { TasksAddForm } from './tasks-add-form'
import { TasksMultiDeleteDialog } from './tasks-multi-delete-dialog'
import { TasksDeleteDialog } from './tasks-delete-dialog'

export function TasksDialogs() {
  const [open, setOpen] = useState(false)

  return (
    <>
      <TasksAddForm
        open={open}
        onOpenChange={setOpen}
      />
      <TasksMultiDeleteDialog />
      <TasksDeleteDialog />
    </>
  )
}