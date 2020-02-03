import {Module} from "vuex";

import {actions} from "./currency.actions";
import {mutations} from "./currency.mutations";
import {getters} from "./currency.getters";
import {CurrencyState, state} from "./currency.state";
import {RootState} from '@/core/store/root.state';

export const currency: Module<CurrencyState, RootState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
