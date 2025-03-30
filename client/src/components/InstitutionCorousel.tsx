import {
    Carousel,
    CarouselContent,
    CarouselItem,
    CarouselPrevious,
    CarouselNext,
} from "@/components/ui/carousel"
import InstitutionCard from "./InstitutionCard"
import InstructorCard from "./InstructorCard"
import { InstitutionDataType } from "@/types/institutionDataTypes"

export default function InstitutionCorousel({ data }: { data: InstitutionDataType }) {
    return (
        <div className="flex flex-col gap-5 justify-center items-center h-full select-none">
            <Carousel
                opts={{
                    align: "center",
                }}
                className="min-w-[90%] p-5 relative"
            >

                <CarouselContent
                    className="h-full flex gap-3"
                >
                    <CarouselItem key={-1} className="md:basis-2/3 lg:basis-3/4 h-[50vh]">
                        <InstitutionCard data={data} id={data.id} />
                    </CarouselItem>

                    {
                        data.instructors &&
                        data.instructors.map((item, index) => {
                            return (<CarouselItem key={index} className="md:basis-1/3 lg:basis-1/4 min-h-full">
                                <InstructorCard data={item} variant="noneclickable" size="small" />
                            </CarouselItem>
                            )
                        })
                    }
                </CarouselContent>
                <CarouselPrevious className="absolute z-10" />
                <CarouselNext className="absolute z-10" />
            </Carousel>


        </div>
    )
}
