import { Language } from '../models/dto.model';

export interface LanguageState {
  entities: Language[];
  entity: Language;
}

export const state: LanguageState = {
  entities: [],
  entity: {} as Language
};
