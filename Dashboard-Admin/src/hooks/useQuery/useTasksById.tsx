import { useQuery } from '@tanstack/react-query'
import { taskService } from '@/services/tasks.service'

interface UseTasksByIdProps {
  projectId: string
  page: number
  pageSize: number
  search?: string
  status?: string
}

export const useTasksById = ({
  projectId,
  page,
  pageSize,
  search,
  status,
}: UseTasksByIdProps) => {
  return useQuery({
    queryKey: ['tasks', projectId, page, pageSize, search, status],

    queryFn: () =>
      taskService.getAll({
        projectId,
        page,
        pageSize,
        search,
        status,
      }),

    enabled: !!projectId,

    placeholderData: (previousData) => previousData,
  })
}
