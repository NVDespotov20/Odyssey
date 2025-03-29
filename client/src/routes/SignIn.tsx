import { useNavigate } from "react-router-dom"
import React, { useState, useEffect } from "react"
import { useMutation } from "@tanstack/react-query"

import { authAPI } from "@/apis/authAPI"

import { toast } from "sonner"

import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

export default function SignIn() {
    const navigate = useNavigate()

    const [userData, setUserData] = useState<{ username: string, password: string } | null>(null)

    const loginUserMutation = useMutation({
        mutationFn: authAPI.signIn,
        onSuccess: (data) => {
            localStorage.setItem('accessToken', data.data.accessToken)
            localStorage.setItem('refreshToken', data.data.refreshToken)
            window.location.href = "/browse"
        },
    })

    useEffect(() => {
        if (localStorage.getItem('token')) {
            navigate("/browse")
        }
    })

    const handleUserCredentials = (e: React.ChangeEvent<HTMLInputElement>) => {
        setUserData(prev => ({
            ...prev!,
            [e.target.name]: e.target.value
        }))
    }

    const handleLogin = (e: React.FormEvent) => {
        e.preventDefault()

        if (!userData?.username || !userData?.password) {
            toast.error("Please enter your email and password")
            return
        }

        loginUserMutation.mutate(userData)
    }

    return (
        <div className="flex flex-row-reverse min-h-screen min-w-screen">
            <div className="flex min-h-full w-1/2 flex-col items-center gap-20 bg-[#f5f5f5] max-lg:w-full">
                <h1 className="text-3xl mt-5 ml-5 font-bold self-start">Odyssey</h1>

                <div className="flex w-2/3 flex-col gap-10">
                    <h1 className="text-3xl font-bold">Welcome back</h1>
                    <form onSubmit={handleLogin} className="flex flex-col gap-3">
                        <Input onChange={handleUserCredentials} placeholder="Username" type="username" name="username" />
                        <Input onChange={handleUserCredentials} placeholder="Password" type="password" name="password" />

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
