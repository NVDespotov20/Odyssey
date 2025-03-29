import { useNavigate } from "react-router-dom"
import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

export default function SignIn() {
    const navigate = useNavigate()

    return (
        <div className="flex flex-row-reverse min-h-screen min-w-screen">
            <div className="flex min-h-full w-1/2 flex-col items-center gap-20 bg-[#f5f5f5] max-lg:w-full">
                <h1 className="text-3xl mt-5 ml-5 font-bold self-start">Odyssey</h1>

                <div className="flex w-2/3 flex-col gap-10">
                    <h1 className="text-3xl font-bold">Welcome back</h1>
                    <form className="flex flex-col gap-3">
                        <Input placeholder="Email" type="email" />
                        <Input placeholder="Password" type="password" />

                        <Button className="py-5 mt-5 rounded-full border-black" type="submit" variant="outline">Sign In</Button>
                    </form>

                    <div className="flex gap-3 justify-center items-center">
                        <p>Don't have an account?</p>
                        <Button onClick={() => { navigate("/signup") }} variant="link">Sign Up</Button>
                    </div>
                </div>
            </div>
            <div className="flex min-h-full w-1/2 bg-custom-radial max-lg:hidden">
                <h1 className="text-4xl mt-28 ml-16 text-black">This is our cool slogan</h1>
            </div>
        </div >
    )
}
