import {Country, CountryEnum} from "@/modules/country/models";
import {Component, Vue} from "vue-property-decorator";

@Component
export class CountryEnumMixin extends Vue {
	
	public getCountryEnums(countries: Array<Country>): CountryEnum[] {
		if (countries.length === 0) throw Error("Countries is null");
		return countries.map(x => this.getCountryEnum(x.alpha2Code));
	}
	
	public getCountryEnum(data: string | Country): CountryEnum {
		const code = typeof data === "string" ? data : data.alpha2Code;
		const key = Object.keys(CountryEnum).find(x => x === code);
		if (key)
			return CountryEnum[key as keyof typeof CountryEnum];
		throw Error("Country enum not found");
	}
}
