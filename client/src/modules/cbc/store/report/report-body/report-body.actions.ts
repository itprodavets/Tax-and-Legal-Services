import {Report, ReportBodyRequest} from "@/modules/cbc/models";
import _ from "lodash";
import {ActionContext, ActionTree} from "vuex";
import {ReportState} from '../report.state';
import {ReportBodyState} from "./report-body.state";

export const actions: ActionTree<ReportBodyState, ReportState> = {
	list: async (
		action: ActionContext<ReportBodyState, ReportState>,
		request: ReportBodyRequest
	) => {
		let data = action.rootGetters["cbc/report/report"] as Report;
		if (data && data.id === request.reportId)
			action.commit("GET_REPORT_BODY_LIST", data.reports);
		else {
			data = _.find(action.rootGetters["cbc/report/reports"], (x: Report) => x.id === request.reportId);
			if (data)
				action.commit("GET_REPORT_BODY_LIST", data.reports);
			else
				action.commit("GET_REPORT_BODY_LIST", []);
		}
	}
};
