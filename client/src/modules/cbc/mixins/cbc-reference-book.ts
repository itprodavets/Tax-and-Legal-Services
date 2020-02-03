import {ReferenceBook} from "@/core/models";
import {
	AddressTypeEnum,
	BizActivityTypeEnum,
	DocTypeEnum,
	MessageTypeEnum,
	MessageTypeIndicEnum,
	ReportingRoleEnum,
	SummaryTypeEnum,
	SupportedSchema,
	UltimateParentEntityRoleEnum
} from "@/modules/cbc/models";
import {Component, Vue} from "vue-property-decorator";

enum IEnum {}

@Component
export class CbcReferenceBookMixin extends Vue {
	public supportedSchemas = [
		{id: SupportedSchema.OECD_100, name: "OECD 1.0.0"},
		{id: SupportedSchema.OECD_101, name: "OECD 1.0.1"},
		{id: SupportedSchema.OECD_200, name: "OECD 2.0.0"}
	] as ReferenceBook<SupportedSchema>[];
	
	public addressTypes = [
		{
			id: AddressTypeEnum.OECD301,
			name: "Residential or Business"
		},
		{
			id: AddressTypeEnum.OECD302,
			name: "Residential"
		},
		{
			id: AddressTypeEnum.OECD303,
			name: "Business"
		},
		{
			id: AddressTypeEnum.OECD304,
			name: "Registered Office"
		},
		{
			id: AddressTypeEnum.OECD305,
			name: "Unspecified"
		}
	] as ReferenceBook<AddressTypeEnum>[];
	
	public reportingRoles = [
		{id: ReportingRoleEnum.CBC701, name: "Ultimate Parent Entity"},
		{id: ReportingRoleEnum.CBC702, name: "Surrogate Parent Entity"},
		{
			id: ReportingRoleEnum.CBC703,
			name: "Local Filing in the framework of an international exchange"
		},
		{
			id: ReportingRoleEnum.CBC704,
			name: "Local Filing with Incomplete Information"
		}
	] as ReferenceBook<ReportingRoleEnum>[];
	
	public docTypes = [
		{id: DocTypeEnum.OECD0, name: "Resent Data"},
		{id: DocTypeEnum.OECD1, name: "New Data"},
		{id: DocTypeEnum.OECD2, name: "Corrected Data"},
		{id: DocTypeEnum.OECD3, name: "Deletion of Data"},
		{id: DocTypeEnum.OECD10, name: "Resent Test Data"},
		{id: DocTypeEnum.OECD11, name: "New Test Data"},
		{id: DocTypeEnum.OECD12, name: "Corrected Test Data"},
		{id: DocTypeEnum.OECD13, name: "Deletion of Test Data"}
	] as ReferenceBook<DocTypeEnum>[];
	
	public ultimateParentEntityRoles = [
		{id: UltimateParentEntityRoleEnum.CBC801, name: "Ultimate Parent Entity"},
		{id: UltimateParentEntityRoleEnum.CBC802, name: "Reporting Entity"},
		{id: UltimateParentEntityRoleEnum.CBC803, name: "Both"}
	] as ReferenceBook<UltimateParentEntityRoleEnum>[];
	
	public bizActivityTypes = [
		{id: BizActivityTypeEnum.CBC501, name: "Research and Development"},
		{
			id: BizActivityTypeEnum.CBC502,
			name: "Holding or Managing intellectual property"
		},
		{id: BizActivityTypeEnum.CBC503, name: "Purchasing or Procurement"},
		{id: BizActivityTypeEnum.CBC504, name: "Manufacturing or Production"},
		{
			id: BizActivityTypeEnum.CBC505,
			name: "Sales, Marketing or Distribution"
		},
		{
			id: BizActivityTypeEnum.CBC506,
			name: "Administrative, Management or Support Services"
		},
		{
			id: BizActivityTypeEnum.CBC507,
			name: "Provision of Services to unrelated parties"
		},
		{id: BizActivityTypeEnum.CBC508, name: "Internal Group Finance"},
		{id: BizActivityTypeEnum.CBC509, name: "Regulated Financial Services"},
		{id: BizActivityTypeEnum.CBC510, name: "Insurance"},
		{
			id: BizActivityTypeEnum.CBC511,
			name: "Holding shares or other equity instruments"
		},
		{id: BizActivityTypeEnum.CBC512, name: "Dormant"},
		{id: BizActivityTypeEnum.CBC513, name: "Other"}
	] as ReferenceBook<BizActivityTypeEnum>[];
	
	public summaryTypes = [
		{id: SummaryTypeEnum.CBC601, name: "Revenues – Unrelated"},
		{id: SummaryTypeEnum.CBC602, name: "Revenues – Related"},
		{id: SummaryTypeEnum.CBC603, name: "Revenues – Total"},
		{id: SummaryTypeEnum.CBC604, name: "Profit or Loss"},
		{id: SummaryTypeEnum.CBC605, name: "Tax Paid"},
		{id: SummaryTypeEnum.CBC606, name: "Tax Accrued"},
		{id: SummaryTypeEnum.CBC607, name: "Capital"},
		{id: SummaryTypeEnum.CBC608, name: "Earnings"},
		{id: SummaryTypeEnum.CBC609, name: "Number of Employees"},
		{id: SummaryTypeEnum.CBC610, name: "Assets"},
		{id: SummaryTypeEnum.CBC611, name: "Name of MNE Group"}
	] as ReferenceBook<SummaryTypeEnum>[];
	
	public messageTypes = [
		{id: MessageTypeEnum.CBC, name: "CBC"}
	] as ReferenceBook<MessageTypeEnum>[];
	
	public messageTypeIndics = [
		{
			id: MessageTypeIndicEnum.CBC401,
			name: "The message contains new information"
		},
		{
			id: MessageTypeIndicEnum.CBC402,
			name:
				"The message contains corrections/deletions for previously sent information"
		}
	] as ReferenceBook<MessageTypeIndicEnum>[];
}

