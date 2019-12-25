import axios from "axios";

const baseDomain = "https://localhost:5001";
const baseURL = `${baseDomain}/api`;

const instance = axios.create({
  baseURL,
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json"
  },
  timeout: 10000
});

instance.interceptors.request.use(config => {
  return config;
});

instance.interceptors.response.use(response => {
  return response;
});

export default instance;
