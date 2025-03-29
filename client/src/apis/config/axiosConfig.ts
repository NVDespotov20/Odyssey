export const axiosConfigGo = {
    baseURL: import.meta.env.VITE_BACKEND_URL_GO,
    headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('token')
    }
}
