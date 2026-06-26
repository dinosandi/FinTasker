import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "@/config/api";

export interface UpdateProjectRequest {
    id: string
    name: string
    description: string
    status: number
    color: string
    startDate: string
    endDate: string
  }

  export const usePutProject = () => {
    const queryClient = useQueryClient()
  
    return useMutation({
      mutationKey: ['putProject'],
  
      mutationFn: async (
        data: UpdateProjectRequest
      ) => {
        const response = await api.put(
          `/project/${data.id}`,
          data
        )
        return response.data
      },
  
      onSuccess: (_, variables) => {
        queryClient.invalidateQueries({
          queryKey: ['projects'],
        })
  
        queryClient.invalidateQueries({
          queryKey: ['project', variables.id],
        })
      },
    })
  }