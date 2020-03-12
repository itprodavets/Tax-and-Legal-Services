import {Module} from "vuex";
import {actions} from "./additional-information.actions";
import {mutations} from "./additional-information.mutations";
import {getters} from "./additional-information.getters";
import {AdditionalInformationState, state} from "./additional-information.state";
import {ReportState} from '../report.state';

export const additionalInformation: Module<AdditionalInformationState, ReportState> = {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
