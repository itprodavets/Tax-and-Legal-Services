import {ReportingEntity} from "@/modules/cbc/models";
import {MutationTree} from "vuex";
import {ReportingEntityState} from "./reporting-entity.state";

export const mutations: MutationTree<ReportingEntityState> = {
	GET_REPORTING_ENTITY_LIST: (
		state: ReportingEntityState,
		reportingEntities: ReportingEntity[]
	) => (state.entities = reportingEntities),
	ADD_REPORTING_ENTITY: (
		state: ReportingEntityState,
		reportingEntity: ReportingEntity
	) => state.entity = reportingEntity
};
