import { z } from "zod";

export const tasksSchema = z.object({
    id: z.string(),
    projectId : z.string(),
    projectName : z.string(),
    title: z.string(),
    description: z.string(),
    status: z.enum([
        "Todo",
        "InProgress",
        "Review",
        "Completed",
        "Cancelled",
      ]),
    
      priority: z.enum([
        "Low",
        "Medium",
        "High",
        "Critical",
      ]),    
    dueDate: z.string(),
    completedAt: z.string().nullable(),
    estimatedMinutes: z.number(),
    createdAt: z.string(),
    updatedAt: z.string(),
})

export type Task = z.infer<
  typeof tasksSchema
>;
