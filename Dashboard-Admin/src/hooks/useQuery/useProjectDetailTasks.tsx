import { useQuery } from "@tanstack/react-query";
import { taskService } from "@/services/tasks.service";

interface useAllProjectTasksProps {
    projectId: string;
    page: number;
    pageSize : number;
    search? : string;
}
export const useProjectDetailTasks = ({
    projectId,
    page,
    pageSize,
    search,
} :  useAllProjectTasksProps) => {
    return useQuery({
        queryKey: [
            "tasks",
            projectId,
            page,
            pageSize,
            search,
        ],
        queryFn: () => taskService.getAll({
            projectId,
            page,
            pageSize,
            search,
        }),
        placeholderData : (previousData) =>
            previousData,
    });
} 