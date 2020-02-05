import {LanguageEnum} from "@/modules/language/models";
import {Language} from "@/modules/language/models/dto.model";
import {Component, Vue} from "vue-property-decorator";

@Component({
	mounted() {
		this.$store.dispatch("language/list");
	}
})
export class LanguageMixin extends Vue {
	public get languages(): Array<Language> {
		return this.$store.state.language.entities as Array<Language>;
	}
	
	public getLanguagesByCodes(languageEnums: Array<LanguageEnum>): Array<Language> {
		const languages = languageEnums.map(x => this.getLanguageByCode(x));
		if (languages && languages.length > 0) return languages;
		else throw Error("codes not found");
	}
	
	public getLanguageByCode(languageEnum: LanguageEnum): Language {
		const language = this.languages.find(language => LanguageEnum[languageEnum] === language.code);
		if (language) return language;
		else throw Error("code not found");
	}
	
	public getNamesByLanguages(data: Language | Array<Language>): string {
		if (Array.isArray(data)) {
			return data.map(c => c.name).join(", ");
		} else return data.name;
	}
}