import { api } from "@/config/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Task } from "@/Type";
import { AxiosError } from "axios";
import { ApiErrorResponse } from '@/Type/api'

export const usePostTask = () => {
    const queryClient = useQueryClient()

    return useMutation<unknown, AxiosError<ApiErrorResponse>, Task>
    ({
        mutationKey:["postTask"],
        mutationFn : async (data: Task) => {
            const response = await api.post("/tasks", data)
            return response.data
        },
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey:['tasks']})
        }
    })
}