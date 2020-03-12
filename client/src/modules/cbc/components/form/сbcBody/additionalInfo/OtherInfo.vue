<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col cols="6" lg="6" md="6">
				<v-autocomplete
						:disabled="readonly"
						:error-messages="languageErrors"
						:items="languages"
						@blur="$v.otherInfo.language.$touch()"
						@input="$v.otherInfo.language.$touch()"
						dense
						filled
						item-text="name"
						label="Language"
						return-object
						v-model="language"
				></v-autocomplete>
			</v-col>
			<v-col cols="6" lg="6" md="6">
				<v-btn class="ma-2" tile outlined color="error" @click="onRemove()">
					<v-icon left>mdi-delete-circle</v-icon>
					Remove
				</v-btn>
			</v-col>
			<v-col cols="12">
				<v-textarea
						:disabled="readonly"
						:error-messages="otherInfoErrors"
						@blur="$v.otherInfo.info.$touch()"
						@input="$v.otherInfo.info.$touch()"
						dense
						filled
						label="Other Info"
						rows="5"
						v-model="otherInfo.info"
				></v-textarea>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {OtherInfo} from "@/modules/cbc/models";
	import {LanguageEnumMixin} from "@/modules/language/mixins";
	import {Language, LanguageEnum} from "@/modules/language/models";
	import _ from "lodash";
	import {Component, Emit, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength} from "vuelidate/lib/validators";

	@Component({
		mixins: [validationMixin],
		validations: {
			otherInfo: {
				info: {
					maxLength: maxLength(4000)
				},
				language: {}
			}
		}
	})
	export default class OtherInfoComponent extends Mixins(LanguageEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly languages!: Language[];
		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					language: undefined,
					info: ""
				} as OtherInfo;
			}
		})
		public readonly otherInfo!: OtherInfo;

		public get value(): OtherInfo {
			return this.otherInfo;
		}

		public set value(otherInfo: OtherInfo) {
			if (this.$v!.otherInfo!.$invalid) {
				this.$v!.otherInfo!.$touch();
			} else
				this.$emit("update:otherInfo", otherInfo);
		}

		@Watch("otherInfo", {deep: true})
		public onChanged(value: OtherInfo, oldValue: OtherInfo) {
			this.value = value;
		}

		public get language(): Language | undefined {
			if (this.value && !_.isUndefined(this.value.language)) {
				const languageEnum = LanguageEnum[this.value.language];
				return this.languages.find(y => y.code === languageEnum)
			}
		}

		public set language(language: Language | undefined) {
			if (language) this.save({language: this.getLanguageEnum(language)});
		}

		@Emit("remove")
		public onRemove() {
			return this.value.id;
		}

		public get languageErrors() {
			const errors: string[] = [];
			if (!this.$v!.otherInfo!.language!.$dirty) return errors;
			return errors;
		}

		public get otherInfoErrors() {
			const errors: string[] = [];
			if (!this.$v!.otherInfo!.info!.$dirty) return errors;
			!this.$v!.otherInfo!.info!.maxLength && errors.push("The maximum of 4’000 characters are allowed");
			return errors;
		}

		private save(otherInfo: OtherInfo | any) {
			this.value = Object.assign(this.value, otherInfo);
		}
	}
</script>