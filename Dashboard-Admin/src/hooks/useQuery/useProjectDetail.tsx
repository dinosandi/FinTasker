import { useQuery } from '@tanstack/react-query'
import { api } from '@/config/api'

export const useProjectDetail = (
  projectId: string
) => {
  return useQuery({
    queryKey: ['project-detail', projectId],

    queryFn: async () => {
      const response = await api.get(
        `/project/${projectId}`
      )

      return response.data.data
    },
  })
}