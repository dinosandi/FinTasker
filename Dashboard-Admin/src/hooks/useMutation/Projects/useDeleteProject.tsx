import { api } from "@/config/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiErrorResponse } from '@/Type/api'
import { AxiosError } from "axios";
import { toast } from "sonner";

export const useDeleteProject = () => {
    const queryClient = useQueryClient()
  
    return useMutation<
      void,
      AxiosError<ApiErrorResponse>,
      string
    >({
      mutationKey: ['deleteProject'],
  
      mutationFn: async (id: string) => {
        await api.delete(`/project/${id}`)
      },
  
      onSuccess: () => {
        toast.success('Project deleted successfully',
          {
            style:{
              background: '#22c55e',
              color: '#fff',
            }
          }
        )
  
        queryClient.invalidateQueries({
          queryKey: ['projects'],
        })
      },
  
      onError: (error) => {
        toast.error(
          error.response?.data?.errors?.[0] ??
          error.response?.data?.message ??
          'Failed to delete project'
        )
      },
    })
  }

