import {ReportBody} from "@/modules/cbc/models";
import _ from "lodash";
import {MutationTree} from "vuex";
import {ReportBodyState} from "./report-body.state";

export const mutations: MutationTree<ReportBodyState> = {
	GET_REPORT_BODY_LIST: (
		state: ReportBodyState,
		parts: ReportBody[]
	) => state.entities = parts,
	GET_REPORT_BODY: (state: ReportBodyState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	},
	CREATE_REPORT_BODY: (
		state: ReportBodyState,
		reportBody: ReportBody
	) => state.entities.push(reportBody),
	UPDATE_REPORT_BODY: (
		state: ReportBodyState,
		reportBody: ReportBody
	) => {
		const index = _.findIndex(state.entities, x => x.id === reportBody.id);
		if (index !== -1)
			state.entities[index] = reportBody;
	}
};
