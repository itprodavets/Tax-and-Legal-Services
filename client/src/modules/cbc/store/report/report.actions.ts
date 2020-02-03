import _ from "lodash";
import {ActionContext, ActionTree} from "vuex";
import {Report, ReportCreateRequest, ReportData, ReportRequest} from "../../models";
import {ReportDataState} from "../cbc.state";
import {ReportState} from "./report.state";

export const actions: ActionTree<ReportState, ReportDataState> = {
    list: async (
        action: ActionContext<ReportState, ReportDataState>,
        request: ReportRequest
    ) => {
        let data = action.rootGetters["cbc/entity"] as ReportData;
        if (data && data.id === request.reportDataId)
            action.commit("GET_REPORT_LIST", data.reports);
        else {
            data = _.find(action.rootGetters["cbc/entities"], (x: ReportData) => x.id === request.reportDataId);
            if (data)
                action.commit("GET_REPORT_LIST", data.reports);
            else
                action.commit("GET_REPORT_LIST", []);
        }
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
