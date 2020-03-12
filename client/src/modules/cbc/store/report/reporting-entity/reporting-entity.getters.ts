import {GetterTree} from 'vuex';
import {ReportingEntityState} from './reporting-entity.state';
import {ReportState} from '../report.state';

export const getters: GetterTree<ReportingEntityState, ReportState> = {
	reportingEntities: (state: ReportingEntityState) => state.entities,
	reportingEntity: (state: ReportingEntityState) => state.entity
};
