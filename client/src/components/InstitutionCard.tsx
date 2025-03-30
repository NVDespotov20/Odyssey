import { Card, CardContent } from "./ui/card"
import { MapPin } from "lucide-react"
import { InstitutionDataType } from "@/types/institutionDataTypes"
import { useNavigate } from "react-router-dom"

export default function InstitutionCard({ data, id }: { data: InstitutionDataType, id: string }) {
    const navigate = useNavigate()

    return (
        <div onClick={() => { navigate(`/institution?id=${id}`) }} className="h-full cursor-pointer">
            <Card className="h-full relative">
                <CardContent className="flex p-4 h-full flex-col gap-5">
                    <div className="absolute top-0 left-0 w-full h-full">
                        <img
                            src={data.photoUrl}
                            className="h-1/2 w-full object-cover rounded-t-xl"
                        />
                    </div>

                    <div className="flex absolute bottom-3 flex-col gap-2">
                        <h1 className="text-3xl font-bold">{data.name}</h1>
                        <div className="flex gap-2 items-center text-xl">
                            <MapPin />
                            <span>{data.location}</span>
                        </div>
                        <div className="flex gap-2 items-center text">
                            <p>BGN</p>
                            <span>{data.price}</span>
                        </div>
                    </div>
                </CardContent>
            </Card>
        </div>
    )
}
