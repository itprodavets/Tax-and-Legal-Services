import { ActionContext, ActionTree } from "vuex";

import { AxiosResponse } from "axios";
import { LanguageState } from "./language.state";
import { Language } from "../models/dto.model";
import languageService from "../services/language.service";

const actions: ActionTree<LanguageState, any> = {
  list: async ({
    commit,
    dispatch,
    state
  }: ActionContext<LanguageState, any>) => {
    return await languageService
      .list()
      .then((response: AxiosResponse<Language[]>) => {
        commit("SET_LANGUAGE_LIST", response.data);
      })
      .catch(error => console.log(error));
  }
};
export default actions;
