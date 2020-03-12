import {Module} from "vuex";
import {actions} from "./cbc.actions";
import {mutations} from "./cbc.mutations";
import {getters} from "./cbc.getters";
import {ReportDataState, state} from "./cbc.state";
import {report} from "./report";
import {RootState} from "@/core/store/root.state";

export const cbc: Module<ReportDataState, RootState> = {
	namespaced: true,
	state: state,
	getters: getters,
	actions: actions,
	mutations: mutations,
	modules: {
		report
	}
};
