import {GetterTree} from 'vuex';
import {ConstituentEntityState} from './constituent-entity.state';
import {ReportState} from '../report.state';

export const getters: GetterTree<ConstituentEntityState, ReportState> = {
	constituentEntities: (state: ConstituentEntityState) => state.entities
};
