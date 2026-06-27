import { createFileRoute } from '@tanstack/react-router'
import z from 'zod'
import { Tasks } from '@/features/project/detail'
import { TASK_STATUS } from '@/features/project/detail/data/data'

const taskSearchSchema = z.object({
  page: z.number().optional().catch(1),
  pageSize: z.number().optional().catch(10),
  status: z
    .array(z.enum(TASK_STATUS.map((status) => status.value)))
    .optional()
    .catch([]),
  filter: z.string().optional().catch(''),
})

export const Route = createFileRoute('/_authenticated/projects/$projectId/')({
  validateSearch: taskSearchSchema,
  component: Tasks,
})


