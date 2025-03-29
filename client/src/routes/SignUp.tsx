import { useNavigate } from "react-router-dom"
import React, { useState } from "react"
import { useMutation } from "@tanstack/react-query"

import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"
import { toast } from "sonner"

import { authAPI } from "@/apis/authAPI"

export default function SignUp() {
    const navigate = useNavigate()

    const [userData, setUserData] = useState<{ email: string, password: string, confirmPassword: string, fname: string, lname: string, uname: string } | null>(null)

    const handleUserInfo = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUserData(prev => ({
            ...prev!,
            [e.target.name]: e.target.value
        }))
    }

    const createUserMutation = useMutation({
        mutationFn: authAPI.signUp,
        onSuccess: () => {
            navigate('/login')
        }
    })

    const handleRegister = (e: React.FormEvent) => {
        e.preventDefault()

        if (!userData?.password || !userData?.confirmPassword) {
            toast.error("Please enter a password")
            return
        }

        if (userData.password !== userData.confirmPassword) {
            toast.error("Passwords do not match")
            return
        }

        createUserMutation.mutate({
            email: userData.email,
            password: userData.password,
            firstName: userData.fname,
            lastName: userData.lname,
            username: userData.uname
        })
    }

    return (
        <div className="flex min-h-screen min-w-screen">
            <div className="flex min-h-full w-1/2 flex-col items-center gap-20 bg-[#f5f5f5] max-lg:w-full">
                <h1 className="text-3xl mt-5 ml-5 font-bold self-start">Odyssey</h1>

                <div className="flex w-2/3 flex-col gap-10">
                    <h1 className="text-3xl font-bold">First, enter your email</h1>
                    <form onSubmit={handleRegister} className="flex flex-col gap-3">
                        <div className="flex gap-3">
                            <Input onChange={handleUserInfo} placeholder="First Name" name="fname" />
                            <Input onChange={handleUserInfo} placeholder="Last Name" name="lname" />
                        </div>
                        <Input onChange={handleUserInfo} placeholder="Username" name="uname" />
                        <Input onChange={handleUserInfo} placeholder="Email" type="email" name="email" />
                        <Input onChange={handleUserInfo} placeholder="Password" type="password" name="password" />
                        <Input onChange={handleUserInfo} placeholder="Confirm Password" type="password" name="confirmPassword" />

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
