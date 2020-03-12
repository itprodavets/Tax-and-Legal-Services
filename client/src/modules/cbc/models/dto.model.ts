import {CountryEnum} from "@/modules/country/models";
import {CurrencyEnum} from "@/modules/currency/models";
import {LanguageEnum} from "@/modules/language/models";
import {
	AddressTypeEnum,
	BizActivityTypeEnum,
	DocTypeEnum,
	MessageTypeEnum,
	MessageTypeIndicEnum,
	ReportingRoleEnum,
	SummaryTypeEnum,
	SupportedSchema,
	UltimateParentEntityRoleEnum,
	ValidationSeverity
} from "./enum.model";


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

export interface Summary extends BaseDto {
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
	language?: LanguageEnum;
	warning: string;
	contact: string;
	refId: string;
	typeIndic?: MessageTypeIndicEnum;
	corrMessageRefId: string[];
	reportingPeriod: string;
	timestamp: string;
}

export interface Doc extends BaseDto {
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
	role?: UltimateParentEntityRoleEnum;
	jurisdiction?: CountryEnum;
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
	jurisdiction?: CountryEnum;
	tin: string;
}

export interface In extends BaseDto {
	jurisdiction?: CountryEnum;
	in: string;
	type: string;
}

export interface Address extends BaseDto {
	jurisdiction: CountryEnum;
	type?: AddressTypeEnum;
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

export interface OtherInfo extends BaseDto {
	language?: LanguageEnum;
	info: string;
}

export interface ValidationMessage {
	severity: ValidationSeverity;
	message: string;
}

