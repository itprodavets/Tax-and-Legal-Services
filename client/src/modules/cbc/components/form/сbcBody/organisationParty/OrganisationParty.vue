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
						v-model="organisation.name"
						:items="organisation.name"
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
						v-bind:tin.sync="organisation.tin"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>

			<v-col cols="12" v-if="!organisation.hasTin">
				<OrganisationInsComponent
						v-bind:identificationNumbers.sync="organisation.in"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>

			<v-col cols="12">
				<AddressTypesComponent
						v-bind:addresses.sync="organisation.address"
						:readonly="readonly"
						:countries="countries"
				/>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Address, In, Organisation, Tin} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
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

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					jurisdictions: [] as CountryEnum[],
					tin: {} as Tin,
					name: [] as string[],
					address: [] as Address[],
					hasTin: true,
					in: [] as In[]
				} as Organisation;
			}
		})
		public readonly organisation!: Organisation;

		public get value(): Organisation {
			return this.organisation;
		}

		public set value(organisation: Organisation) {
			if (this.$v!.organisation!.$invalid) {
				this.$v!.organisation!.$touch();
			} else
				this.$emit("update:organisation", organisation);
		}

		@Watch("organisation", {deep: true})
		public onChanged(value: Organisation, oldValue: Organisation) {
			this.value = value;
		}

		public get jurisdictions(): Country[] {
			if (this.value && this.value.jurisdictions && this.value.jurisdictions.length > 0)
				return this.countries.filter(x => this.value.jurisdictions.find(y => CountryEnum[y] === x.alpha2Code));
			return [];
		}

		public set jurisdictions(countries: Country[]) {
			if (this.value && countries) this.save({jurisdictions: countries.length > 0 ? this.getCountryEnums(countries) : []});
		}

		public get jurisdictionsErrors() {
			const errors: string[] = [];
			if (!this.$v!.organisation!.jurisdictions!.$dirty) return errors;
			!this.$v!.organisation!.jurisdictions!.required && errors.push("The jurisdictions is required");
			return errors;
		}

		private save(organisation: Organisation | any) {
			this.value = Object.assign(this.value, organisation);
		}
	}
</script>
