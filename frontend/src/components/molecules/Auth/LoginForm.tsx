// components/molecules/LoginForm.tsx
import { GoogleLogin } from '@react-oauth/google';
import { usePostLogin } from "@/hooks/useMutation/Auth/usePostLogin";

export const LoginForm = () => {
  const { mutate, isPending } = usePostLogin();

  return (
    <div className="flex flex-col gap-4">
      <GoogleLogin
        onSuccess={(idToken) => {
         mutate({
           idToken: idToken.credential || "",
         })
        }}
        onError={() => console.log("Login gagal")}
      />

      {isPending && (
        <p className="text-center text-sm text-muted-foreground">
          Loading...
        </p>
      )}
    </div>
  );
};