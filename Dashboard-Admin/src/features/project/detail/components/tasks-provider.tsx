'use client'

import React, { createContext, useContext, useState } from 'react'
import { Task } from '../data/shema' 

type TasksDialogType = 'create' | 'edit' | 'delete' | 'import' | 'bulk-delete'

interface TasksContextType {
  open: TasksDialogType | null
  setOpen: (type: TasksDialogType | null) => void
  currentTask: Task | null
  setCurrentTask: (task: Task | null) => void
  selectedTasks: Task[]
  setSelectedTasks: (tasks: Task[]) => void
}

const TasksContext = createContext<TasksContextType | undefined>(undefined)

export function TasksProvider({ children }: { children: React.ReactNode }) {
  const [open, setOpen] = useState<TasksDialogType | null>(null)
  const [currentTask, setCurrentTask] = useState<Task | null>(null)
  const [selectedTasks, setSelectedTasks] = useState<Task[]>([])

  return (
    <TasksContext.Provider
      value={{
        open,
        setOpen,
        currentTask,
        setCurrentTask,
        selectedTasks,
        setSelectedTasks,
      }}
    >
      {children}
    </TasksContext.Provider>
  )
}

export function useTasks() {
  const context = useContext(TasksContext)
  if (!context) throw new Error('useTasks must be used within TasksProvider')
  return context
}