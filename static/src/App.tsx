import Navbar from './components/Navbar';

function App() {
  return (
    <div className="w-screen h-screen flex flex-col bg-slate-100 p-2">
      <div className="basis-16"><Navbar /></div>
      <div className="flex-1 overflow-y-auto overflow-x-hidden"></div>
    </div>
  );
}

export default App;
