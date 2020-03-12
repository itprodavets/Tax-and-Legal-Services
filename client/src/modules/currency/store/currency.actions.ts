import {ActionContext, ActionTree} from "vuex";
import {AxiosResponse} from "axios";
import {CurrencyState} from "./currency.state";
import {Currency} from "../models/dto.model";
import currencyService from "../services/currency.service";
import {RootState} from "@/core/store/root.state";

export const actions: ActionTree<CurrencyState, RootState> = {
  list: async ({
                 commit,
                 dispatch,
                 state
               }: ActionContext<CurrencyState, RootState>) => {
    if (state.entities.length === 0) {
      return await currencyService
          .list()
          .then((response: AxiosResponse<Currency[]>) => {
            commit("SET_CURRENCY_LIST", response.data);
          })
          .catch(error => console.log(error));
    }
  }
};
export default actions;
