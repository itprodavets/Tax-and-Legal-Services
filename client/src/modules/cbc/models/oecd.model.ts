import {
  CbcReportingRole_EnumType,
  CountryCode_Type,
  currCode_Type,
  OECDLegalAddressType_EnumType,
  CbcBizActivityType_EnumType,
  UltimateParentEntityRole_EnumType,
  OECDDocTypeIndic_EnumType,
  CbcSummaryListElementsType_EnumType,
  LanguageCode_Type,
  CbcMessageTypeIndic_EnumType,
  MessageType_EnumType
} from "./oecd.enum";

export interface CBC_OECD {
  messageSpec: MessageSpec_Type;
  cbcBody: CbcBody_Type[];
  version: string;
}

export interface MessageSpec_Type {
  sendingEntityIN: string;
  transmittingCountry: CountryCode_Type;
  receivingCountry: CountryCode_Type[];
  messageType: MessageType_EnumType;
  language: LanguageCode_Type;
  languageSpecified: boolean;
  warning: string;
  contact: string;
  messageRefId: string;
  messageTypeIndic: CbcMessageTypeIndic_EnumType;
  corrMessageRefId: string[];
  reportingPeriod: string;
  timestamp: string;
}

export interface StringMin1Max4000WithLang_Type {
  language: LanguageCode_Type;
  languageSpecified: boolean;
  value: string;
}

export interface CorrectableAdditionalInfo_Type {
  docSpec: DocSpec_Type;
  otherInfo: StringMin1Max4000WithLang_Type[];
  resCountryCode: CountryCode_Type[];
  summaryRef: CbcSummaryListElementsType_EnumType[];
}

export interface DocSpec_Type {
  docTypeIndic: OECDDocTypeIndic_EnumType;
  docRefId: string;
  corrMessageRefId: string;
  corrDocRefId: string;
}

export interface ConstituentEntity_Type {
  constEntity: OrganisationParty_Type;
  role: UltimateParentEntityRole_EnumType;
  roleSpecified: boolean;
  incorpCountryCode: CountryCode_Type;
  incorpCountryCodeSpecified: boolean;
  bizActivities: CbcBizActivityType_EnumType[];
  otherEntityInfo: string;
}

export interface OrganisationParty_Type {
  resCountryCode: CountryCode_Type[];
  tIN: TIN_Type;
  iN: OrganisationIN_Type[];
  name: NameOrganisation_Type[];
  address: Address_Type[];
}

export interface TIN_Type {
  issuedBy: CountryCode_Type;
  issuedBySpecified: boolean;
  value: string;
}

export interface OrganisationIN_Type {
  issuedBy: CountryCode_Type;
  issuedBySpecified: boolean;
  iNType: string;
  value: string;
}

export interface NameOrganisation_Type {
  value: string;
}

export interface Address_Type {
  countryCode: CountryCode_Type;
  items: any[];
  legalAddressType: OECDLegalAddressType_EnumType;
  legalAddressTypeSpecified: boolean;
}

export interface AddressFix_Type {
  street: string;
  buildingIdentifier: string;
  suiteIdentifier: string;
  floorIdentifier: string;
  districtName: string;
  pOB: string;
  postCode: string;
  city: string;
  countrySubentity: string;
}

export interface MonAmnt_Type {
  currCode: currCode_Type;
  value: string;
}

export interface CorrectableCbcReport_Type {
  docSpec: DocSpec_Type;
  resCountryCode: CountryCode_Type;
  summary: CorrectableCbcReport_TypeSummary;
  constEntities: ConstituentEntity_Type[];
}

export interface CorrectableCbcReport_TypeSummary {
  revenues: CorrectableCbcReport_TypeSummaryRevenues;
  profitOrLoss: MonAmnt_Type;
  taxPaid: MonAmnt_Type;
  taxAccrued: MonAmnt_Type;
  capital: MonAmnt_Type;
  earnings: MonAmnt_Type;
  nbEmployees: string;
  assets: MonAmnt_Type;
}

export interface CorrectableCbcReport_TypeSummaryRevenues {
  unrelated: MonAmnt_Type;
  related: MonAmnt_Type;
  total: MonAmnt_Type;
}

export interface ReportingEntity_Type {
  entity: OrganisationParty_Type;
  nameMNEGroup: string;
  reportingRole: CbcReportingRole_EnumType;
  reportingPeriod: ReportingEntity_TypeReportingPeriod;
}

export interface ReportingEntity_TypeReportingPeriod {
  startDate: string;
  endDate: string;
}

export interface CorrectableReportingEntity_Type extends ReportingEntity_Type {
  docSpec: DocSpec_Type;
}

export interface CbcBody_Type {
  reportingEntity: CorrectableReportingEntity_Type;
  cbcReports: CorrectableCbcReport_Type[];
  additionalInfo: CorrectableAdditionalInfo_Type[];
}
