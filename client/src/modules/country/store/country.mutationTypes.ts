import { MutationTree } from "vuex";
import { Country } from '../models/dto.model';
import { CountryState } from "./country.state";

export const mutations: MutationTree<CountryState> = {
  SET_COUNTRY_LIST: (
    state: CountryState,
    countries: Country[]
  ) => (state.entities = countries)
};
export default mutations;
