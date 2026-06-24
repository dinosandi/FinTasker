import { z } from "zod";


export const projectSchema = z.object({
  id: z.string(),
  name: z.string(),
  description: z.string(),
  status: z.string(),
  color: z.string(),
  startDate: z.string(),
  endDate: z.string(),
  createdAt: z.string(),
  updatedAt: z.string(),
});

export type Project = z.infer<
  typeof projectSchema
>;

export const projectListResponseSchema = z.object({
  success: z.boolean(),
  message: z.string(),
  data: z.array(projectSchema),
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

export type ProjectListResponse = z.infer<typeof projectListResponseSchema>
