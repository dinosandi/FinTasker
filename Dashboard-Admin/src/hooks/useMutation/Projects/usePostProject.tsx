import { useMutation, useQueryClient } from '@tanstack/react-query'
import { api } from '@/config/api'
import { Project } from '@/Type'

export const usePostProject = () => {
  const queryClient = useQueryClient()

  return useMutation({
    mutationKey: ['postProject'],

    mutationFn: async (data: Project) => {
      const response = await api.post(
        '/project',data)
      return response.data
    },

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ['projects'],
      })
    },
  })
}