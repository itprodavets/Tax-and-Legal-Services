import {ActionContext, ActionTree} from "vuex";
import {ReportState} from "./report.state";
import {ReportDataState} from "../cbc.state";
import _ from "lodash";
import {Report, ReportCreateRequest, ReportRequest} from "../../models";

export const actions: ActionTree<ReportState, ReportDataState> = {
    list: async (
        action: ActionContext<ReportState, ReportDataState>,
        request: ReportRequest
    ) => {
        const reportData = _.find(action.rootState.entities, x => x.id === request.reportDataId);
        if (reportData) action.commit("GET_REPORT_LIST", reportData.reports);
        else action.commit("GET_REPORT_LIST", []);
    },
    get: async (
        action: ActionContext<ReportState, ReportDataState>,
        id: string | number
    ) => {
        action.commit("GET_REPORT", id);
    },
    create: async (
        action: ActionContext<ReportState, ReportDataState>,
        command: ReportCreateRequest
    ) => {
        action.commit("CREATE_REPORT", command);
    },
    update: async (
        action: ActionContext<ReportState, ReportDataState>,
        request: Report
    ) => {
        action.commit("UPDATE_REPORT", request);
    }
};
