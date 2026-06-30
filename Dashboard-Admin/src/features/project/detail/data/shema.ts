import { z } from 'zod'

export const taskSchema = z.object({
  id: z.string(),
  projectId: z.string(),
  projectName: z.string(),
  title: z.string(),
  description: z.string(),
  status: z.string(),
  priority: z.string().optional(),
  dueDate: z.string(),
  completedAt: z.string().nullable(),
  estimatedMinutes: z.number(),
  createdAt: z.string(),
  updatedAt: z.string(),
})

export type Task = z.infer<typeof taskSchema>

export const taskListResponseSchema = z.object({
  success: z.boolean(),
  message: z.string(),
  data: z.array(taskSchema),
  meta: z.object({
    page: z.number(),
    pageSize: z.number(),
    totalCount: z.number(),
    totalPages: z.number(),
    hasNextPage: z.boolean(),
    hasPreviousPage: z.boolean(),
    timestamp: z.string(),
  }),
  errors: z.null(),
  traceId: z.string(),
})

export type TaskListResponse = z.infer<typeof taskListResponseSchema>

export const taskFormSchema = z.object({
  title: z
    .string()
    .min(1, 'Title is required')
    .max(200, 'Title must be 200 characters or less'),
  description: z
    .string()
    .max(2000, 'Description must be 2000 characters or less')
    .optional(),
  status: z.enum(['ToDo', 'InProgress', 'Review', 'Completed', 'Cancelled'], {
    error: 'Status is required',
  }),
  priority: z.enum(['Low', 'Medium', 'High', 'Critical'], {
    error: 'Priority is required',
  }),
  dueDate: z.string().min(1, 'Due date is required'),
  estimatedMinutes: z
    .number({ error: 'Estimate must be a number' })
    .min(0, 'Estimate cannot be negative')
    .max(100000, 'Estimate is too large')
    .optional(),
})
 
export type TaskFormValues = z.input<typeof taskFormSchema>
 
export type TaskFormOutput = z.output<typeof taskFormSchema>
 
export const TASK_FORM_DEFAULTS: TaskFormValues = {
  title: '',
  description: '',
  status: 'ToDo',
  priority: 'Medium',
  dueDate: '',
  estimatedMinutes: undefined,
}
