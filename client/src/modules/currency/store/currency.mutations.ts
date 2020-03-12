import {MutationTree} from "vuex";
import {Currency} from '../models/dto.model';
import {CurrencyState} from "./currency.state";

export const mutations: MutationTree<CurrencyState> = {
  SET_CURRENCY_LIST: (
      state: CurrencyState,
      currencies: Currency[]
  ) => (state.entities = currencies)
};
export default mutations;
