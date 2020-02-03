import {Currency} from '../models/dto.model';

export interface CurrencyState {
  entities: Currency[];
  entity: Currency;
}

export const state: CurrencyState = {
  entities: [],
  entity: {} as Currency
};
