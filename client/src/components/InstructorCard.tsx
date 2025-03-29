import { Card, CardContent } from "./ui/card"
import { Avatar, AvatarImage } from "./ui/avatar"
import { InstructorDataType } from "@/types/institutionDataTypes"

export default function InstructorCard({ data }: { data: InstructorDataType }) {
    return (
        <div className="h-full">
            <Card className="h-full relative">
                <CardContent className="flex p-4 min-h-full flex-col gap-5 justify-center items-center">
                    <Avatar className="w-48 h-48">
                        <AvatarImage className="select-none" src="/public/placeholder.png"></AvatarImage>
                    </Avatar>

                    <div className="flex flex-col gap-1 items-center select-none">
                        <h1 className="text-xl font-bold">{data.name}</h1>
                        <p className="text-sm">{data.experiance}</p>
                    </div>

                </CardContent>
            </Card>
        </div>
    )
}
