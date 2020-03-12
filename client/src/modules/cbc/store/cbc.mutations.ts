import _ from "lodash";
import {MutationTree} from "vuex";
import {Message, Report, ReportData} from "../models";
import {ReportDataState} from "./cbc.state";

export const mutations: MutationTree<ReportDataState> = {
	GET_REPORT_DATA_LIST: (state: ReportDataState, data: ReportData[]) => state.entities = data,
	ADD_REPORT_DATA: (state: ReportDataState, data: ReportData) =>
		state.entities.push(data),
	CREATE_REPORT_DATA: (state: ReportDataState, data: ReportData) => state.entities.push(data),
	CREATE_REPORT_DATA_REPORT: (state: ReportDataState, [id, report]: [string | number, Report]) => {
		const index = _.findIndex(state.entities, x => x.id === id);
		if (index !== -1) state.entities[index].reports.push(report);
	},
	UPDATE_REPORT_DATA: (state: ReportDataState, data: ReportData) => {
		const index = _.findIndex(state.entities, x => x.id === data.id);
		if (index !== -1) state.entities[index] = data;
	},
	UPDATE_REPORT_DATA_REPORT: (state: ReportDataState, [id, report]: [string | number, Report]) => {
		const index = _.findIndex(state.entities, x => x.id === id);
		if (index !== -1) {
			const indexReport = _.findIndex(state.entities[index].reports, x => x.id === report.id);
			if (indexReport !== -1) state.entities[index].reports[indexReport] = report;
		}
	},
	UPDATE_REPORT_DATA_MESSAGE: (state: ReportDataState, [reportId, message]: [string | number, Message]) => {
		const index = _.findIndex(state.entities, x => x.id === reportId);
		if (index !== -1) state.entities[index].message = message;
	},
	GET_REPORT_DATA: (state: ReportDataState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	}
};
