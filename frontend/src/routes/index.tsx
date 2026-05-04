import { BrowserRouter, Routes, Route } from "react-router-dom";
import AuthPage from "@/pages/Login/Auth";

export const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AuthPage />} />
      </Routes>
    </BrowserRouter>
  );
};