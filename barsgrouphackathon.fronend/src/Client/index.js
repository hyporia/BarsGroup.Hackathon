import axios from 'axios';

const baseUrl = "http://localhost:5000";

export const AuthorizeUser = (login, password) =>
    axios.post(`${baseUrl}/user/login`, { login, password })
        .then(resp => {
            return Promise.resolve(resp)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })
