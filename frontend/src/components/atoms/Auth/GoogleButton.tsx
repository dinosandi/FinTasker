// components/atoms/GoogleButton.tsx
import { GoogleLogin } from "@react-oauth/google";

interface Props {
  onSuccess: (idToken: string) => void;
}

export const GoogleButton = ({ onSuccess }: Props) => {
  return (
    <div className="w-full flex justify-center">
      <GoogleLogin
        onSuccess={(credentialResponse) => {
          if (credentialResponse.credential) {
            onSuccess(credentialResponse.credential);
          }
        }}
        onError={() => console.log("Login gagal")}
      />
    </div>
  );
};