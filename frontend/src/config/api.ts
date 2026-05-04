// config/api.ts
import axios from "axios";
import type { AxiosInstance } from "axios";

console.log("API URL:", import.meta.env.VITE_API_URL);
export const api: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL as string,
  
  headers: {
    "Content-Type": "application/json",
  },
  
});