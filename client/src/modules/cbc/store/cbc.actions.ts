import {RootState} from "@/core/store/root.state";
import {ActionContext, ActionTree} from "vuex";
import {ReportDataState} from "./cbc.state";
import {ReportDataAddRequest, ReportDataCreateRequest, ReportDataParseRequest, ReportDataUpdateRequest} from "../models";
import cbcService from "../services/cbc.service";

export const actions: ActionTree<ReportDataState, RootState> = {
	list: async (action: ActionContext<ReportDataState, any>) => {},
	get: async (
		action: ActionContext<ReportDataState, RootState>,
		id: string | number
	) => {
		action.commit("GET_REPORT_DATA", id);
	},
	add: async (action: ActionContext<ReportDataState, any>,
	            request: ReportDataAddRequest) => {
		action.commit("ADD_REPORT_DATA", request.data);
	},
	create: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataCreateRequest
	) => {
		action.commit("CREATE_REPORT_DATA", request.data);
	},
	update: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataUpdateRequest
	) => {
		action.commit("UPDATE_REPORT_DATA", request.data);
	},
	parse: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataParseRequest
	) => {
		cbcService.parse(request).then(response => {
			const request = {data: response.data} as ReportDataAddRequest;
			action.dispatch("add", request);
		});
	}
};
