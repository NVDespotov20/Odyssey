export type InstructorDataType = {
    name: string,
    pfp: string,
    about: string,
    experiance: string
}

export type InstitutionDataType = {
    id: string,
    name: string;
    price: number,
    photo: string,
    location: string,
    instructors: InstructorDataType[]
}
