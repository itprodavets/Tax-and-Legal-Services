import {ConstituentEntity} from "@/modules/cbc/models";
import _ from "lodash";
import {MutationTree} from "vuex";
import {ConstituentEntityState} from "./constituent-entity.state";

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
	) => state.entities.push(constituentEntity),
	UPDATE_CONSTITUENT_ENTITY: (
		state: ConstituentEntityState,
		constituentEntity: ConstituentEntity
	) => {
		const index = _.findIndex(state.entities, x => x.id === constituentEntity.id);
		if (index !== -1) state.entities[index] = constituentEntity;
	}
};
