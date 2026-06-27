import { api } from "@/config/api";
import { ApiResponse } from "@/Type/api";
import { ProjectResponse } from "@/Type";

interface GetProjectsParams {
  page: number;
  pageSize: number;
  search?: string;
}

export const projectService = {
  getAll: async ({
    page,
    pageSize,
    search,
  }: GetProjectsParams) => {
    const response = await api.get<ApiResponse<ProjectResponse[]>>("/project", {
      params: {
        Page: page,
        PageSize: pageSize,
        Search: search,
      },
    });

    return response.data;
  },
};