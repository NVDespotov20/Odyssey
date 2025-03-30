import Nav from "@/components/Nav"
import { useState } from "react"
import InstitutionCorousel from "@/components/InstitutionCorousel"
import { browseAPI } from "@/apis/browseAPI"
import { useQuery } from "@tanstack/react-query"

export default function Browse() {
    const [searchText, setSearchText] = useState("")

    const { data, isLoading } = useQuery({
        queryKey: ['browse'],
        queryFn: browseAPI.browse
    })

    if (isLoading) {
        return
    }

    const filteredData = data.filter((item) => {
        return item.name.toLowerCase().includes(searchText.toLowerCase())
    })

    return (
        <div className="flex flex-col min-h-screen min-w-screen bg-[#f5f5f5] gap-5">
            {/* // @ts-nocheck */}
            <Nav search={setSearchText} />

            <div className="flex flex-col">
                {
                    // @ts-nocheck
                    filteredData.map((item) => {
                        // @ts-nocheck
                        return <InstitutionCorousel key={item.page} data={item} />
                    })
                }
            </div>
        </div>
    )
}
