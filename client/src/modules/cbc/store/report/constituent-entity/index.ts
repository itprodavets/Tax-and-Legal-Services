import {Module} from "vuex";
import {actions} from "./constituent-entity.actions";
import {mutations} from "./constituent-entity.mutations";
import {getters} from "./constituent-entity.getters";
import {ConstituentEntityState, state} from "./constituent-entity.state";
import {ReportState} from '../report.state';

export const constituentEntity: Module<ConstituentEntityState, ReportState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
