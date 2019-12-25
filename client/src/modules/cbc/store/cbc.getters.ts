import { GetterTree } from "vuex";
import { AppState } from "./cbc.state";

export const getters: GetterTree<AppState, any> = {
  reports: (state: AppState) => state.items
};
