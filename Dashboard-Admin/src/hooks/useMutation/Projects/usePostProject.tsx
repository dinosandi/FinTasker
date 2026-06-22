import { api } from "@/config/api";
import { useMutation } from "@tanstack/react-query";
import { Project } from "@/Type";

export const usePostProject = () => {
    return useMutation({
        mutationKey: ["postProject"],
        mutationFn: async (data: Project) => {
            const response = await api.post("/project", data);
            return response.data;
        },
    });
}