import axios from "axios";

const baseDomain = `${process.env.VUE_APP_ROOT_API}/v1`;
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

instance.interceptors.request.use((config: any) => {
  return config;
});

instance.interceptors.response.use((response: any) => {
  return response;
});

export default instance;
