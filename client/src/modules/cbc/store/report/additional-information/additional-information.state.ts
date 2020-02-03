import {AdditionalInfo} from '@/modules/cbc/models';

export interface AdditionalInformationState {
    entities: AdditionalInfo[]
}

export const state: AdditionalInformationState = {
    entities: []
};
