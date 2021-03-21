import axios from 'axios';

const baseUrl = "http://localhost:5000";


const config = {
    withCredentials: true
}


export const AuthorizeUser = (login, password) =>
    axios.post(`${baseUrl}/user/login`, { login, password }, config)
        .then(resp => {
            if (!!resp?.data?.result)
                return Promise.resolve(resp.data)
            else return Promise.reject(resp?.data?.error)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })

export const LogoutUser = () =>
    axios.post(`${baseUrl}/user/logout`, {}, config)
        .then(resp => {
            return Promise.resolve(resp)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })

export const GetFiles = (userId = null, onlyDeleted = null) => {
    let qparams = [];
    if (!!userId) {
        qparams = qparams.concat([`userId=${userId}`]);
    }
    if (!!onlyDeleted) {
        qparams = qparams.concat([`onlyDeleted=${onlyDeleted}`]);
    }
    var q = qparams.length > 0 ? "?".concat(qparams.join('&')) : "";
    return axios.get(`${baseUrl}/File${q}`, config)
        .then(resp => {
            if (!resp?.data?.error)
                return Promise.resolve(resp.data.result.files)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });
}


export const DeleteFile = (id) =>
    axios.delete(`${baseUrl}/File/${id}`, config)
        .then(resp => {
            if (!resp?.data?.error)
                return Promise.resolve(resp.data.result);
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });

export const DeleteFilesFromBucket = (ids) => {
    const qparams = ids && Array.isArray(ids) && ids.map(x => `ids=${x}`) || [];
    const q = qparams.length > 0 ? "?".concat(qparams.join('&')) : "";
    return axios.delete(`${baseUrl}/File${q}`, config)
        .then(resp => {
            if (!resp?.data?.error)
                return Promise.resolve(resp.data.result);
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });
}


export const SaveFile = (file) => {
    var formData = new FormData();
    formData.append('file', file);
    return axios.post(`${baseUrl}/File`, formData, config)
        .then(resp => {
            return Promise.resolve(resp.data.result)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        })
}

export const DeleteUser = (id) =>
    axios.delete(`${baseUrl}/User/${id}`, config)
        .then(resp => {
            if (!resp?.data?.error)
                return Promise.resolve(resp.data.result);
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });

export const GetUsers = () => {
    return axios.get(`${baseUrl}/User`, config)
        .then(resp => {
            if (!!resp?.data?.result?.users)
                return Promise.resolve(resp.data.result.users)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });
}


export const AddUser = (userData) => {
    return axios.post(`${baseUrl}/User`, userData, config)
        .then(resp => {
            if (!!resp?.data?.result)
                return Promise.resolve(resp.data.result)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });
}

const FileDownload = require('js-file-download');

export const DownloadFileById = (id, name) => {
    return axios.get(`${baseUrl}/File/${id}`, { ...config, responseType: 'blob' })
        .then(resp => {
            if (!!resp?.data)
                FileDownload(resp.data, name);
            return Promise.resolve(resp.data.result)
        })
        .catch(e => {
            console.error(e);
            return Promise.reject(e);
        });
}
