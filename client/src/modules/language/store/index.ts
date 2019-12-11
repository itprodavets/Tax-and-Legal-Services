import { Module } from "vuex";

import actions from "./language.action";
import { mutations } from "./language.mutationTypes";
import getters from "./language.getters";
import { state, LanguageState } from "./language.state";

export const language: Module<LanguageState, any> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
