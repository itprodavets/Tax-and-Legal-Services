import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";

Vue.use(Vuex);

import { RootState } from "./root.state";

import { cbc } from "@/modules/cbc/store";
import { country } from "@/modules/country/store";
import { language } from "@/modules/language/store";

const store: StoreOptions<RootState> = {
  state: {
    version: "1.0.0"
  },
  mutations: {},
  actions: {},
  modules: {
    cbc,
    country,
    language
  }
};
export default new Vuex.Store<RootState>(store);
