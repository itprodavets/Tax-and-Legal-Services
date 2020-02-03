import {CountryEnum} from "@/modules/country/models";

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
	jurisdiction: CountryEnum[];
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

export enum LanguageEnum {
	AA,
	AB,
	AF,
	AK,
	SQ,
	AM,
	AR,
	AN,
	HY,
	AS,
	AV,
	AE,
	AY,
	AZ,
	BA,
	BM,
	EU,
	BE,
	BN,
	BH,
	BI,
	BS,
	BR,
	BG,
	MY,
	CA,
	CH,
	CE,
	ZH,
	CU,
	CV,
	KW,
	CO,
	CR,
	CS,
	DA,
	DV,
	NL,
	DZ,
	EN,
	EO,
	ET,
	EE,
	FO,
	FJ,
	FI,
	FR,
	FY,
	FF,
	KA,
	DE,
	GD,
	GA,
	GL,
	GV,
	EL,
	GN,
	GU,
	HT,
	HA,
	HE,
	HZ,
	HI,
	HO,
	HR,
	HU,
	IG,
	IS,
	IO,
	II,
	IU,
	IE,
	IA,
	ID,
	IK,
	IT,
	JV,
	JA,
	KL,
	KN,
	KS,
	KR,
	KK,
	KM,
	KI,
	RW,
	KY,
	KV,
	KG,
	KO,
	KJ,
	KU,
	LO,
	LA,
	LV,
	LI,
	LN,
	LT,
	LB,
	LU,
	LG,
	MK,
	MH,
	ML,
	MI,
	MR,
	MS,
	MG,
	MT,
	MN,
	NA,
	NV,
	NR,
	ND,
	NG,
	NE,
	NN,
	NB,
	NO,
	NY,
	OC,
	OJ,
	OR,
	OM,
	OS,
	PA,
	FA,
	PI,
	PL,
	PT,
	PS,
	QU,
	RM,
	RO,
	RN,
	RU,
	SG,
	SA,
	SI,
	SK,
	SL,
	SE,
	SM,
	SN,
	SD,
	SO,
	ST,
	ES,
	SC,
	SR,
	SS,
	SU,
	SW,
	SV,
	TY,
	TA,
	TT,
	TE,
	TG,
	TL,
	TH,
	BO,
	TI,
	TO,
	TN,
	TS,
	TK,
	TR,
	TW,
	UG,
	UK,
	UR,
	UZ,
	VE,
	VI,
	VO,
	CY,
	WA,
	WO,
	XH,
	YI,
	YO,
	ZA,
	ZU
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

export enum CurrencyEnum {
	AED,
	AFN,
	ALL,
	AMD,
	ANG,
	AOA,
	ARS,
	AUD,
	AWG,
	AZN,
	BAM,
	BBD,
	BDT,
	BGN,
	BHD,
	BIF,
	BMD,
	BND,
	BOB,
	BOV,
	BRL,
	BSD,
	BTN,
	BWP,
	BYN,
	BYR,
	BZD,
	CAD,
	CDF,
	CHE,
	CHF,
	CHW,
	CLF,
	CLP,
	CNY,
	COP,
	COU,
	CRC,
	CUC,
	CUP,
	CVE,
	CZK,
	DJF,
	DKK,
	DOP,
	DZD,
	EGP,
	ERN,
	ETB,
	EUR,
	FJD,
	FKP,
	GBP,
	GEL,
	GHS,
	GIP,
	GMD,
	GNF,
	GTQ,
	GYD,
	HKD,
	HNL,
	HRK,
	HTG,
	HUF,
	IDR,
	ILS,
	INR,
	IQD,
	IRR,
	ISK,
	JMD,
	JOD,
	JPY,
	KES,
	KGS,
	KHR,
	KMF,
	KPW,
	KRW,
	KWD,
	KYD,
	KZT,
	LAK,
	LBP,
	LKR,
	LRD,
	LSL,
	LTL,
	LVL,
	LYD,
	MAD,
	MDL,
	MGA,
	MKD,
	MMK,
	MNT,
	MOP,
	MRO,
	MRU,
	MUR,
	MVR,
	MWK,
	MXN,
	MXV,
	MYR,
	MZN,
	NAD,
	NGN,
	NIO,
	NOK,
	NPR,
	NZD,
	OMR,
	PAB,
	PEN,
	PGK,
	PHP,
	PKR,
	PLN,
	PYG,
	QAR,
	RON,
	RSD,
	RUB,
	RWF,
	SAR,
	SBD,
	SCR,
	SDG,
	SEK,
	SGD,
	SHP,
	SLL,
	SOS,
	SRD,
	SSP,
	STD,
	STN,
	SVC,
	SYP,
	SZL,
	THB,
	TJS,
	TMT,
	TND,
	TOP,
	TRY,
	TTD,
	TWD,
	TZS,
	UAH,
	UGX,
	USD,
	USN,
	USS,
	UYI,
	UYU,
	UYW,
	UZS,
	VEF,
	VES,
	VND,
	VUV,
	WST,
	XAF,
	XAG,
	XAU,
	XBA,
	XBB,
	XBC,
	XBD,
	XCD,
	XDR,
	XFU,
	XOF,
	XPD,
	XPF,
	XPT,
	XSU,
	XUA,
	XXX,
	YER,
	ZAR,
	ZMW,
	ZWL
}

export enum ReportingRoleEnum {
	CBC701,
	CBC702,
	CBC703,
	CBC704
}
