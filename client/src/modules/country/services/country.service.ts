import { ApiCrudService } from "@/core/services/api.crud.service";

class CountryService extends ApiCrudService<number, any, any> {
  constructor() {
    super("countries");
  }
}
export default new CountryService();
