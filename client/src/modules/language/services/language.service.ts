import { ApiCrudService } from "@/core/services/api.crud.service";
import { AxiosRequestConfig } from 'axios';

class LanguageService extends ApiCrudService<number, any, any> {
  constructor() {
    super("languages");
    this.requestConfig = { baseURL: 'https://localhost:5001/api/v1'} as AxiosRequestConfig;
  }
}
export default new LanguageService();
