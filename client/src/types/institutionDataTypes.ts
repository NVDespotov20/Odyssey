export type InstructorDataType = {
    id: string,
    name: string,
    pfp: string,
    about: string,
    experiance: string
}

export type InstitutionDataType = {
    id: string,
    name: string;
    price: number,
    photoUrl: string,
    location: string,
    instructors: InstructorDataType[]
}
