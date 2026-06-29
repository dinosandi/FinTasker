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