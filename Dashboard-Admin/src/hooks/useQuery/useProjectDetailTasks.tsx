import { useQuery } from "@tanstack/react-query";
import { api } from "@/config/api";
import { ProjectDetail } from "@/Type";

export const useProjectDetail = (
    projectId: string
) => {
    return useQuery<ProjectDetail>({
        queryKey: ["project-detail", projectId],
        queryFn: async () => {
            const { data } = await api.get(
                `/project/${projectId}`
            )
            return data.data
        },
        enabled: !!projectId,
    })
}