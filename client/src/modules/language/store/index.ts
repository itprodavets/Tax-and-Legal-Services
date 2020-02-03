import {Module} from "vuex";

import {actions} from "./language.actions";
import {mutations} from "./language.mutations";
import {getters} from "./language.getters";
import {LanguageState, state} from "./language.state";
import {RootState} from '@/store/root.state';

export const language: Module<LanguageState, RootState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
