import {ApiCrudService} from "@/core/services/api.crud.service";

class CountryService extends ApiCrudService<number, any, any> {
  constructor() {
    super("countries", {
      baseURL: `${process.env.VUE_APP_REF_BOOKS_API}/v1`
    });
  }
}

export default new CountryService();
