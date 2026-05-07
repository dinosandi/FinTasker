import axios, {
    AxiosInstance,
} from "axios";


console.log("API Base URL:", import.meta.env.VITE_CLERK_PUBLISHABLE_KEY);
export const api: AxiosInstance = axios.create({
    baseURL: import.meta.env.VITE_CLERK_PUBLISHABLE_KEY,
    headers: {
        "Content-Type": "application/json",
    },
});