import { useMutation } from "@tanstack/react-query";
import { api } from "@/config/api";

export interface loginReequest {
    idToken: string;
}
export interface AuthResponse {
    token: string;
    email: string;
    name: string;
    isProfileCompleted: boolean;
  }
  
export const usePostLogin = () => {
    return useMutation({
        mutationFn:  async(data: loginReequest): Promise<AuthResponse> => {
            const res = await api.post('/auth/google-login', data);
            return res.data.data;
        }
    });
};