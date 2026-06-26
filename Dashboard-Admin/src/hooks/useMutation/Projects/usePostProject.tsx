import { useMutation, useQueryClient } from '@tanstack/react-query'
import { api } from '@/config/api'
import { AxiosError } from 'axios'
import { CreateProject } from '@/Type'
import { ApiErrorResponse } from '@/Type/api'


export const usePostProject = () => {
  const queryClient = useQueryClient()

  return useMutation<
  unknown, AxiosError<ApiErrorResponse>, CreateProject>
  ({
    mutationKey: ["postProject"],
    mutationFn: async (data: CreateProject) => {
      const response = await api.post("/project", data)
      return response.data
    }

    , onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ['projects'],
      })
    }
  })
}