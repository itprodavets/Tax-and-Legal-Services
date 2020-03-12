import {CbcReferenceBookMixin} from "@/modules/cbc/mixins/cbc-reference-book";
import {Component, Mixins} from "vue-property-decorator";

@Component
export class CbcMixin extends Mixins(CbcReferenceBookMixin) {
  
  protected random(min: number = 0, max: number) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }
  
  public onGenerateRefId(
      jurisdiction: string,
      year: number,
      jurisdictions: string,
      date: Date | undefined = undefined
  ) {
    if (!jurisdiction) return "";
    if (!year) return "";
    if (!jurisdictions) return "";
    
    let result = `${jurisdiction}${year}${jurisdictions}`;

    if (date)
      result += `${date.getFullYear()}${date.getMonth()}${date.getDay()}T${date.getHours()}${date.getMinutes()}${date.getSeconds()}`;
    return `${result}${this.random(0, 999999999)}`;
  }
}
