import { api } from "@/config/api";
import { ApiResponse } from "@/Type/api";
import { ProjectResponse } from "@/Type";

export const projectService = {
  getAll: async () => {
    const response =
      await api.get<ApiResponse<ProjectResponse[]>>("/project");

    return response.data;
  },
};