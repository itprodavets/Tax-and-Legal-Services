import {CountryEnum} from "@/modules/country/models";
import {CurrencyEnum} from "@/modules/currency/models";
import {LanguageEnum} from "@/modules/language/models";

export enum SupportedSchema {
	OECD_100 = 1,
	OECD_101 = 2,
	OECD_200 = 3
}

export interface BaseDto {
	id: string | number;
}

export interface ReportData extends BaseDto {
	version: SupportedSchema;
	message: Message;
	reports: Report[];
}

export interface Report extends BaseDto {
	reportingEntity: ReportingEntity;
	constituentEntities: ConstituentEntity[];
	additionalInfo: AdditionalInfo[];
	reports: ReportBody[];
}

export interface ReportBody extends BaseDto {
	doc: Doc;
	jurisdiction: CountryEnum;
	summary: Summary;
}

export interface MonAmnt {
	currency: CurrencyEnum;
	value: string;
}

export interface Summary {
	unrelated: MonAmnt;
	related: MonAmnt;
	total: MonAmnt;
	profitOrLoss: MonAmnt;
	taxPaid: MonAmnt;
	taxAccrued: MonAmnt;
	capital: MonAmnt;
	earnings: MonAmnt;
	nbEmployees: string;
	assets: MonAmnt;
}

export interface Message {
	sendingEntityIn: string;
	jurisdiction: CountryEnum;
	jurisdictions: CountryEnum[];
	type: MessageTypeEnum;
	language: LanguageEnum;
	warning: string;
	contact: string;
	refId: string;
	typeIndic: MessageTypeIndicEnum;
	corrMessageRefId: string[];
	reportingPeriod: string;
	timestamp: string;
}

export interface Doc {
	type: DocTypeEnum;
	refId: string;
	corrMessageRefId: string;
	corrDocRefId: string;
}

export interface ReportingEntity extends BaseDto {
	doc: Doc;
	organisation: Organisation;
	nameMNEGroup: string;
	role: ReportingRoleEnum;
	startDate: string;
	endDate: string;
}

export interface ConstituentEntity extends BaseDto {
	organisation: Organisation;
	role: UltimateParentEntityRoleEnum;
	jurisdiction: CountryEnum;
	bizActivities: BizActivityTypeEnum[];
	otherInfo: string;
}

export interface Organisation extends BaseDto {
	jurisdictions: CountryEnum[];
	hasTin: boolean;
	tin: Tin;
	in: In[];
	name: string[];
	address: Address[];
}

export interface Tin extends BaseDto {
	jurisdiction: CountryEnum;
	tin: string;
}

export interface In extends BaseDto {
	jurisdiction: CountryEnum;
	in: string;
	type: string;
}

export interface Address extends BaseDto {
	jurisdiction: CountryEnum;
	type: AddressTypeEnum;
	isFixed: boolean;
	fix: AddressFix;
	free: string;
}

export interface AddressFix {
	street: string;
	buildingIdentifier: string;
	suiteIdentifier: string;
	floorIdentifier: string;
	districtName: string;
	pob: string;
	postCode: string;
	city: string;
	countrySubentity: string;
}

export interface AdditionalInfo extends BaseDto {
	doc: Doc;
	otherInfo: OtherInfo[];
	jurisdictions: CountryEnum[];
	summaryTypes: SummaryTypeEnum[];
}

export interface OtherInfo {
	language: LanguageEnum;
	info: string;
}

export interface ValidationMessage {
	severity: ValidationSeverity;
	message: string;
}

export enum ValidationSeverity
{
	Error,
	Warning,
}

export enum MessageTypeEnum {
	CBC
}


export enum MessageTypeIndicEnum {
	CBC401,
	CBC402
}

export enum DocTypeEnum {
	OECD0,
	OECD1,
	OECD2,
	OECD3,
	OECD10,
	OECD11,
	OECD12,
	OECD13
}

export enum SummaryTypeEnum {
	CBC601,
	CBC602,
	CBC603,
	CBC604,
	CBC605,
	CBC606,
	CBC607,
	CBC608,
	CBC609,
	CBC610,
	CBC611
}

export enum AddressTypeEnum {
	OECD301,
	OECD302,
	OECD303,
	OECD304,
	OECD305
}

export enum UltimateParentEntityRoleEnum {
	CBC801,
	CBC802,
	CBC803
}

export enum BizActivityTypeEnum {
	CBC501,
	CBC502,
	CBC503,
	CBC504,
	CBC505,
	CBC506,
	CBC507,
	CBC508,
	CBC509,
	CBC510,
	CBC511,
	CBC512,
	CBC513
}

export enum ReportingRoleEnum {
	CBC701,
	CBC702,
	CBC703,
	CBC704
}
