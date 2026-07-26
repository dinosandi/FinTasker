import { api } from "@/config/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ApiErrorResponse } from "@/Type/api";
import { toast } from "sonner";

export const useDeleteTask = () => {
  const queryClient = useQueryClient();

  return useMutation<void, AxiosError<ApiErrorResponse>, string>({
    mutationKey: ["deleteTasks"],
    mutationFn: async (id: string) => {
      await api.delete(`/tasks/${id}`);
    },
    onSuccess: () => {
      toast.success("Task deleted successfully", {
        style: {
          background: "#22c55e",
          color: "#fff",
        },
      });
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
    onError: (error) => {
      const message =
        error.response?.data?.message ?? "Failed to delete task";
      toast.error(message);
    },
  });
};