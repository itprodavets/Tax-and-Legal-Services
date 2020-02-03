import {MutationTree} from "vuex";
import {ReportDataState} from "./cbc.state";
import {ReportData} from "../models";
import _ from "lodash";

export const mutations: MutationTree<ReportDataState> = {
	ADD_REPORT_DATA: (state: ReportDataState, data: ReportData) =>
		state.entities.push(data),
	CREATE_REPORT_DATA: (state: ReportDataState, data: ReportData) =>
		state.entities.push(data),
	UPDATE_REPORT_DATA: (state: ReportDataState, data: ReportData) => {
		const index = _.findIndex(state.entities, x => x.id === data.id);
		if (index !== -1) state.entities[index] = data;
	},
	GET_REPORT_DATA: (state: ReportDataState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	}
};
