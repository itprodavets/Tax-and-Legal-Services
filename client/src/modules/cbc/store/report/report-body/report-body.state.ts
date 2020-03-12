import {ReportBody} from '@/modules/cbc/models';

export interface ReportBodyState {
    entities: ReportBody[],
    entity: ReportBody
}

export const state: ReportBodyState = {
    entities: [],
    entity: {} as ReportBody
};
