<template>
	<v-container fluid class="pa-0">
		<div class="subtitle-1 text-uppercase mb-2 text-center">Taxpayer Identification Number</div>
		<v-divider class="mb-4"></v-divider>
		<v-row>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-autocomplete
						dense
						filled
						v-model="jurisdiction"
						:items="countries"
						item-text="name"
						label="Jurisdiction"
						:disabled="readonly"
						return-object
						:error-messages="jurisdictionErrors"
						@input="$v.tin.jurisdiction.$touch()"
						@blur="$v.tin.jurisdiction.$touch()"
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-text-field
						dense
						filled
						v-model="tin.tin"
						label="TIN"
						:disabled="readonly"
						:error-messages="tinErrors"
						@input="$v.tin.tin.$touch()"
						@blur="$v.tin.tin.$touch()"
				></v-text-field>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Tin} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";

	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			tin: {
				tin: {
					required,
					maxLength: maxLength(200)
				},
				jurisdiction: {
				}
			}
		}
	})
	export default class OrganisationInComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public readonly countries!: Country[];

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					tin: "",
					jurisdiction: undefined
				} as Tin;
			}
		})
		public readonly tin!: Tin;

		public get value(): Tin {
			return this.tin;
		}

		public set value(tin: Tin) {
			if (this.$v!.tin!.$invalid) {
				this.$v!.tin!.$touch();
			} else
				this.$emit("update:tin", tin);
		}

		@Watch("tin", {deep: true})
		public onChanged(value: Tin, oldValue: Tin) {
			this.value = value;
		}

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction)) {
				const countryEnum = CountryEnum[this.value.jurisdiction];
				return this.countries.find(x => x.alpha2Code === countryEnum)!;
			}
		}

		public set jurisdiction(country: Country | undefined) {
			if (this.value && country) this.save({jurisdiction: this.getCountryEnum(country)});
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.tin!.jurisdiction!.$dirty) return errors;
			return errors;
		}

		public get tinErrors() {
			const errors: string[] = [];
			if (!this.$v!.tin!.tin!.$dirty) return errors;
			!this.$v!.tin!.tin!.required && errors.push("The TIN is required");
			!this.$v!.tin!.tin!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		private save(tin: Tin | any) {
			this.value = Object.assign(this.value, tin);
		}
	}
</script>