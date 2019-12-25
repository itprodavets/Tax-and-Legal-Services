import { ActionContext, ActionTree } from "vuex";
import { AxiosResponse } from "axios";
import { CountryState } from "./country.state";
import { Country } from "../models/dto.model";
import countryService from "../services/country.service";
import { RootState } from '@/store/root.state';

const actions: ActionTree<CountryState, RootState> = {
  list: async ({
    commit,
    dispatch,
    state
  }: ActionContext<CountryState, RootState>) => {
    return await countryService
      .list()
      .then((response: AxiosResponse<Country[]>) => {
        commit("SET_COUNTRY_LIST", response.data);
      })
      .catch(error => console.log(error));
  }
};
export default actions;
