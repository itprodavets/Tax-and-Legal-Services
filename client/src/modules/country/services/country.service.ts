import { ApiCrudService } from "@/core/services/api.crud.service";
import { AxiosRequestConfig } from 'axios';

class CountryService extends ApiCrudService<number, any, any> {
  constructor() {
    super("countries");
    this.requestConfig = { baseURL: 'https://localhost:5001/api/v1'} as AxiosRequestConfig;
  }
}
export default new CountryService();
