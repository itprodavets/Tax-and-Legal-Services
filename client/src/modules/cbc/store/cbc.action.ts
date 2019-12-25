import { ActionContext, ActionTree } from "vuex";
import { AppState } from "./cbc.state";
import { AxiosResponse } from "axios";

export const actions: ActionTree<AppState, any> = {
  list: async (
    { commit, dispatch, state }: ActionContext<AppState, any>,
    request: any
  ) => {
    return [];
  }
};
