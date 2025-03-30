import axios from 'axios';

export const appointmentsAPI = {
    getMonth: async (month: number) => {
        const response = await axios.get(`/api/appointments/getMonth?month=${month}`);
        return response.data;
    }
};