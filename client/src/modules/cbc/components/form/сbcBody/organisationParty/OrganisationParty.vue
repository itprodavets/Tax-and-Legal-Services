<template>
    <v-container class="pa-0">
        <v-row>
            <v-col cols="12" lg="3" md="3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.organisation.jurisdictions.$model"
                        :items="countries"
                        item-text="name"
                        label="Jurisdictions"
                        :disabled="readonly"
                        required
                        multiple
                        return-object
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

            <v-col cols="12" v-if="organisation.jurisdictions.length > 0">
                <v-switch v-model="isTinOrIn" label="TIN or IN" class="px-3"></v-switch>
            </v-col>

            <v-col cols="12" v-if="!isTinOrIn && organisation.jurisdictions.length > 0">
                <OrganisationTinComponent
                        v-model="$v.organisation.tin.$model"
                        :readonly="readonly"
                        :country="firstCountry"
                        :countries="organisation.jurisdictions"
                />
            </v-col>

            <v-col cols="12" v-if="isTinOrIn && organisation.jurisdictions.length > 0">
                <OrganisationInsComponent
                        v-model="$v.organisation.in.$model"
                        :readonly="readonly"
                        :countries="organisation.jurisdictions"
                />
            </v-col>

            <v-col cols="12" v-if="organisation.jurisdictions.length > 0">
                <AddressTypesComponent
                        v-model="$v.organisation.address.$model"
                        :readonly="readonly"
                        :countries="organisation.jurisdictions"
                />
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	import OrganisationTinComponent from "./OrganisationTin.vue";
	import OrganisationInsComponent from "./OrganisationIns.vue";
	import AddressTypesComponent from "./AddressTypes.vue";
	import {Country} from "@/modules/country/models/dto.model";
	import {Address, In, Organisation, Tin} from "@/modules/cbc/models";

	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";

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
	export default class OrganisationPartyComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public countries!: Country[];
		@Prop()
		public value!: Organisation;

		public data() {
			return {
				isTinOrIn: false,
				firstCountry: {} as Country,
				organisation: {
					id: Guid.create().toString(),
					jurisdictions: [] as Country[],
					tin: {} as Tin,
					in: [] as In[],
					name: [] as string[],
					address: [] as Address[]
				}
			};
		}

		public created() {
			if (this.value && this.value.id)
				this.$data.organisation = {
					id: this.value.id,
					jurisdictions: this.countries.filter(x => this.value.jurisdictions.find(y => y === x.id)),
					tin: this.value.tin,
					in: this.value.in,
					name: this.value.name,
					address: this.value.address
				};
		}

		@Watch("organisation", {deep: true})
		onOrganisationChanged(value: Organisation, oldValue: Organisation) {
			this.onSave();
		}

		@Watch("organisation.jurisdictions", {deep: true})
		onJurisdictionsChanged(values: Country[], oldValues: Country[]) {
			this.$data.firstCountry = values.length > 0 ? values[0] : "";
		}

		@Watch("isTinOrIn")
		onIsTinOrInChanged(value: boolean, oldValue: boolean) {
			this.$data.organisation.tin.tin = value ? "NOTIN" : "";
		}

		public onSave() {
			if (this.$v.organisation.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					id: this.$data.organisation.id,
					jurisdictions: this.onGetCountryEnum(this.$data.organisation.jurisdictions),
					tin: this.$data.organisation.tin,
					in: this.$data.organisation.in,
					name: this.$data.organisation.name,
					address: this.$data.organisation.address
				} as Organisation;
				this.$emit("input", data);
			}
		}
	}
</script>
