import {Guid} from "@/core/common/guid";
import {RootState} from "@/core/store/root.state";
import {saveAs} from "file-saver";
import {ActionContext, ActionTree} from "vuex";
import {
	ReportDataAddRequest,
	ReportDataCreateReportRequest,
	ReportDataCreateRequest,
	ReportDataGenerateRequest,
	ReportDataParseRequest,
	ReportDataRequest,
	ReportDataUpdateMessageRequest,
	ReportDataUpdateReportRequest,
	ReportDataUpdateRequest,
	ReportDataValidationRequest
} from "../models";
import cbcService from "../services/cbc.service";
import {ReportDataState} from "./cbc.state";

export const actions: ActionTree<ReportDataState, RootState> = {
	list: async (action: ActionContext<ReportDataState, any>, request: ReportDataRequest) => {
		action.commit("GET_REPORT_DATA_LIST", action.state.entities || []);
	},
	get: async (
		action: ActionContext<ReportDataState, RootState>,
		id: string | number
	) => {
		action.commit("GET_REPORT_DATA", id);
	},
	get_message: async (
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
		request.data.id = Guid.create().toString();
		action.commit("CREATE_REPORT_DATA", request.data);
		return request.data.id;
	},
	create_report: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataCreateReportRequest
	) => {
		action.commit("CREATE_REPORT_DATA_REPORT", [request.id, request.report]);
	},
	update: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataUpdateRequest
	) => {
		action.commit("UPDATE_REPORT_DATA", request.data);
	},
	update_report: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataUpdateReportRequest
	) => {
		action.commit("UPDATE_REPORT_DATA_REPORT", [request.id, request.report]);
	},
	update_message: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataUpdateMessageRequest
	) => {
		action.commit("UPDATE_REPORT_DATA_MESSAGE", [request.reportDataId, request.message]);
	},
	parse: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataParseRequest
	) => {
		cbcService.parse(request).then(response => {
			const request = {data: response.data} as ReportDataAddRequest;
			action.dispatch("add", request);
		});
	},
	generate: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataGenerateRequest
	) => {
		cbcService.generate(request).then(response => {
			const blob = new Blob([response.data], {type: response.headers['content-type']});
			const content_disposition = response.headers['content-disposition'];
			saveAs(blob, content_disposition ? content_disposition.split('filename=')[1] : `${new Date().getTime()}.xml`);
		});
	},
	validate: async (
		action: ActionContext<ReportDataState, RootState>,
		request: ReportDataValidationRequest
	) => {
		cbcService.validate(request).then(response => {
			console.log(response);
		});
	}
};
