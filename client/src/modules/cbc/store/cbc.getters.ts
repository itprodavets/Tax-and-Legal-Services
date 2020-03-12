import {RootState} from "@/core/store/root.state";
import {GetterTree} from "vuex";
import {ReportDataState} from "./cbc.state";

export const getters: GetterTree<ReportDataState, RootState> = {
	entities: (state: ReportDataState) => state.entities,
	entity: (state: ReportDataState) => state.entity
};
