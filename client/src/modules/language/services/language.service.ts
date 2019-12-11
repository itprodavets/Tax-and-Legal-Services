import { ApiCrudService } from "@/core/services/api.crud.service";

class LanguageService extends ApiCrudService<number, any, any> {
  constructor() {
    super("languages");
  }
}
export default new LanguageService();
