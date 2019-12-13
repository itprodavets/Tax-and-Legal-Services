import { Country } from '../models/dto.model';

export interface CountryState {
  entities: Country[];
  entity: Country;
}

export const state: CountryState = {
  entities: [],
  entity: {} as Country
};
