interface PhotocardProps {
    title: string;
    description: string;
    tags?: string[];
    src: string;
}

function Photocard(props: PhotocardProps) {
    return (
        <div className="w-full aspect-video border border-stale-400 shadow-md rounded-xl">
            <img className="w-full aspect-video object-cover rounded-xl" src={props.src} alt="Photocard" />
        </div>
    );
}

export default Photocard;
