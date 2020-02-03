<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col cols="12" lg="3" md="3">
				<v-autocomplete
						dense
						filled
						v-model="jurisdictions"
						:items="countries"
						item-text="name"
						label="Jurisdictions"
						:disabled="readonly"
						required
						multiple
						return-object
						:error-messages="jurisdictionsErrors"
						@input="$v.organisation.jurisdictions.$touch()"
						@blur="$v.organisation.jurisdictions.$touch()"
				></v-autocomplete>
			</v-col>

			<v-col cols="12" lg="9" md="9">
				<v-combobox
						dense
						filled
						v-model="$v.organisation.name.$model"
						:items="$v.organisation.name.$model"
						label="Name"
						multiple
						:disabled="readonly"
				></v-combobox>
			</v-col>

			<v-col cols="12">
				<v-switch v-model="organisation.hasTin" label="Has TIN" class="px-3"></v-switch>
			</v-col>

			<v-col cols="12" v-if="organisation.hasTin">
				<OrganisationTinComponent
						:tin.sync="$v.organisation.tin.$model"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>

			<v-col cols="12" v-if="!organisation.hasTin">
				<OrganisationInsComponent
						:ins.sync="$v.organisation.in.$model"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>

			<v-col cols="12">
				<AddressTypesComponent
						:addresses.sync="$v.organisation.address.$model"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Address, In, Organisation} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {Component, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";
	import AddressTypesComponent from "./AddressTypes.vue";
	import OrganisationInsComponent from "./OrganisationIns.vue";
	import OrganisationTinComponent from "./OrganisationTin.vue";

	@Component({
		components: {
			OrganisationTinComponent,
			OrganisationInsComponent,
			AddressTypesComponent
		},
		mixins: [validationMixin],
		validations: {
			organisation: {
				jurisdictions: {
					required
				},
				tin: {},
				in: {},
				name: {},
				address: {}
			}
		}
	})
	export default class OrganisationPartyComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@PropSync('organisation', {
			type: Object, default: () => {
				return {
					id: "",
					jurisdictions: [] as CountryEnum[],
					tin: {},
					name: [] as string[],
					address: [] as Address[],
					hasTin: true,
					in: [] as In[]
				} as Organisation;
			}
		})
		public value!: Organisation;

		public get jurisdictions(): Country[] | undefined {
			if (this.value && this.value.jurisdictions)
				return this.countries.filter(x => this.value.jurisdictions.find(y => CountryEnum[y] === x.alpha2Code));
		}

		public set jurisdictions(countries: Country[] | undefined) {
			if (countries)
				this.value = {
					id: this.value.id,
					jurisdictions: this.getCountryEnums(countries),
					name: this.value.name,
					tin: this.value.tin,
					in: this.value.in,
					hasTin: this.value.hasTin,
					address: this.value.address
				} as Organisation;
		}

		public get jurisdictionsErrors() {
			const errors: string[] = [];
			if (!this.$v!.organisation!.jurisdictions!.$dirty) return errors;
			!this.$v!.organisation!.jurisdictions!.required && errors.push("The jurisdictions is required");
			return errors;
		}
	}
</script>
