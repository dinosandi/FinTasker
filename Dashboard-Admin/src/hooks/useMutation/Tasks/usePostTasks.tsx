import { api } from "@/config/api";
import { useMutation } from "@tanstack/react-query";
import { Tasks } from "@/Type";

export const usePostTasks = () => {
    return useMutation({
        mutationKey: ["postTasks"],
        mutationFn: async (data: Tasks) => {
            const response = await api.post("/Tasks", data);
            return response.data;
        },
    });
}