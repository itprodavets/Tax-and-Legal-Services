import {GetterTree} from "vuex";
import {ReportState} from "./report.state";

export const getters: GetterTree<ReportState, any> = {
  reports: (state: ReportState) => state.entities,
  report: (state: ReportState) => state.entity
};
