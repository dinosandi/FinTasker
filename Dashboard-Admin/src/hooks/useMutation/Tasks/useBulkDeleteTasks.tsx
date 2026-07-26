import { useMutation, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ApiErrorResponse } from "@/Type/api";
import { toast } from "sonner";
import { taskService } from "@/services/tasks.service";


export const useBulkDeleteTasks = () => {
  const queryClient = useQueryClient();

  return useMutation<void, AxiosError<ApiErrorResponse>, string[]>({
    mutationKey: ["bulkDeleteTasks"],
    mutationFn: async (ids: string[]) => {
      await taskService.bulkDelete(ids);
    },
    onSuccess: () => {
      toast.success("Tasks deleted successfully", {
        style: {
          background: "#22c55e",
          color: "#fff",
        },
      });
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
    onError: (error) => {
      const message =
        error.response?.data?.message ?? "Failed to delete tasks";
      toast.error(message);
    },
  });

}