import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";

Vue.use(Vuex);

import { country } from "@/modules/country/store/index";
import { language } from "@/modules/language/store/index";

import { RootState } from './root.state';

const store: StoreOptions<RootState> = {
  state: {
    version: "1.0.0"
  },
  mutations: {},
  actions: {},
  modules: {
    country,
    language
  }
};
export default new Vuex.Store<RootState>(store);
