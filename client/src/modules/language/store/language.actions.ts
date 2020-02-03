import {ActionContext, ActionTree} from "vuex";

import {AxiosResponse} from "axios";
import {LanguageState} from "./language.state";
import {Language} from "../models/dto.model";
import languageService from "../services/language.service";
import {RootState} from "@/store/root.state";

export const actions: ActionTree<LanguageState, RootState> = {
  list: async ({
                 commit,
                 dispatch,
                 state
               }: ActionContext<LanguageState, RootState>) => {
    if (state.entities.length === 0) {
      return await languageService
          .list()
          .then((response: AxiosResponse<Language[]>) => {
            commit("SET_LANGUAGE_LIST", response.data);
          })
          .catch(error => console.log(error));
    }
  }
};
export default actions;
