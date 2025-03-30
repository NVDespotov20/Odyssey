import ProfileAvatar from "./ProfileAvatar"
import { Input } from "./ui/input"
import { useNavigate } from "react-router-dom";

export default function Nav({ isSeachVisible = true }: { isSeachVisible?: boolean }) {
    const navigate = useNavigate();

    return (
        <div className="flex items-center bg-[#f5f5f5] justify-between shadow-sm">
            <h1 onClick={() => navigate("/browse")} className="text-xl ml-5 font-bold cursor-pointer select-none">Odyssey</h1>
            <Input placeholder="Search" className={`w-2/4 ${!isSeachVisible ? "hidden" : ""}`} />
            <ProfileAvatar name="John Doe" role="admin" />
        </div>
    )
}
