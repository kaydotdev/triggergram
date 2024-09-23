import { useState } from "react";
import { Button } from "@/components/ui/button";

function App() {
    const [count, setCount] = useState<number>(0);

    return (
      <div className="w-screen h-screen flex flex-col justify-center items-center">
          <h1 className="text-4xl font-extrabold tracking-tight lg:text-5xl m-6">
              {count}
          </h1>
          <Button onClick={() => setCount(prevState => prevState + 1)}>+1</Button>
      </div>
    )
}

export default App;
