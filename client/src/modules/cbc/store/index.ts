import { Module } from "vuex";
import { actions } from "./cbc.action";
import { mutations } from "./cbc.mutationTypes";
import { getters } from "./cbc.getters";
import { state, AppState } from "./cbc.state";

export const cbc: Module<AppState, any> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
