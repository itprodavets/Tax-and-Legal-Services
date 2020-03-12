import {AdditionalInfo} from "@/modules/cbc/models";
import _ from "lodash";
import {MutationTree} from "vuex";
import {AdditionalInformationState} from "./additional-information.state";

export const mutations: MutationTree<AdditionalInformationState> = {
	GET_ADDITIONAL_INFO_LIST: (state: AdditionalInformationState, additionalInfo: AdditionalInfo[]) => state.entities = additionalInfo,
	GET_ADDITIONAL_INFO: (state: AdditionalInformationState, id: string | number) => {
		const entity = state.entities.find(x => x.id === id);
		if (entity) state.entity = entity;
	},
	CREATE_ADDITIONAL_INFO: (
		state: AdditionalInformationState,
		additionalInfo: AdditionalInfo
	) => state.entities.push(additionalInfo),
	UPDATE_ADDITIONAL_INFO: (
		state: AdditionalInformationState,
		additionalInfo: AdditionalInfo
	) => {
		const index = _.findIndex(state.entities, x => x.id === additionalInfo.id);
		if (index !== -1)
			state.entities[index] = additionalInfo;
	}
};
