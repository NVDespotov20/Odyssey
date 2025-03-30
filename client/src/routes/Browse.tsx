import Nav from "@/components/Nav"
import { useState } from "react"
import InstitutionCorousel from "@/components/InstitutionCorousel"
import { Search } from "lucide-react"
import { browseAPI } from "@/apis/browseAPI"
import {useQuery} from "@tanstack/react-query"

export default function Browse() {
    const [searchText, setSearchText] = useState("")

	const {data, isLoading} = useQuery({
		queryKey: ['browse'],
		queryFn: browseAPI.browse
	})
    // const data = {
    //     page: 0,
    //     totalPages: 100,
    //     pageSize: 5,
    //     data: [
    //         {
    //             id: "guid1",
    //             name: "Alo",
    //             price: 10000,
    //             photo: "url",
    //             location: "Sofia blah blah blah",
    //             instructors: [
    //                 {
    //                     id: "guid1",
    //                     name: "John Doe",
    //                     pfp: "url",
    //                     about: "You there. Ogre. -I. By the order of lord Farquaad. I am authorized to place you both underarrest. And transport you to designated resettlement facility. Oh really? You and what army? Can Isay something to you?",
    //                     experiance: "10 years"
    //                 },
    //                 {
    //                     id: "guid2",
    //                     name: "Jane Doe",
    //                     pfp: "url",
    //                     about: "str",
    //                     experiance: "3 years"
    //                 },
    //                 {
    //                     id: "guid3",
    //                     name: "Mitko",
    //                     pfp: "url",
    //                     about: "str",
    //                     experiance: "4 years"
    //                 }
    //             ]
    //         },
    //         {
    //             id: "guid23",
    //             name: "Alo",
    //             price: 10000,
    //             photo: "url",
    //             location: "Sofia blah blah blah",
    //             instructors: [
    //                 {
    //                     name: "John Doe",
    //                     pfp: "url",
    //                     about: "str",
    //                     experiance: "10 years"
    //                 }
    //             ]
    //         }
    //     ]
    // }

	if(isLoading) {
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
