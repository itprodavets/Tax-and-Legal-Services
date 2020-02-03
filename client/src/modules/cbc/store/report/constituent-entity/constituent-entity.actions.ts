import {ActionContext, ActionTree} from "vuex";
import {ConstituentEntityState} from "./constituent-entity.state";
import {ReportState} from "../report.state";
import {ConstituentEntityCreateRequest, ConstituentEntityRequest} from "@/modules/cbc/models";
import _ from "lodash";
import {Guid} from "@/core/common/guid";

export const actions: ActionTree<ConstituentEntityState, ReportState> = {
	list: async (
		action: ActionContext<ConstituentEntityState, ReportState>,
		request: ConstituentEntityRequest
	) => {
		const report = _.find(action.rootState.entities, x => x.id === request.reportId);
		if (report) action.commit("GET_CONSTITUENT_ENTITY_LIST", report.constituentEntities);
		else action.commit("GET_CONSTITUENT_ENTITY_LIST", []);
	},
	get: async (
		action: ActionContext<ConstituentEntityState, ReportState>,
		id: string | number
	) => {
		action.commit("GET_CONSTITUENT_ENTITY", id);
	},
	create: async (
		action: ActionContext<ConstituentEntityState, ReportState>,
		request: ConstituentEntityCreateRequest
	) => {
		request.constituentEntity.id = Guid.create().toString();
		action.commit("CREATE_CONSTITUENT_ENTITY", request.constituentEntity);
	}
};
