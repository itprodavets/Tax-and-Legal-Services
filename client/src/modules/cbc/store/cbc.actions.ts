import {ActionContext, ActionTree} from "vuex";
import {ReportDataState} from "./cbc.state";
import {ReportData, ReportDataImportRequest} from "../models";
import cbcService from "../services/cbc.service";

export const actions: ActionTree<ReportDataState, any> = {
	list: async ({
		             commit,
		             dispatch,
		             state
	             }: ActionContext<ReportDataState, any>) => {},
	get: async (
		{commit, dispatch, state}: ActionContext<ReportDataState, any>,
		id: string | number
	) => {
		commit("GET_REPORT_DATA", id);
	},
	create: async (
		{commit, dispatch, state}: ActionContext<ReportDataState, any>,
		request: ReportData
	) => {
		commit("CREATE_REPORT_DATA", request);
	},
	update: async (
		{commit, dispatch, state}: ActionContext<ReportDataState, any>,
		request: ReportData
	) => {
		commit("UPDATE_REPORT_DATA", request);
	},
	import: async (
		action: ActionContext<ReportDataState, any>,
		request: ReportDataImportRequest
	) => {
		cbcService.xmlToModel(request).then(response => {
			console.log(response.data);
		});
	}
};
