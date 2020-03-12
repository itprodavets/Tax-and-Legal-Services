import {Module} from "vuex";
import {actions} from "./report-body.actions";
import {mutations} from "./report-body.mutations";
import {getters} from "./report-body.getters";
import {ReportBodyState, state} from "./report-body.state";
import {ReportState} from '../report.state';

export const reportBody: Module<ReportBodyState, ReportState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
