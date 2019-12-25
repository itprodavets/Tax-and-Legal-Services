import { ApiCrudService } from "@/core/services/api.crud.service";
import { AxiosRequestConfig } from 'axios';

class CbcService extends ApiCrudService<number, any, any> {
  constructor() {
    super("cbc");
    this.requestConfig = { baseURL: 'https://localhost:5001/api/v1'} as AxiosRequestConfig;
  }
}
export default new CbcService();