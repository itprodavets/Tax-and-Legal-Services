<template>
    <v-container class="pa-0" :id="address.id">
        <v-row>
            <v-col cols="12" lg="9" md="9">
                <v-switch v-model="isAddressFreeOrFix" label="Address Free or Fix" class="px-3"></v-switch>
            </v-col>
            <v-col cols="12" lg="3" md="3">
                <v-btn class="ma-2" tile outlined color="success">
                    <v-icon left>mdi-plus-circle</v-icon>
                    Add
                </v-btn>
                <v-btn class="ma-2" tile outlined color="error">
                    <v-icon left>mdi-minus-circle</v-icon>
                    Remove
                </v-btn>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" lg="3" md="3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.address.jurisdiction.$model"
                        :items="countries"
                        item-text="name"
                        label="Country Code"
                        :disabled="readonly"
                        required
                        return-object
                        :error-messages="jurisdictionErrors"
                        @input="$v.address.jurisdiction.$touch()"
                        @blur="$v.address.jurisdiction.$touch()"
                ></v-autocomplete>
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.address.type.$model"
                        :items="addressTypes"
                        item-text="name"
                        label="Address Type"
                        :disabled="readonly"
                        return-object
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="9" md="9">
                <v-row>
                    <v-col cols="12" class="pa-0 px-3" v-if="!isAddressFreeOrFix">
                        <v-textarea
                                dense
                                filled
                                v-model="$v.address.free.$model"
                                label="Address Free"
                                rows="5"
                                :disabled="readonly"
                                :error-messages="addressFreeErrors"
                                @input="$v.address.free.$touch()"
                                @blur="$v.address.free.$touch()"
                        ></v-textarea>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.street.$model"
                                label="street"
                                :disabled="readonly"
                                :error-messages="streetErrors"
                                @input="$v.address.fix.street.$touch()"
                                @blur="$v.address.fix.street.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.buildingIdentifier.$model"
                                label="Building Identifier"
                                :disabled="readonly"
                                :error-messages="buildingIdentifierErrors"
                                @input="$v.address.fix.buildingIdentifier.$touch()"
                                @blur="$v.address.fix.buildingIdentifier.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.suiteIdentifier.$model"
                                label="Suite Identifier"
                                :disabled="readonly"
                                :error-messages="suiteIdentifierErrors"
                                @input="$v.address.fix.suiteIdentifier.$touch()"
                                @blur="$v.address.fix.suiteIdentifier.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.floorIdentifier.$model"
                                label="Floor Identifier"
                                :disabled="readonly"
                                :error-messages="floorIdentifierErrors"
                                @input="$v.address.fix.floorIdentifier.$touch()"
                                @blur="$v.address.fix.floorIdentifier.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.districtName.$model"
                                label="District Name"
                                :disabled="readonly"
                                :error-messages="districtNameErrors"
                                @input="$v.address.fix.districtName.$touch()"
                                @blur="$v.address.fix.districtName.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.pob.$model"
                                label="POB"
                                :disabled="readonly"
                                :error-messages="pobErrors"
                                @input="$v.address.fix.pob.$touch()"
                                @blur="$v.address.fix.pob.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.postCode.$model"
                                label="Post Code"
                                :disabled="readonly"
                                :error-messages="postCodeErrors"
                                @input="$v.address.fix.postCode.$touch()"
                                @blur="$v.address.fix.postCode.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.city.$model"
                                label="City"
                                :disabled="readonly"
                                required
                                :error-messages="cityErrors"
                                @input="$v.address.fix.city.$touch()"
                                @blur="$v.address.fix.city.$touch()"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="isAddressFreeOrFix">
                        <v-text-field
                                dense
                                filled
                                v-model="$v.address.fix.countrySubentity.$model"
                                label="Country Subentity"
                                :disabled="readonly"
                                :error-messages="countrySubentityErrors"
                                @input="$v.address.fix.countrySubentity.$touch()"
                                @blur="$v.address.fix.countrySubentity.$touch()"
                        ></v-text-field>
                    </v-col>
                </v-row>
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Guid} from "@/core/common/guid";

	import {Country} from "@/modules/country/models/dto.model";
	import {Address, AddressFix, AddressTypeEnum, CountryEnum} from "@/modules/cbc/models";
	import {ReferenceBook} from "@/core/models";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			address: {
				jurisdiction: {
					required
				},
				type: {},
				free: {},
				fix: {
					street: {},
					buildingIdentifier: {},
					suiteIdentifier: {},
					floorIdentifier: {},
					districtName: {},
					pob: {},
					postCode: {},
					city: {
						required
					},
					countrySubentity: {}
				}
			}
		}
	})
	export default class AddressTypeComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public country!: Country;
		@Prop()
		public countries!: Country[];
		public isAddressFreeOrFix: boolean = false;

		@Watch("address", {deep: true})
		onAddressChanged(value: Address, oldValue: Address) {
			this.onSave();
		}

		public data() {
			return {
				address: {
					id: Guid.create().toString(),
					jurisdiction: {} as Country,
					type: {} as ReferenceBook,
					fix: {
						street: "",
						buildingIdentifier: "",
						suiteIdentifier: "",
						floorIdentifier: "",
						districtName: "",
						pob: "",
						postCode: "",
						city: "",
						countrySubentity: ""
					},
					free: ""
				}
			};
		}

		public created() {
			this.$data.address.jurisdiction = this.country;
		}

		public onSave() {
			if (this.$v.address.jurisdiction)
				if (this.$v.address.jurisdiction.$invalid) {
					this.$v.$touch();
					return;
				}
			if (this.$v.address.fix)
				if (this.$v.address.fix.$invalid && this.isAddressFreeOrFix) {
					this.$v.$touch();
					return;
				}
			if (this.$v.address.free)
				if (this.$v.address.free.$invalid && !this.isAddressFreeOrFix) {
					this.$v.$touch();
					return;
				}
			const data = {
				id: this.$data.address.id,
				jurisdiction: this.onGetCountryEnum(this.$data.address.jurisdiction),
				type: this.$data.address.type,
				fix: this.isAddressFreeOrFix ? this.$data.address.fix : null,
				free: this.isAddressFreeOrFix ? null : this.$data.address.free
			} as Address;
			this.$emit("input", data);
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (this.$v.address.jurisdiction) {
				if (!this.$v.address.jurisdiction.$dirty) return errors;
				if (!this.$v.address.jurisdiction.required) {
					errors.push("The Country Code is required");
				}
			}
			return errors;
		}

		public get addressFreeErrors() {
			const errors: string[] = [];
			if (this.$v.address.free) {
				if (!this.$v.address.free.$dirty) return errors;
			}
			return errors;
		}

		public get streetErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.street) {
					if (!this.$v.address.fix.street.$dirty) return errors;
				}
			return errors;
		}

		public get buildingIdentifierErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.buildingIdentifier) {
					if (!this.$v.address.fix.buildingIdentifier.$dirty) return errors;
				}
			return errors;
		}

		public get suiteIdentifierErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.suiteIdentifier) {
					if (!this.$v.address.fix.suiteIdentifier.$dirty) return errors;
				}
			return errors;
		}

		public get floorIdentifierErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.floorIdentifier) {
					if (!this.$v.address.fix.floorIdentifier.$dirty) return errors;
				}
			return errors;
		}

		public get districtNameErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.districtName) {
					if (!this.$v.address.fix.districtName.$dirty) return errors;
				}
			return errors;
		}

		public get pobErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.pob) {
					if (!this.$v.address.fix.pob.$dirty) return errors;
				}
			return errors;
		}

		public get postCodeErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.postCode) {
					if (!this.$v.address.fix.postCode.$dirty) return errors;
				}
			return errors;
		}

		public get cityErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.city) {
					if (!this.$v.address.fix.city.$dirty) return errors;
					if (!this.$v.address.fix.city.required) {
						errors.push("The City is required");
					}
				}
			return errors;
		}

		public get countrySubentityErrors() {
			const errors: string[] = [];
			if (this.$v.address.fix)
				if (this.$v.address.fix.countrySubentity) {
					if (!this.$v.address.fix.countrySubentity.$dirty) return errors;
				}
			return errors;
		}
	}
</script>