import { useSearchParams } from "react-router-dom";
import Nav from "@/components/Nav";
import { Avatar, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
} from "@/components/ui/dialog"
import { useState } from "react";
import { Calendar } from "@/components/ui/calendar"
import { format } from "date-fns"
import { TimePicker } from "@/components/ui/datetime"

export default function Instructor() {
    const [searchParams] = useSearchParams();
    const id = searchParams.get('id');

    const [selectedDate, setSelectedDate] = useState<Date | undefined>(undefined);
    const [newTime, setNewTime] = useState<Date | undefined>(undefined)

    const [isDatePickerModalOpened, setIsDatePickerModalOpened] = useState(false);

    const [timeRanges, setTimeRanges] = useState([
        { start: "08:00", end: "09:00" },
        { start: "14:00", end: "15:00" },
        { start: "20:00", end: "21:00" }
    ]);

    return (
        <div className="flex flex-col min-h-screen min-w-screen bg-[#f5f5f5]">
            <Nav isSeachVisible={false} />
            <div className="flex justify-around gap-5 m-15 items-center">
                <div className="flex w-full items-center gap-10">
                    <Avatar className="w-56 h-56">
                        <AvatarImage className="select-none" src="/public/placeholder.png"></AvatarImage>
                    </Avatar>
                    <div className="flex flex-col gap-1 items-center select-none">
                        <h1 className="text-2xl font-bold">John Doe</h1>
                        <p className="text-xl">10 years</p>
                    </div>

                </div>

                <Button onClick={() => { setIsDatePickerModalOpened(true) }} className="py-6 px-20">Book</Button>
            </div>
            <div className="flex justify-center items-center">
                <p className="w-[90ch] text-lg"> Oh, shut up! Next. What do we got? This little wooden puppet. I'm not a puppet, I'm a real boy. Five
                    shillings for the possessed toy. Take it away. No! Please, don't let them do it! Next. What do you
                    got? Well, I've got a talking donkey! Right. Well that's good for ten schillings, if you can prove it. Oh,
                    go ahead fella. Well?
                </p>
            </div>

            <Dialog open={isDatePickerModalOpened} onOpenChange={setIsDatePickerModalOpened}>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Pick avaliable date</DialogTitle>
                    </DialogHeader>

                    <div className="w-full flex justify-center mt-5 flex-col gap-5">
                        <div className="flex justify-between w-full">
                            <div className="w-[240px] text-left font-normal border border-input/30 flex justify-center items-center rounded-lg">
                                {selectedDate ? (
                                    format(selectedDate, "PPP")
                                ) : (
                                    <span>Pick a date</span>
                                )}
                            </div>

                            <Button>BOOK</Button>
                        </div>

                        <div className="flex w-full justify-center items-center flex-col gap-5">
                            <div className="flex gap-5">
                                <Calendar
                                    mode="single"
                                    selected={selectedDate}
                                    onSelect={setSelectedDate}
                                    className="justify-center border border-input/30 rounded-lg"
                                />
                                <div className="flex flex-col gap-1">
                                    {timeRanges.map((timeRange, index) => (
                                        <div key={index} className="flex gap-5 border border-input/30 p-2 rounded-lg">
                                            <p className="text-lg">{timeRange.start} - {timeRange.end}</p>
                                        </div>
                                    ))}
                                </div>
                            </div>


                            <div className="flex gap-10">
                                <div className="flex flex-col gap-2">
                                    <p className="font-bold text-sm">Pick start date</p>
                                    <TimePicker date={newTime} onChange={setNewTime} />
                                </div>

                                <div className="flex flex-col gap-2">
                                    <p className="font-bold text-sm">Pick end date</p>
                                    <TimePicker date={newTime} onChange={setNewTime} />
                                </div>
                            </div>
                        </div>

                    </div>

                </DialogContent>
            </Dialog>
        </div>
    )
}
