import {ApiCrudService} from "@/core/services/api.crud.service";
import {AxiosRequestConfig} from 'axios';

class CountryService extends ApiCrudService<number, any, any> {
  constructor() {
    super("countries");
    this.requestConfig = {baseURL: `${process.env.VUE_APP_REF_BOOKS_API}/v1`} as AxiosRequestConfig;
  }
}

export default new CountryService();
