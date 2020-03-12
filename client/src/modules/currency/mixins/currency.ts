import {Currency, CurrencyEnum} from "@/modules/currency/models";
import {Component, Vue} from "vue-property-decorator";

@Component({
	mounted() {
		this.$store.dispatch("currency/list");
	}
})
export class CurrencyMixin extends Vue {
	public get currencies(): Array<Currency> {
		return this.$store.state.currency.entities as Array<Currency>;
	}
	
	public getCurrenciesByCodes(currencyEnums: Array<CurrencyEnum>): Array<Currency> {
		const currencies = currencyEnums.map(x => this.getCurrencyByCode(x));
		if (currencies && currencies.length > 0) return currencies;
		else throw Error("codes not found");
	}
	
	public getCurrencyByCode(currencyEnum: CurrencyEnum): Currency {
		const currency = this.currencies.find(currency => CurrencyEnum[currencyEnum] === currency.code);
		if (currency) return currency;
		else throw Error("code not found");
	}
	
	public getNamesByCurrencies(data: Currency | Array<Currency>): string {
		if (Array.isArray(data)) {
			return data.map(c => c.name).join(", ");
		} else return data.name;
	}
}