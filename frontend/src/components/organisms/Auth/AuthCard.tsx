// components/organisms/AuthCard.tsx
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

export const AuthCard = ({ children }: { children: React.ReactNode }) => {
  return (
    <div className="flex items-center justify-center min-h-screen bg-muted">
      <Card className="w-full max-w-md shadow-xl rounded-2xl">
        <CardHeader>
          <CardTitle className="text-center text-xl">
            Login ke FinTasker
          </CardTitle>
        </CardHeader>
        <CardContent>{children}</CardContent>
      </Card>
    </div>
  );
};