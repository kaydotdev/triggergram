import { Routes, Route } from "react-router-dom";

import LoginPage from "./components/pages/LoginPage";
import HomePage from "./components/pages/HomePage";

function App() {
  return (
    <div className="w-screen h-screen bg-slate-100">
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="homepage" element={<HomePage />} />
      </Routes>
    </div>
  );
}

export default App;
