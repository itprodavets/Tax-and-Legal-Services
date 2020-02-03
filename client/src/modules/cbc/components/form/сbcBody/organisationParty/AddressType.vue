<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col cols="12" lg="9" md="9">
				<v-switch v-model="$v.address.isFixed.$model" label="Address Free or Fix" class="px-3"></v-switch>
			</v-col>
			<v-col cols="12" lg="3" md="3">
				<v-btn class="ma-2" tile outlined color="error" @click="onRemove()">
					<v-icon left>mdi-delete-circle</v-icon>
					Remove
				</v-btn>
			</v-col>
		</v-row>
		<v-row>
			<v-col cols="12" lg="3" md="3">
				<v-autocomplete
						dense
						filled
						v-model="jurisdiction"
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
						v-model="type"
						:items="addressTypes"
						item-text="name"
						label="Address Type"
						:disabled="readonly"
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="9" md="9">
				<v-row>
					<v-col cols="12" class="pa-0 px-3" v-if="!$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="2" lg="2" md="2" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
					<v-col cols="3" lg="3" md="3" class="pa-0 px-3" v-if="$v.address.isFixed.$model">
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
	import {ReferenceBook} from "@/core/models";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Address, AddressTypeEnum} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {Component, Emit, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			address: {
				jurisdiction: {
					required
				},
				type: {},
				isFixed: {},
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
	export default class AddressTypeComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@PropSync('address', {
			type: Object, default: () => {
				return {
					id: "",
					type: {},
					jurisdiction: {},
					isFixed: false,
					fix: {},
					free: ""
				} as Address;
			}
		})
		public value!: Address;

		public get jurisdiction(): Country | undefined {
			if (this.value && this.value.jurisdiction)
				return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (country)
				this.createAddress(
					{
						id: this.value.id,
						jurisdiction: this.getCountryEnum(country),
						type: this.value.type,
						isFixed: this.value.isFixed,
						fix: this.value.fix,
						free: this.value.free
					} as Address
				);
		}

		public get type(): ReferenceBook<AddressTypeEnum> | undefined {
			if (this.value && this.value.type)
				return this.addressTypes.find(x => x.id === this.value.type)!;
		}

		public set type(type: ReferenceBook<AddressTypeEnum> | undefined) {
			if (type)
				this.createAddress(
					{
						id: this.value.id,
						jurisdiction: this.value.jurisdiction,
						type: type.id,
						isFixed: this.value.isFixed,
						fix: this.value.fix,
						free: this.value.free
					} as Address
				);
		}

		private createAddress(value: Address) {
			this.value = value;
		}

		@Emit("remove")
		public onRemove() {
			return this.value.id;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.jurisdiction!.$dirty) return errors;
			!this.$v!.address!.jurisdiction!.required && errors.push("The Country Code is required");
			return errors;
		}

		public get addressFreeErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.free!.$dirty) return errors;
			return errors;
		}

		public get streetErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.street!.$dirty) return errors;
			return errors;
		}

		public get buildingIdentifierErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.buildingIdentifier!.$dirty) return errors;
			return errors;
		}

		public get suiteIdentifierErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.suiteIdentifier!.$dirty) return errors;
			return errors;
		}

		public get floorIdentifierErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.floorIdentifier!.$dirty) return errors;
			return errors;
		}

		public get districtNameErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.districtName!.$dirty) return errors;
			return errors;
		}

		public get pobErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.pob!.$dirty) return errors;
			return errors;
		}

		public get postCodeErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.postCode!.$dirty) return errors;
			return errors;
		}

		public get cityErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.city!.$dirty) return errors;
			!this.$v!.address!.fix!.city!.required && errors.push("The City is required");
			return errors;
		}

		public get countrySubentityErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.fix!.countrySubentity!.$dirty) return errors;
			return errors;
		}
	}
</script>