import { useState } from 'react';

function Navbar() {
    const [notificationCount, setNotificationCount] = useState(0);

    return (
        <div className="navbar rounded-xl bg-base-100 border border-gray-200 shadow-md">
            <div className="navbar-start">
                <div className="tooltip tooltip-bottom" data-tip="Homepage">
                    <a className="btn btn-ghost btn-circle normal-case text-xl">
                        <svg className="w-8 h-8" viewBox="0 0 31 45" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M3.14043 42.1702L3.13836 42.1771L3.13638 42.184C3.13142 42.2015 3.12514 42.2228 3.118 42.2472C3.08426 42.3622 3.03127 42.5428 3.00676 42.6796C3.00644 42.6814 3.00605 42.6836 3.00559 42.686C2.99459 42.7455 2.94726 43.0016 3.03619 43.2773C3.09613 43.4631 3.24507 43.7363 3.56897 43.8946C3.87074 44.042 4.14991 44.0025 4.29118 43.9668C4.5411 43.9036 4.72845 43.7592 4.79876 43.7048C4.90235 43.6245 5.01162 43.5258 5.12344 43.4182C5.34924 43.2008 5.65108 42.883 6.04849 42.4468C7.61875 40.7233 10.914 36.8921 17.6264 29.0878L17.8763 28.7973L17.8781 28.7952L26.76 18.4094L28.1057 16.8358L26.0365 16.7602L21.7975 16.6052L21.7903 16.6049L21.7832 16.6047C20.676 16.5801 19.674 16.5197 18.9459 16.4431C19.1079 16.1162 19.3046 15.7247 19.5303 15.2794C20.2156 13.9276 21.16 12.0968 22.1939 10.1086C23.2292 8.11794 24.1768 6.28092 24.8661 4.9276C25.2105 4.25149 25.492 3.69316 25.688 3.29582C25.7856 3.09807 25.8647 2.93476 25.9205 2.81481C25.9478 2.75613 25.9737 2.69889 25.9944 2.6495C26.0042 2.62612 26.018 2.59211 26.031 2.5546C26.0374 2.53641 26.0479 2.50489 26.058 2.46614L26.0586 2.46417C26.0645 2.44142 26.0916 2.33834 26.0916 2.20668C26.0916 1.85567 25.9159 1.61772 25.8067 1.50523C25.7 1.39535 25.5929 1.33529 25.542 1.30897C25.4367 1.25442 25.3425 1.22903 25.3073 1.21983C25.2211 1.19731 25.1349 1.18453 25.0765 1.17679C24.9473 1.15968 24.7796 1.14548 24.5918 1.13299C24.2094 1.10757 23.6686 1.08454 23.0145 1.06503C21.703 1.02592 19.9012 1 17.9256 1H10.7091H10.0167L9.77304 1.64806L5.63663 12.6493C3.38371 18.4663 1.3849 23.7783 1.1351 24.5595C1.05586 24.765 0.880396 25.2704 1.12696 25.7858C1.40118 26.359 1.98109 26.5274 2.29262 26.5963C2.66515 26.6786 3.15309 26.7175 3.74558 26.7395C4.35248 26.762 5.13204 26.7684 6.11673 26.7684C7.28608 26.7684 8.33863 26.8068 9.08644 26.8742C9.1119 26.8765 9.13685 26.8788 9.1613 26.8811C9.02306 27.2504 8.84731 27.7099 8.64101 28.2429C8.10069 29.6388 7.35878 31.5186 6.55218 33.5575C4.89973 37.5567 3.36055 41.4377 3.14043 42.1702ZM9.47686 25.9928C9.4766 25.9937 9.47632 25.9947 9.47602 25.9958C9.48168 25.9695 9.48454 25.9648 9.47686 25.9928ZM3.57416 42.1235C3.55255 42.1403 3.55405 42.1365 3.57502 42.1229C3.57473 42.1231 3.57444 42.1233 3.57416 42.1235Z" fill="#FFD62F" stroke="#FFA200" stroke-width="2" />
                        </svg>
                    </a>
                </div>
                <input type="text" placeholder="Search..." className="input input-bordered input-sm w-full ml-2" />
            </div>
            <div className="navbar-end">
                <div className="tooltip tooltip-bottom" data-tip="Create">
                    <button className="btn btn-ghost btn-circle">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}>
                            <path strokeLinecap="round" strokeLinejoin="round" d="M12 9v3m0 0v3m0-3h3m-3 0H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                    </button>
                </div>
                <div className="tooltip tooltip-bottom" data-tip="Notifications">
                    <button className="btn btn-ghost btn-circle">
                        {
                            notificationCount === 0 ? (
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" /></svg>
                            ) : (
                                <div className="indicator">
                                    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" /></svg>
                                    <span className="badge badge-xs badge-primary indicator-item"></span>
                                </div>
                            )
                        }
                    </button>
                </div>
                <div className="dropdown dropdown-end">
                    <label tabIndex={0} className="btn btn-ghost btn-circle avatar">
                        <div className="w-8 h-8 rounded-full">
                            <img src="https://api.lorem.space/image/face" alt="Profile" />
                        </div>
                    </label>
                    <ul tabIndex={0} className="menu menu-compact dropdown-content mt-4 p-2 shadow bg-base-100 rounded-box">
                        <li>
                            <a>
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}>
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                                </svg>
                                Profile
                            </a>
                        </li>
                        <li>
                            <a>
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}>
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z" />
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                                </svg>
                                Settings
                            </a>
                        </li>
                        <li>
                            <a>
                                <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor" strokeWidth={2}>
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
                                </svg>
                                Logout
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
}

export default Navbar;
