import {Module} from "vuex";

import actions from "./country.actions";
import {mutations} from "./country.mutations";
import getters from "./country.getters";
import {CountryState, state} from "./country.state";
import {RootState} from '@/core/store/root.state';

export const country: Module<CountryState, RootState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
