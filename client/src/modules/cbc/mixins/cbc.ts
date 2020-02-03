import {Component, Vue} from "vue-property-decorator";
import {EnumUnion, EnumUnionType, IEnum, ReferenceBook} from "@/core/models";
import {
  AddressTypeEnum,
  BizActivityTypeEnum,
  CountryEnum,
  CurrencyEnum,
  DocTypeEnum,
  MessageTypeEnum,
  MessageTypeIndicEnum,
  ReportingRoleEnum,
  SummaryTypeEnum,
  SupportedSchema,
  UltimateParentEntityRoleEnum
} from "../models";
import _ from "lodash";
import moment from "moment";
import {Country} from "@/modules/country/models/dto.model";
import {Currency} from "@/modules/currency/models/dto.model";

@Component
export class CbcMixin extends Vue {
  public supportedSchemas = [
    {id: SupportedSchema.OECD_100, name: "OECD 1.0.0"},
    {id: SupportedSchema.OECD_101, name: "OECD 1.0.1"},
    {id: SupportedSchema.OECD_200, name: "OECD 2.0.0"}
  ] as ReferenceBook[];
  
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
  ] as ReferenceBook[];
  
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
  ] as ReferenceBook[];
  
  public docTypes = [
    {id: DocTypeEnum.OECD0, name: "Resent Data"},
    {id: DocTypeEnum.OECD1, name: "New Data"},
    {id: DocTypeEnum.OECD2, name: "Corrected Data"},
    {id: DocTypeEnum.OECD3, name: "Deletion of Data"},
    {id: DocTypeEnum.OECD10, name: "Resent Test Data"},
    {id: DocTypeEnum.OECD11, name: "New Test Data"},
    {id: DocTypeEnum.OECD12, name: "Corrected Test Data"},
    {id: DocTypeEnum.OECD13, name: "Deletion of Test Data"}
  ] as ReferenceBook[];
  
  public ultimateParentEntityRoles = [
    {id: UltimateParentEntityRoleEnum.CBC801, name: "Ultimate Parent Entity"},
    {id: UltimateParentEntityRoleEnum.CBC802, name: "Reporting Entity"},
    {id: UltimateParentEntityRoleEnum.CBC803, name: "Both"}
  ] as ReferenceBook[];
  
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
  ] as ReferenceBook[];
  
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
  ] as ReferenceBook[];
  
  public messageTypes = [
    {id: MessageTypeEnum.CBC, name: "CBC"}
  ] as ReferenceBook[];
  
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
  ] as ReferenceBook[];
  
  public onGetNameSupportedSchema(
      supportedSchema: SupportedSchema
  ): string | undefined {
    const schema = _.find(this.supportedSchemas, x => x.id === supportedSchema);
    return schema ? schema.name : "";
  }
  
  public onGetNameUltimateParentEntityRole(
      ultimateParentEntityRole: UltimateParentEntityRoleEnum
  ): string | undefined {
    const role = _.find(this.ultimateParentEntityRoles, x => x.id === ultimateParentEntityRole);
    return role ? role.name : "";
  }

  public onGetCountryEnum(
      data: string | Country | Array<Country>
  ): CountryEnum | CountryEnum[] {
    if (Array.isArray(data)) {
      if (data.length === 0) Error("date is null");
      return data.map(d => this.getCountryEnum(d.alpha2Code));
    } else {
      if (!data) throw Error("code is null");
      return typeof data === "string"
          ? this.getCountryEnum(data)
          : this.getCountryEnum(data.alpha2Code);
    }
  }
  
  private getCountryEnum(code: string): CountryEnum {
    const key = _.find(Object.keys(CountryEnum), x => x === code);
    return CountryEnum[key as keyof typeof CountryEnum];
  }
  
  public onGetCurrencyEnum(data: string | Currency): CurrencyEnum {
    const code = typeof data === "string" ? data : data.code;
    if (code) {
      const key = _.find(Object.keys(CurrencyEnum), x => x === code);
      return CurrencyEnum[key as keyof typeof CurrencyEnum];
    }
    throw Error("code is null");
  }
  
  public onGetIdReferenceBook(data: ReferenceBook | ReferenceBook[]): number | number[] {
    if (!data) throw Error("data is null");
    return Array.isArray(data) ? data.map(d => d.id) : data.id;
  }
  
  private onCreateJurisdictionCode(
      data: CountryEnum | Array<CountryEnum>
  ): string {
    if (data === undefined) return "";
    if (Array.isArray(data))
      return data.length > 0 ? data.map(x => CountryEnum[x]).join("") : "";
    return CountryEnum[data];
  }
  
  private static get random(): number {
    const min = Math.ceil(0);
    const max = Math.floor(999999999);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  
  public onGetYear(date: string): number {
    if (!date) return 0;
    const [year, month, day] = date.split("-");
    return parseInt(year);
  }
  
  private static onGetDateTimeToString(): string {
    const date = moment(new Date());
    return `${date.year()}${date.month()}${date.day()}T${date.hour()}${date.minute()}${date.second()}`;
  }
  
  public onGenerateDocRefId(
      jurisdiction: CountryEnum,
      year: number | null,
      jurisdictions: CountryEnum | Array<CountryEnum>
  ): string {
    if (jurisdiction === undefined) return "";
    if (year === 0 || year === null) return "";
    if (jurisdictions === undefined) return "";
    
    return `${this.onCreateJurisdictionCode(
        jurisdiction
    )}${year}${this.onCreateJurisdictionCode(jurisdictions)}${CbcMixin.random}`;
  }
  
  public onGenerateMessageRefId(
      jurisdiction: CountryEnum | Array<CountryEnum>,
      year: number | null,
      jurisdictions: CountryEnum | Array<CountryEnum>
  ) {
    if (jurisdiction === undefined) return "";
    if (year === 0 || year === null) return "";
    if (jurisdictions === undefined) return "";
    
    return `${this.onCreateJurisdictionCode(
        jurisdiction
    )}${year}${this.onCreateJurisdictionCode(
        jurisdictions
    )}${CbcMixin.onGetDateTimeToString()}${CbcMixin.random}`;
  }
}
