import {ReportingEntity} from '@/modules/cbc/models';

export interface ReportingEntityState {
    entities: ReportingEntity[],
    entity: ReportingEntity
}

export const state: ReportingEntityState = {
    entities: [],
    entity: {} as ReportingEntity
};
