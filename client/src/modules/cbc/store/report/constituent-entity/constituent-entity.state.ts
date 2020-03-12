import {ConstituentEntity} from '@/modules/cbc/models';

export interface ConstituentEntityState {
    entities: ConstituentEntity[],
    entity: ConstituentEntity
}

export const state: ConstituentEntityState = {
    entities: [],
    entity: {} as ConstituentEntity
};
