import Photocard from "./Photocard";

function Photospace() {
    const photocards = [
        {
            title: "Lorem ipsum",
            description: "Lorem ipsum dolor sit amet consectetur, adipisicing elit.Quos aspernatur quidem minus quasi animi debitis laborum dolorem dolore repellendus praesentium molestiae atque rerum laudantium quas perferendis, dolorum autem ipsum tenetur?",
            src: "https://api.lorem.space/image?hash=10001"
        },
        {
            title: "Lorem ipsum dolor sit amet consectetur",
            description: "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Repellendus, a labore voluptas quasi sapiente rem iste, ducimus distinctio perspiciatis corporis quisquam cupiditate omnis aspernatur culpa saepe dolor autem placeat animi!",
            src: "https://api.lorem.space/image?hash=10002"
        },
        {
            title: "Lorem ipsum dolor sit",
            description: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Consequuntur nihil laborum ea facilis, quaerat sapiente laudantium. Nihil officia omnis aut voluptatem vel magnam, odit nesciunt dicta! Sequi corporis nesciunt reprehenderit!",
            src: "https://api.lorem.space/image?hash=10003"
        },
        {
            title: "Lorem ipsum",
            description: "Lorem ipsum dolor sit amet consectetur, adipisicing elit.Quos aspernatur quidem minus quasi animi debitis laborum dolorem dolore repellendus praesentium molestiae atque rerum laudantium quas perferendis, dolorum autem ipsum tenetur?",
            src: "https://api.lorem.space/image?hash=10004"
        },
        {
            title: "Lorem ipsum",
            description: "Lorem ipsum dolor sit amet consectetur, adipisicing elit.Quos aspernatur quidem minus quasi animi debitis laborum dolorem dolore repellendus praesentium molestiae atque rerum laudantium quas perferendis, dolorum autem ipsum tenetur?",
            src: "https://api.lorem.space/image?hash=10005"
        },
        {
            title: "Lorem ipsum",
            description: "Lorem ipsum dolor sit amet consectetur, adipisicing elit.Quos aspernatur quidem minus quasi animi debitis laborum dolorem dolore repellendus praesentium molestiae atque rerum laudantium quas perferendis, dolorum autem ipsum tenetur?",
            src: "https://api.lorem.space/image?hash=10006"
        }
    ];

    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-3 w-full">
        {
            photocards.map(card => (<Photocard title={card.title} description={card.description} src={card.src} />))
        }
        </div>
    );
}

export default Photospace;
