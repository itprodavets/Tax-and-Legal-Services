import {Report, ReportingEntityAddRequest, ReportingEntityRequest} from "@/modules/cbc/models";
import _ from "lodash";
import {ActionContext, ActionTree} from "vuex";
import {ReportState} from '../report.state';
import {ReportingEntityState} from "./reporting-entity.state";

export const actions: ActionTree<ReportingEntityState, ReportState> = {
	list: async (
		action: ActionContext<ReportingEntityState, ReportState>,
		request: ReportingEntityRequest
	) => {
		let data = action.rootGetters["cbc/report/report"] as Report;
		if (data && data.id === request.reportId)
			action.commit("ADD_REPORTING_ENTITY", data.reportingEntity);
		else {
			data = _.find(action.rootGetters["cbc/report/reports"], (x: Report) => x.id === request.reportId);
			if (data)
				action.commit("ADD_REPORTING_ENTITY", data.reportingEntity);
			else
				action.commit("ADD_REPORTING_ENTITY", []);
		}
	},
	get: async (
		action: ActionContext<ReportingEntityState, ReportState>,
		id: string | number
	) => {
	
	},
	add: async (
		action: ActionContext<ReportingEntityState, ReportState>,
		request: ReportingEntityAddRequest
	) => {
		action.commit("ADD_REPORTING_ENTITY", request.reportingEntity);
	}
};
