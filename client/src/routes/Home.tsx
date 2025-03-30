import { useNavigate } from "react-router-dom"
import { Button } from "@/components/ui/button";

export default function Home() {
    const navigate = useNavigate();

    return (
        <div className="w-full flex justify-center mt-5 flex-col gap-5">
            <div className="flex justify-between items-center">
                <h1 onClick={() => { navigate("/") }} className="text-xl ml-5 font-bold cursor-pointer select-none">Odyssey</h1>

                <div className="flex gap-3 mr-5">
                    <Button onClick={() => navigate("/signup")} variant="secondary">SignIn</Button>
                    <Button onClick={() => navigate("/login")}>SignUp</Button>
                </div>
            </div>

            <div className="w-full flex justify-center items-center mt-20">
                <div className="w-[80%] rounded-xl relative flex items-center justify-center min-h-screen">
                    <img className="absolute rounded-xl top-0 left-0 right-0 bottom-0 z-[-1]" src="/plane.jpg" />
                    <div className="flex gap-2 flex-col">
                        <h1 className="text-8xl text-white font-black">Odyssey</h1>
                        <h1 className="text-6xl text-white font-black">Find your flight instructor</h1>
                        <p className="text-2xl text-white">Learn to fly with the best, most experienced instructors.</p>
                    </div>
                </div>
            </div>

            <div className="flex justify-center items-center mb-10">

                <h1 className="text-4xl">Ready for a flight</h1>
            </div>

        </div >
    )
}
