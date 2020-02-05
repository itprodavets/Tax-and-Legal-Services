import {CbcReferenceBookMixin} from "@/modules/cbc/mixins/cbc-reference-book";
import moment from "moment";
import {Component, Mixins} from "vue-property-decorator";

@Component
export class CbcMixin extends Mixins(CbcReferenceBookMixin) {
  
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
      jurisdiction: string,
      year: number | null,
      jurisdictions: string
  ): string {
    
    if (jurisdiction === undefined) return "";
    if (year === 0 || year === null) return "";
    if (jurisdictions === undefined) return "";
    return `${jurisdiction}${year}${jurisdictions}${CbcMixin.random}`;
  }
  
  public onGenerateMessageRefId(
      jurisdiction: string,
      year: number | null,
      jurisdictions: string
  ) {
    if (jurisdiction === undefined) return "";
    if (year === 0 || year === null) return "";
    if (jurisdictions === undefined) return "";
    
    return `${jurisdiction}${year}${jurisdictions
    }${CbcMixin.onGetDateTimeToString()}${CbcMixin.random}`;
  }
}
