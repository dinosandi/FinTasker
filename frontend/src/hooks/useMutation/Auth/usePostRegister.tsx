import { useMutation } from "@tanstack/react-query";
import { api } from "@/config/api";
import type { RegisterUser }  from "@/Type";

export const usePostRegister = () => {
    return useMutation({
        mutationKey: ['register'],

        mutationFn: async (data: RegisterUser) => {
            const response = await api.post('/auth/register', data)
            return response.data
        },
    })
}