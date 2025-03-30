import {axiosConfigGo} from "@/apis/config/axiosConfig.ts";
import axios from "axios";

export const academyAPI = {
    getInstitution: async (id: string) => {
        return (await axios.get(`/Academies/${id}`, axiosConfigGo)).data;
    },
    getInstructor: async (id: string) => {
        return (await axios.get(`/User/${id}`, axiosConfigGo)).data;
    }
}