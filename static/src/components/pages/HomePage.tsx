import Navbar from '../common/Navbar';
import Photospace from '../common/Photospace';

function HomePage() {
    return (
        <div className="w-screen h-screen flex flex-col p-2">
            <div className="basis-16"><Navbar /></div>
            <div className="flex-1 overflow-y-auto overflow-x-hidden py-3"><Photospace /></div>
        </div>
    );
}

export default HomePage;
