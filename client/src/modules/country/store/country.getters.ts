import {GetterTree} from "vuex";
import {CountryState} from "./country.state";
import {RootState} from '@/core/store/root.state';

const getters: GetterTree<CountryState, RootState> = {
  countries: (state: CountryState) => state.entities
};
export default getters;
