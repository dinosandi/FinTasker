import { useState } from "react";
import { Button } from "@/components/ui/button";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div className="bg-primary text-primary-foreground p-4">TEST Primary</div>

      <Button onClick={() => setCount((count) => count + 1)} className="bg-black text-white p-4">
        count is: {count}
      </Button>
    </>
  );
}

export default App;
