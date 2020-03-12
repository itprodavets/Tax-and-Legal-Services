import {GetterTree} from "vuex";
import {LanguageState} from "./language.state";
import {RootState} from '@/core/store/root.state';

export const getters: GetterTree<LanguageState, RootState> = {
  languages: (state: LanguageState) => state.entities
};
export default getters;
