import {AdditionalInfo} from "@/modules/cbc/models";
import {MutationTree} from "vuex";
import {AdditionalInformationState} from "./additional-information.state";

export const mutations: MutationTree<AdditionalInformationState> = {
	GET_ADDITIONAL_INFO_LIST: (state: AdditionalInformationState, additionalInfo: AdditionalInfo[]) => state.entities = additionalInfo
};
