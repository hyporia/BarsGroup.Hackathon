import axios from 'axios';

const baseUrl = "http://localhost:5000";

export const AuthorizeUser = (login, password) =>
    axios.post(`${baseUrl}/user/login`, { login, password })
        .then(resp => {
            debugger;
            return Promise.resolve(resp)
        })
        .catch(e => {
            debugger;
            console.error(e);
            return Promise.reject(e);
        })

export const GetFiles = () =>
    axios.get(`${baseUrl}/user/login`)
        .then(resp => {
            return Promise.resolve(resp.result)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })

export const SaveFile = (file) =>
    axios.post(`${baseUrl}/file`, file)
        .then(resp => {
            return Promise.resolve(resp.result)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })
