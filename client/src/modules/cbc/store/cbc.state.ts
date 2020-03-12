import {ReportData} from "../models";

export interface ReportDataState {
  entities: ReportData[];
  entity: ReportData;
}

export const state: ReportDataState = {
  entities: [] as ReportData[],
  entity: {} as ReportData
};
