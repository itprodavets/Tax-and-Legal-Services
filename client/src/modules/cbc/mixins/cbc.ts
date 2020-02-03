import {CbcReferenceBookMixin} from "@/modules/cbc/mixins/cbc-reference-book";
import {CountryEnum} from "@/modules/country/models";
import {Component, Mixins, Vue} from "vue-property-decorator";
import {ReferenceBook} from "@/core/models";
import {
  AddressTypeEnum,
  BizActivityTypeEnum,
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
export class CbcMixin extends Mixins(CbcReferenceBookMixin) {
  
  public onGetNameSupportedSchema(
      supportedSchema: SupportedSchema | string
  ): string | undefined {
    let schema;
    if (typeof supportedSchema === "string")
      schema = _.find(this.supportedSchemas, x => SupportedSchema[x.id] === supportedSchema);
    else
      schema = _.find(this.supportedSchemas, x => x.id === supportedSchema);
    return schema ? schema.name : "";
  }
  
  public onGetNameReportingRoleEnum(
      reportingRoleEnum: ReportingRoleEnum
  ): string | undefined {
    const role = _.find(this.reportingRoles, x => x.id === reportingRoleEnum);
    return role ? role.name : "";
  }
  
  public onGetNameUltimateParentEntityRole(
      ultimateParentEntityRole: UltimateParentEntityRoleEnum
  ): string | undefined {
    const role = _.find(this.ultimateParentEntityRoles, x => x.id === ultimateParentEntityRole);
    return role ? role.name : "";
  }
  
  public onGetCurrencyEnum(data: string | Currency): CurrencyEnum {
    const code = typeof data === "string" ? data : data.code;
    if (code) {
      const key = _.find(Object.keys(CurrencyEnum), x => x === code);
      return CurrencyEnum[key as keyof typeof CurrencyEnum];
    }
    throw Error("code is null");
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
