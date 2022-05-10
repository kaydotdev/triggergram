import Navbar from './components/common/Navbar';
import Photospace from './components/common/Photospace';

function App() {
  return (
    <div className="w-screen h-screen flex flex-col bg-slate-100 p-2">
      <div className="basis-16"><Navbar /></div>
      <div className="flex-1 overflow-y-auto overflow-x-hidden py-3"><Photospace /></div>
    </div>
  );
}

export default App;
