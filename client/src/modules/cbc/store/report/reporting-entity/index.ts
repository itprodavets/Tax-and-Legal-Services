import {Module} from "vuex";
import {actions} from "./reporting-entity.actions";
import {mutations} from "./reporting-entity.mutations";
import {getters} from "./reporting-entity.getters";
import {ReportingEntityState, state} from "./reporting-entity.state";
import {ReportState} from '../report.state';

export const reportingEntity: Module<ReportingEntityState, ReportState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
