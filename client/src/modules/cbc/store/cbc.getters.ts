import {GetterTree} from "vuex";
import {ReportDataState} from "./cbc.state";

export const getters: GetterTree<ReportDataState, any> = {
	entities: (state: ReportDataState) => state.entities
};
