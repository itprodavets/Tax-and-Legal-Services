import Vue from "vue";
import Vuex, {StoreOptions} from "vuex";
import VuexPersistence from "vuex-persist";
import {RootState} from "./root.state";

import {cbc} from "@/modules/cbc/store";
import {country} from "@/modules/country/store";
import {currency} from "@/modules/currency/store";
import {language} from "@/modules/language/store";

Vue.use(Vuex);

const vuexLocal = new VuexPersistence<RootState>({
  storage: window.sessionStorage
});

const store: StoreOptions<RootState> = {
  state: {
    version: "1.0.0"
  },
  mutations: {},
  actions: {},
  modules: {
    cbc,
    country,
    currency,
    language
  },
  plugins: [vuexLocal.plugin]
};
export default new Vuex.Store<RootState>(store);
