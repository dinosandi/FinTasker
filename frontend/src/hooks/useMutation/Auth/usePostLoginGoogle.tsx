import { api } from "@/config/api";
import { useMutation } from "@tanstack/react-query";
import type { GoogleLogin } from "@/Type";

export const usePostLoginGoogle = () => {
    return useMutation({
        mutationKey: ["loginGoogle"],
        mutationFn: async (data: GoogleLogin) => {
            const response = await api.post("/auth/google-login", data);
            return response.data;
        },
    });
}

