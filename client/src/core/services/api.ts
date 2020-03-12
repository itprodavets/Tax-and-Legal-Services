import axios, {AxiosInstance, AxiosRequestConfig} from "axios";

class Api {
  private baseDomain = `${process.env.VUE_APP_ROOT_API}/v1`;
  private baseURL = `${this.baseDomain}`;
  
  private get config(): AxiosRequestConfig {
    return {
      baseURL: this.baseURL,
      withCredentials: false,
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      timeout: 10000
    } as AxiosRequestConfig;
  }
  
  public createInstance(config?: AxiosRequestConfig): AxiosInstance {
    return axios.create(Object.assign(this.config, config));
  }
}

export default new Api;
