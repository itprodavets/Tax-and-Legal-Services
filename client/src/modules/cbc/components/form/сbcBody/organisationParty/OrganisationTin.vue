<template>
	<v-container fluid class="pa-0">
		<div class="subtitle-1 text-uppercase mb-2">Taxpayer Identification Number</div>
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
						v-model="$v.tin.tin.$model"
						label="TIN"
						required
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
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Tin} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";

	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Mixins, Prop, PropSync} from "vue-property-decorator";
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
					required
				}
			}
		}
	})
	export default class OrganisationInComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public readonly countries!: Country[];

		@PropSync('tin', {
			type: Object, default: () => {
				return {
					id: "",
					tin: "",
					jurisdiction: {}
				} as Tin;
			}
		})
		public value!: Tin;

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction))
				return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (country)
				this.value = {
					id: this.value.id,
					jurisdiction: this.getCountryEnum(country),
					tin: this.value.tin
				} as Tin;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.tin!.jurisdiction!.$dirty) return errors;
			!this.$v!.tin!.jurisdiction!.required && errors.push("The jurisdiction is required");

			return errors;
		}

		public get tinErrors() {
			const errors: string[] = [];
			if (!this.$v!.tin!.tin!.$dirty) return errors;
			!this.$v!.tin!.tin!.required && errors.push("The TIN is required");
			!this.$v!.tin!.tin!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}
	}
</script>