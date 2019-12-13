import { Module } from "vuex";

import actions from "./language.action";
import { mutations } from "./language.mutationTypes";
import getters from "./language.getters";
import { state, LanguageState } from "./language.state";
import { RootState } from '@/store/root.state';

export const language: Module<LanguageState, RootState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
