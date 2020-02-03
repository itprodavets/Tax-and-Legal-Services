import {Report, ReportingEntityAddRequest, ReportingEntityRequest} from "@/modules/cbc/models";
import {ActionContext, ActionTree} from "vuex";
import {ReportState} from '../report.state';
import {ReportingEntityState} from "./reporting-entity.state";

export const actions: ActionTree<ReportingEntityState, ReportState> = {
	list: async (
		action: ActionContext<ReportingEntityState, ReportState>,
		request: ReportingEntityRequest
	) => {
		let data = action.rootGetters["cbc/report/report"] as Report;
		if (data && data.id === request.reportId) {
			action.commit("ADD_REPORTING_ENTITY", data.reportingEntity);
		}
		
		
		//		if (data && data.id === request.reportId) {
		//			const reportingEntities = data.constituentEntities.filter(e => e.role === UltimateParentEntityRoleEnum.CBC802).map(e => {
		//				return {organisation: e.organisation} as ReportingEntity;
		//			});
		//			if (data.reportingEntity) {
		//				await action.dispatch("add", {reportId: request.reportId, reportingEntity: data.reportingEntity} as ReportingEntityAddRequest);
		//				reportingEntities.push(data.reportingEntity);
		//			}
		//			action.commit("GET_REPORTING_ENTITY_LIST", reportingEntities);
		//		}
		
		//		else {
		//			data = _.find(action.rootGetters["cbc/report/reports"], (x: Report) => x.id === request.reportId);
		//			if (data) {
		//				const reportingEntities = data.constituentEntities.filter(e => e.role === UltimateParentEntityRoleEnum.CBC802).map(e => {
		//					return {organisation: e.organisation} as ReportingEntity;
		//				});
		//				if (data.reportingEntity) {
		//					await action.dispatch("add", {reportId: request.reportId, reportingEntity: data.reportingEntity} as ReportingEntityAddRequest);
		//					reportingEntities.push(data.reportingEntity);
		//				}
		//				action.commit("GET_REPORTING_ENTITY_LIST", reportingEntities);
		//			} else
		//				action.commit("GET_REPORTING_ENTITY_LIST", []);
		//		}
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
