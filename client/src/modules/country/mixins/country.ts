import {CountryEnum} from "@/modules/country/models";
import {Country} from "@/modules/country/models/dto.model";
import {Component, Vue} from "vue-property-decorator";

@Component
export class CountryMixin extends Vue {
    public created() {
        this.$store.dispatch("country/list");
    }
    
    public get countries(): Array<Country> {
        return this.$store.state.country.entities as Array<Country>;
    }
    
    public getCountriesByCodes(data: CountryEnum | Array<CountryEnum>): Country | Array<Country> {
        if (Array.isArray(data)) {
            const _countries = this.countries.filter(country => data.find(code => CountryEnum[code] === country.alpha2Code));
            if (_countries && _countries.length > 0) return _countries;
            throw Error("codes not found");
        } else return this.getCountryByCode(data);
    }
    
    private getCountryByCode(code: CountryEnum): Country {
        const _country = this.countries.find(country => CountryEnum[code] === country.alpha2Code);
        if (_country) return _country;
        
        throw Error("code not found");
    }
    
    public getNamesByCountries(data: Country | Array<Country>): string {
        if (Array.isArray(data)) {
            return data.map(c => c.name).join(", ");
        } else return data.name;
    }
}
