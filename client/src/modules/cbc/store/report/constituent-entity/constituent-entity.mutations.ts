import {MutationTree} from "vuex";
import {ConstituentEntityState} from "./constituent-entity.state";
import {ConstituentEntity} from "@/modules/cbc/models";

export const mutations: MutationTree<ConstituentEntityState> = {
	GET_CONSTITUENT_ENTITY_LIST: (
		state: ConstituentEntityState,
		constituentEntities: ConstituentEntity[]
	) => (state.entities = constituentEntities),
	GET_CONSTITUENT_ENTITY: (state: ConstituentEntityState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	},
	CREATE_CONSTITUENT_ENTITY: (
		state: ConstituentEntityState,
		constituentEntity: ConstituentEntity
	) => state.entities.push(constituentEntity)
};
