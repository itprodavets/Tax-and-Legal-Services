import {CountryEnum} from "@/modules/country/models";
import {Country} from "@/modules/country/models/dto.model";
import {Component, Vue} from "vue-property-decorator";

@Component({
    mounted() {
        this.$store.dispatch("country/list");
    }
})
export class CountryMixin extends Vue {
    public get countries(): Array<Country> {
        return this.$store.state.country.entities as Array<Country>;
    }
    
    public getCountriesByCodes(countryEnums: Array<CountryEnum>): Array<Country> {
        const countries = countryEnums.map(x => this.getCountryByCode(x));
        if (countries && countries.length > 0) return countries;
        else throw Error("codes not found");
    }
    
    public getCountryByCode(countryEnum: CountryEnum): Country {
        const country = this.countries.find(country => CountryEnum[countryEnum] === country.alpha2Code);
        if (country) return country;
        else throw Error("code not found");
    }
    
    public getNamesByCountries(data: Country | Array<Country>): string {
        if (Array.isArray(data)) {
            return data.map(c => c.name).join(", ");
        } else return data.name;
    }
}
