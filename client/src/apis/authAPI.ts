import { axiosConfigGo } from "./config/axiosConfig";
import axios from "axios";

export type SignUpData = {
    email: string,
    password: string,
    firstName?: string,
    lastName?: string,
    username?: string
}

export const authAPI = {
    signUp: async (data: SignUpData) => {
        return (await axios.post('/api/auth/register', data, axiosConfigGo)).data;
    },
    signIn: async (data: { username: string, password: string }) => {
        return (await axios.post('/api/auth/login', data, axiosConfigGo)).data;
    }
};
