import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "@/config/api";
import { Project } from "@/Type";

export const usePutProject = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationKey: ["putProject"],

        mutationFn: async (data: Project) => {
            const response = await api.put(
                `/project/${data.Id}`, data);
            return response.data
        },

        onSuccess: () => {
            queryClient.invalidateQueries({
                queryKey: ['projects'],
            })
        }
        }
    )
}