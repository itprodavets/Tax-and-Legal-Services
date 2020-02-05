import {ReportBody} from "@/modules/cbc/models";
import {MutationTree} from "vuex";
import {ReportBodyState} from "./report-body.state";

export const mutations: MutationTree<ReportBodyState> = {
	GET_REPORT_BODY_LIST: (
		state: ReportBodyState,
		parts: ReportBody[]
	) => state.entities = parts
};
