import Vue from "vue";
import Vuex, { StoreOptions } from "vuex";

Vue.use(Vuex);

import { language } from "@/modules/language/store/index";

export interface RootState {
  version: string;
}
const store: StoreOptions<RootState> = {
  state: {
    version: "1.0.0"
  },
  mutations: {},
  actions: {},
  modules: {
    language
  }
};
export default new Vuex.Store<RootState>(store);
