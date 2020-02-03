import {ApiCrudService} from "@/core/services/api.crud.service";

class CurrencyService extends ApiCrudService<number, any, any> {
  constructor() {
    super("currencies", {
      baseURL: `${process.env.VUE_APP_REF_BOOKS_API}/v1`
    });
    //    this.requestConfig = {baseURL: `${process.env.VUE_APP_REF_BOOKS_API}/v1`} as AxiosRequestConfig;
  }
}

export default new CurrencyService();
