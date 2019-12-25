import { XmlReportData } from '../models';

export interface AppState {
  items: XmlReportData[];
  total: number;
}

export const state: AppState = {
  items: [],
  total: 0
};
