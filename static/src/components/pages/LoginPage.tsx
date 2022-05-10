import Banner from '../../static/banner.svg';

function LoginPage() {
    return (
        <div className="w-screen h-screen flex justify-center items-center">
            <form>
                <div className="flex items-center w-screen h-screen sm:items-stretch sm:card sm:h-fit sm:w-128 bg-base-100 shadow-xl">
                    <div className="card-body">
                        <div className="self-center py-4"><img src={Banner} alt="Banner" /></div>
                        <p className="text-center">Log in to your account</p>
                        <div className="form-control w-full">
                            <label className="label"><span className="label-text">Email</span></label>
                            <input type="text" placeholder="example@domain.com" className="input input-bordered w-full" />
                        </div>
                        <div className="form-control w-full">
                            <label className="label"><span className="label-text">Password</span></label>
                            <input type="password" className="input input-bordered w-full" />
                        </div>
                        <div className="flex justify-between items-center">
                            <div className="form-control">
                                <label className="label cursor-pointer px-0">
                                    <input type="checkbox" className="checkbox checkbox-primary" />
                                    <span className="label-text ml-2">Remember me</span>
                                </label>
                            </div>
                            <a href="#!" className="link link-secondary">Forgot password?</a>
                        </div>
                        <div className="form-control w-full">
                            <button type="submit" className="btn btn-primary">Login</button>
                        </div>
                        <div className="card-actions justify-end pt-4">
                            <p className="text-center">Don't have an account? <a href="#!" className="link link-secondary">Register</a></p>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    );
}

export default LoginPage;
