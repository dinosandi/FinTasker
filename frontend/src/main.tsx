import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider, createRouter } from '@tanstack/react-router'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import './index.css'
<<<<<<< HEAD
import App from './App.tsx'
import {GoogleOAuthProvider} from '@react-oauth/google'


console.log("Client ID",import.meta.env.VITE_GOOGLE_CLIENT_ID);
createRoot(document.getElementById('root')!).render(
  <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
  <StrictMode>
    <App />
  </StrictMode>
  </GoogleOAuthProvider>
  
)
=======
import '@fontsource-variable/geist' 

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, 
      refetchOnWindowFocus: false,
    },
  },
})

const router = createRouter({
  routeTree,
  context: {
    auth: {
      isAuthenticated: false, // Ubah ke false untuk menguji route guard
      user: null,
    },
  },
})

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} />
    </QueryClientProvider>
  </React.StrictMode>,
)
>>>>>>> dev
