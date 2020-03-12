import {GetterTree} from 'vuex';
import {ReportState} from '../report.state';
import {ReportBodyState} from './report-body.state';

export const getters: GetterTree<ReportBodyState, ReportState> = {
	parts: (state: ReportBodyState) => state.entities
};
