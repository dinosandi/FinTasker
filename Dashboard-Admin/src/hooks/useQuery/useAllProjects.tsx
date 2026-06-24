import { useQuery } from "@tanstack/react-query";
import { projectService } from "@/services/project.service";

interface UseAllProjectsProps {
  page: number;
  pageSize: number;
  search?: string;
}

export const useAllProjects = ({
  page,
  pageSize,
  search,
}: UseAllProjectsProps) => {
  return useQuery({
    queryKey: [
      "projects",
      page,
      pageSize,
      search,
    ],
    queryFn: () =>
      projectService.getAll({
        page,
        pageSize,
        search,
      }),
    placeholderData: (previousData) =>
      previousData,
  });
};