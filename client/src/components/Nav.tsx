import ProfileAvatar from "./ProfileAvatar"
import { Input } from "./ui/input"
import { useNavigate } from "react-router-dom";

export default function Nav({ isSeachVisible = true, search }: { isSeachVisible?: boolean, search: (text: string) => void }) {
    const navigate = useNavigate();

    return (
        <div className="flex items-center bg-[#f5f5f5] justify-between shadow-sm">
            <h1 onClick={() => navigate("/browse")} className="text-xl ml-5 font-bold cursor-pointer select-none">Odyssey</h1>
            {
                // @ts-nocheck
                isSeachVisible && <Input onChange={(e: any) => search(e.target.value)} placeholder="Search" className={`w-2/4 ${!isSeachVisible ? "hidden" : ""}`} />
            }
            <ProfileAvatar name="John Doe" role="admin" />
        </div>
    )
}
