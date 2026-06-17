import { useMutation } from "@tanstack/react-query";
import { api } from "@/config/api";


export const usePostRefreshToken = () => {
    return useMutation({
        mutationKey: ["postRefreshToken"],
        mutationFn: () => api.post("/auth/refresh"),
    });
};


