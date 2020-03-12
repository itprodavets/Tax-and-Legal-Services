import {Report} from '../../models';

export interface ReportState {
    entities: Report[],
    entity: Report
}

export const state: ReportState = {
    entities: [],
    entity: {} as Report
};
