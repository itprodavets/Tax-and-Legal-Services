import {MutationTree} from "vuex";
import {ReportDataState} from "./cbc.state";
import {ReportData} from "../models";
import _ from "lodash";

export const mutations: MutationTree<ReportDataState> = {
	CREATE_REPORT_DATA: (state: ReportDataState, reportData: ReportData) =>
		state.entities.push(reportData),
	UPDATE_REPORT_DATA: (state: ReportDataState, reportData: ReportData) => {
		const index = _.findIndex(state.entities, x => x.id === reportData.id);
		if (index !== -1) state.entities[index] = reportData;
	},
	GET_REPORT_DATA: (state: ReportDataState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	}
};
