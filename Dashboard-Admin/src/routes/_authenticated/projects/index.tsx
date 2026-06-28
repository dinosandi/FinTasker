import { createFileRoute } from '@tanstack/react-router'
import z from 'zod'
import { Projects } from '@/features/project'
import { statuses } from '@/features/project/data/data'

const projectSearchSchema = z.object({
    page: z.number().optional().catch(1),
    pageSize: z.number().optional().catch(10),
    status: z
        .array(z.enum(statuses.map((status) => status.value)))
        .optional()
        .catch([]),
})
export const Route = createFileRoute('/_authenticated/projects/')({
  validateSearch: projectSearchSchema,
  component: Projects,
})


