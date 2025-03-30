import { useNavigate, useSearchParams } from "react-router-dom"
import { MapPin } from "lucide-react"
import Nav from "@/components/Nav"

import InstructorCard from "@/components/InstructorCard"

export default function Insitution() {
    const [searchParams] = useSearchParams()
    const institutionId = searchParams.get("id")

    const data = {
        id: "guid1",
        name: "Alo",
        price: 10000,
        photo: "url",
        location: "Sofia blah blah blah",
        instructors: [
            {
                id: "guid2",
                name: "John Doe",
                pfp: "url",
                about: "You there. Ogre. -I. By the order of lord Farquaad. I am authorized to place you both underarrest. And transport you to designated resettlement facility. Oh really? You and what army? Can Isay something to you?",
                experiance: "10 years"
            },
            {
                id: "guid3",
                name: "Jane Doe",
                pfp: "url",
                about: "str",
                experiance: "3 years"
            },
            {
                id: "guid4",
                name: "Mitko",
                pfp: "url",
                about: "str",
                experiance: "4 years"
            }
        ]
    }

    return (
        <div className="flex flex-col min-h-screen min-w-screen bg-[#f5f5f5]">
            <Nav isSeachVisible={false} />
            <img className="h-[35vh] m-3 rounded-sm bg-center object-cover" src="/public/random.jpg" />

            <div className="m-3 flex flex-col gap-3">
                <h1 className="text-3xl">This is really cool name here</h1>

                <div className="flex gap-3">
                    <MapPin />
                    <p className="text-2xl">Location</p>
                </div>

                <div className="flex gap-3">
                    <p className="text-2xl">BGN</p>
                    <p className="text-2xl">1000</p>
                </div>

                <div className="flex w-full justify-center  my-10">
                    <div className="flex gap-3 text-cente w-[90ch]">
                        <p>Once upon a time there was a lovely princess. But she had an enchantment upon her of a fearful
                            sort, which could only be broken by Love's first kiss. She was locked away in a castle guarded by a
                            terrible fire breathing dragon. Many brave knights had attempted to free her from this dreadful
                            prison, but none prevailed.</p>
                    </div>
                </div>
            </div>

            <div className="grid grid-cols-4 gap-4 m-5 justify-center items-center">

                {
                    data.instructors.map((instructor) => {
                        return (
                            <InstructorCard key={instructor.name} data={instructor} variant="clickable" size="normal" />
                        )
                    })
                }

            </div>
        </div>
    )
}
