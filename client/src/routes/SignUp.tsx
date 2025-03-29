import { useNavigate } from "react-router-dom"

import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

export default function SignUp() {
    const navigate = useNavigate()

    return (
        <div className="flex min-h-screen min-w-screen">
            <div className="flex min-h-full w-1/2 flex-col items-center gap-20 bg-[#f5f5f5] max-lg:w-full">
                <h1 className="text-3xl mt-5 ml-5 font-bold self-start">Odyssey</h1>

                <div className="flex w-2/3 flex-col gap-10">
                    <h1 className="text-3xl font-bold">First, enter your email</h1>
                    <form className="flex flex-col gap-3">
                        <div className="flex gap-3">
                            <Input placeholder="First Name" />
                            <Input placeholder="Last Name" />
                        </div>
                        <Input placeholder="Username" />
                        <Input placeholder="Email" type="email" />
                        <Input placeholder="Password" type="password" />
                        <Input placeholder="Confirm Password" type="password" />

                        <Button className="py-5 mt-5 rounded-full border-black" type="submit" variant="outline">Sign Up</Button>
                    </form>

                    <div className="flex gap-3 justify-center items-center">
                        <p>Already have an account?</p>
                        <Button onClick={() => { navigate("/login") }} variant="link">Login</Button>
                    </div>
                </div>
            </div>
            <div className="flex min-h-full w-1/2 bg-custom-radial max-lg:hidden">
                <h1 className="text-4xl mt-28 ml-16 text-black">This is our cool slogan</h1>
            </div>
        </div >
    )
}
