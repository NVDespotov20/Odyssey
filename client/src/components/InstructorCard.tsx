import { Card, CardContent } from "./ui/card"
import { Avatar, AvatarImage } from "./ui/avatar"
import { InstructorDataType } from "@/types/institutionDataTypes"

export default function InstructorCard({ data, variant, size }: { data: InstructorDataType, variant: "clickable" | "noneclickable", size: "small" | "normal" }) {
    return (
        <div className={`h-full ${variant == 'clickable' ? "cursor-pointer" : ""}`}>
            <Card className="h-full relative">
                <CardContent className="flex p-4 min-h-full flex-col gap-5 items-center">
                    <Avatar className="w-48 h-48">
                        <AvatarImage className="select-none" src="/public/placeholder.png"></AvatarImage>
                    </Avatar>

                    <div className="flex flex-col gap-1 items-center select-none">
                        <h1 className="text-xl font-bold">{data.name}</h1>
                        <p className="text-sm">{data.experiance}</p>
                    </div>

                    <div className={`w-full line-clamp-4 text-justify ${size == "small" ? "hidden" : ""}`}>
                        {data.about}
                    </div>
                </CardContent>
            </Card>
        </div>
    )
}
