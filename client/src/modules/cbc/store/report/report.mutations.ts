import {MutationTree} from "vuex";
import {ReportState} from "./report.state";
import {Report} from "../../models";
import _ from "lodash";

export const mutations: MutationTree<ReportState> = {
  GET_REPORT_LIST: (
      state: ReportState,
      reports: Report[]
  ) => (state.entities = reports),
  GET_REPORT: (
      state: ReportState,
      id: string | number
  ) => {
    const entity = state.entities.find(x => x.id === id);
    if (entity) state.entity = entity;
  },
  CREATE_REPORT: (state: ReportState, report: Report) =>
      state.entities.push(report),
  UPDATE_REPORT: (state: ReportState, report: Report) => {
    const index = _.findIndex(state.entities, x => x.id === report.id);
    if (index !== -1) state.entities[index] = report;
  },
};
