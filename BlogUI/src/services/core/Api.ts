import axios from "axios";
import LocalStorageService from "../LocalStorageService";

const api = axios.create({
    //baseURL: process.env.REACT_APP_API_BASE,
    baseURL: "http://localhost:5188",
    timeout: 1000000
});


api.interceptors.request.use(async config => {
    const token = LocalStorageService.getToken();
    if (token) {
       config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});


api.interceptors.response.use(
    response => {
      return response;
    },
    (error: any) => {
      if (error.response && Number(error.response.status) === 401) {
          //refreshToken
      }
      return Promise.reject(error.response);
    }
);

export default api;