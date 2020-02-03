
import {Module} from "vuex";
import {actions} from "./report.actions";
import {mutations} from "./report.mutations";
import {getters} from "./report.getters";
import {ReportState, state} from "./report.state";

import {constituentEntity} from "./constituent-entity";
import {reportingEntity} from "./reporting-entity";
import {additionalInformation} from "./additional-information";
import {reportBody} from "./report-body";

export const report: Module<ReportState, any> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
  modules: {
    constituentEntity,
    reportingEntity,
    additionalInformation,
    reportBody
  }
};
