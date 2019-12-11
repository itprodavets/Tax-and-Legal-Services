import { MutationTree } from "vuex";
import { Language } from '../models/dto.model';
import { LanguageState } from "./language.state";

export const mutations: MutationTree<LanguageState> = {
  SET_LANGUAGE_LIST: (
    state: LanguageState,
    categories: Language[]
  ) => (state.entities = categories)
};
export default mutations;
