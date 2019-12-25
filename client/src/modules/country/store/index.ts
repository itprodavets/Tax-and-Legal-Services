import { Module } from "vuex";

import actions from "./country.action";
import { mutations } from "./country.mutationTypes";
import getters from "./country.getters";
import { state, CountryState } from "./country.state";
import { RootState } from '@/store/root.state';

export const country: Module<CountryState, RootState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
