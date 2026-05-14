import axios, { AxiosInstance, AxiosError } from "axios"

console.log("API URL:", import.meta.env.VITE_API_URL)
export const api: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL, 
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true, // untuk implementasi cookie
})

// Interceptor response untuk tangkap error global
api.interceptors.response.use(
  (response) => response,
  (error: AxiosError) => {
    return Promise.reject(error)
  }
)