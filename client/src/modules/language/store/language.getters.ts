import { GetterTree } from "vuex";
import { LanguageState } from "./language.state";

const getters: GetterTree<LanguageState, any> = {
  languages: (state: LanguageState) => state.entities
};
export default getters;
