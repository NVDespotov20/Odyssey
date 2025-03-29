import ProfileAvatar from "./ProfileAvatar"
import { Input } from "./ui/input"

export default function Nav() {
    return (
        <div className="flex items-center bg-[#f5f5f5] justify-between shadow-sm">
            <h1 className="text-xl ml-5 font-bold">Odyssey</h1>
            <Input placeholder="Search" className="w-2/4" />
            <ProfileAvatar name="John Doe" role="admin" />
        </div>
    )
}
