import { axiosConfigGo } from "./config/axiosConfig";
import axios from "axios";

export const browseAPI = {
    browse: async () => {
        return (await axios.get('/academies', axiosConfigGo)).data;
    }
};
