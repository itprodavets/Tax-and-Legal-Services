import {AdditionalInfo} from '@/modules/cbc/models';

export interface AdditionalInformationState {
    entities: AdditionalInfo[],
    entity: AdditionalInfo
}

export const state: AdditionalInformationState = {
    entities: [],
    entity: {} as AdditionalInfo
};
