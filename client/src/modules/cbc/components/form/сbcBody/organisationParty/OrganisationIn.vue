<template>
	<v-container fluid class="pa-0">
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
						@input="$v.identificationNumber.jurisdiction.$touch()"
						@blur="$v.identificationNumber.jurisdiction.$touch()"
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-text-field
						dense
						filled
						v-model="identificationNumber.in"
						label="IN"
						:disabled="readonly"
						:error-messages="inErrors"
						@input="$v.identificationNumber.in.$touch()"
						@blur="$v.identificationNumber.in.$touch()"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-text-field
						dense
						filled
						v-model="identificationNumber.type"
						label="Type"
						:disabled="readonly"
						:error-messages="typeErrors"
						@input="$v.identificationNumber.type.$touch()"
						@blur="$v.identificationNumber.type.$touch()"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-btn class="ma-2" tile outlined color="error" @click="onRemove()">
					<v-icon left>mdi-delete-circle</v-icon>
					Remove
				</v-btn>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {In} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Emit, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			identificationNumber: {
				in: {
					maxLength: maxLength(200)
				},
				type: {
					maxLength: maxLength(200)
				},
				jurisdiction: {
				}
			}
		}
	})
	export default class OrganisationInComponent extends Mixins(CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public readonly countries!: Country[];

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					type: "",
					in: "",
					jurisdiction: undefined
				} as In;
			}
		})
		public readonly identificationNumber!: In;

		public get value(): In {
			return this.identificationNumber;
		}

		public set value(identificationNumber: In) {
			if (this.$v!.identificationNumber!.$invalid) {
				this.$v!.identificationNumber!.$touch();
			} else
				this.$emit("update:identificationNumber", identificationNumber);
		}

		@Watch("identificationNumber", {deep: true})
		public onChanged(value: In, oldValue: In) {
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

		@Emit("remove")
		public onRemove() {
			return this.value.id;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.identificationNumber!.jurisdiction!.$dirty) return errors;
			return errors;
		}

		public get inErrors() {
			const errors: string[] = [];
			if (!this.$v!.identificationNumber!.in!.$dirty) return errors;
			!this.$v!.identificationNumber!.in!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		public get typeErrors() {
			const errors: string[] = [];
			if (!this.$v!.identificationNumber!.type!.$dirty) return errors;
			!this.$v!.identificationNumber!.type!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		private save(identificationNumber: In | any) {
			this.value = Object.assign(this.value, identificationNumber);
		}
	}
</script>