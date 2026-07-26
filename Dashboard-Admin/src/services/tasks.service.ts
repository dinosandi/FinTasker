import { api } from '@/config/api'
import { ApiResponse } from '@/Type/api'
import { TaskResponse } from '@/Type'

interface GetTasksParams {
  projectId: string
  page: number
  pageSize: number
  search?: string
  status?: string
}
interface BulkDeleteResponse {
  deletedCount: number
}


export const taskService = {
  getAll: async ({
    projectId,
    page,
    pageSize,
    search,
    status,
  }: GetTasksParams) => {
    const response = await api.get<ApiResponse<TaskResponse[]>>('/tasks', {
      params: {
        ProjectId: projectId,
        Page: page,
        PageSize: pageSize,
        Search: search,
        Status: status,
      },
    })

    return response.data
  },
  bulkDelete: async (TaskIds: string[]) => {
    const response = await api.delete<ApiResponse<BulkDeleteResponse>>(
      '/Tasks/bulk',
      { data: { TaskIds } }
    )

    return response.data
  },

}