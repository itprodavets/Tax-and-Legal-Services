import {Language, LanguageEnum} from "@/modules/language/models";
import {Component, Vue} from "vue-property-decorator";

@Component
export class LanguageEnumMixin extends Vue {
	
	public getLanguageEnums(languages: Array<Language>): LanguageEnum[] {
		if (languages.length === 0) throw Error("Languages is null");
		return languages.map(x => this.getLanguageEnum(x.code));
	}
	
	public getLanguageEnum(data: string | Language): LanguageEnum {
		const code = typeof data === "string" ? data : data.code;
		const key = Object.keys(LanguageEnum).find(x => x === code);
		if (key)
			return LanguageEnum[key as keyof typeof LanguageEnum];
		throw Error("Language enum not found");
	}
}
