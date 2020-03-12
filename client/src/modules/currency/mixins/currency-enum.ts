import {Currency, CurrencyEnum} from "@/modules/currency/models";
import {Component, Vue} from "vue-property-decorator";

@Component
export class CurrencyEnumMixin extends Vue {
	public getCurrencyEnums(currencies: Array<Currency>): CurrencyEnum[] {
		if (currencies.length === 0) throw Error("Currencies is null");
		return currencies.map(x => this.getCurrencyEnum(x.code));
	}
	
	public getCurrencyEnum(data: string | Currency): CurrencyEnum {
		const code = typeof data === "string" ? data : data.code;
		const key = Object.keys(CurrencyEnum).find(x => x === code);
		if (key)
			return CurrencyEnum[key as keyof typeof CurrencyEnum];
		throw Error("Currency enum not found");
	}
}