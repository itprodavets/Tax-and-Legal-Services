import { MutationTree } from "vuex";
import { Language } from '../models/dto.model';
import { LanguageState } from "./language.state";

export const mutations: MutationTree<LanguageState> = {
  SET_LANGUAGE_LIST: (
    state: LanguageState,
    languages: Language[]
  ) => (state.entities = languages)
};
export default mutations;
