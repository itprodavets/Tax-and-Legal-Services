<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col cols="12" lg="9" md="9">
				<v-switch class="px-3" label="Address Free or Fix" v-model="address.isFixed"></v-switch>
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
						label="Jurisdiction"
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
				<v-row v-if="!address.isFixed">
					<v-col class="pa-0 px-3" cols="12">
						<v-textarea
								dense
								filled
								v-model="address.free"
								label="Address Free"
								rows="5"
								:disabled="readonly"
								:error-messages="addressFreeErrors"
								@input="$v.address.free.$touch()"
								@blur="$v.address.free.$touch()"
						></v-textarea>
					</v-col>
				</v-row>
				<v-row v-if="address.isFixed">
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="streetErrors"
								@blur="$v.address.fix.street.$touch()"
								@input="$v.address.fix.street.$touch()"
								dense
								filled
								label="street"
								v-model="address.fix.street"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="buildingIdentifierErrors"
								@blur="$v.address.fix.buildingIdentifier.$touch()"
								@input="$v.address.fix.buildingIdentifier.$touch()"
								dense
								filled
								label="Building Identifier"
								v-model="address.fix.buildingIdentifier"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="suiteIdentifierErrors"
								@blur="$v.address.fix.suiteIdentifier.$touch()"
								@input="$v.address.fix.suiteIdentifier.$touch()"
								dense
								filled
								label="Suite Identifier"
								v-model="address.fix.suiteIdentifier"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="floorIdentifierErrors"
								@blur="$v.address.fix.floorIdentifier.$touch()"
								@input="$v.address.fix.floorIdentifier.$touch()"
								dense
								filled
								label="Floor Identifier"
								v-model="address.fix.floorIdentifier"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="districtNameErrors"
								@blur="$v.address.fix.districtName.$touch()"
								@input="$v.address.fix.districtName.$touch()"
								dense
								filled
								label="District Name"
								v-model="address.fix.districtName"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="2" md="2">
						<v-text-field
								:disabled="readonly"
								:error-messages="pobErrors"
								@blur="$v.address.fix.pob.$touch()"
								@input="$v.address.fix.pob.$touch()"
								dense
								filled
								label="POB"
								v-model="address.fix.pob"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="2" md="2">
						<v-text-field
								:disabled="readonly"
								:error-messages="postCodeErrors"
								@blur="$v.address.fix.postCode.$touch()"
								@input="$v.address.fix.postCode.$touch()"
								dense
								filled
								label="Post Code"
								v-model="address.fix.postCode"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="2" md="2">
						<v-text-field
								:disabled="readonly"
								:error-messages="cityErrors"
								@blur="$v.address.fix.city.$touch()"
								@input="$v.address.fix.city.$touch()"
								dense
								filled
								label="City"
								required
								v-model="address.fix.city"
						></v-text-field>
					</v-col>
					<v-col class="pa-0 px-3" cols="12" lg="3" md="3">
						<v-text-field
								:disabled="readonly"
								:error-messages="countrySubentityErrors"
								@blur="$v.address.fix.countrySubentity.$touch()"
								@input="$v.address.fix.countrySubentity.$touch()"
								dense
								filled
								label="Country Subentity"
								v-model="address.fix.countrySubentity"
						></v-text-field>
					</v-col>
				</v-row>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {ReferenceBook} from "@/core/models";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Address, AddressFix, AddressTypeEnum} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Emit, Mixins, Prop, Watch} from "vue-property-decorator";
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

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					type: undefined,
					jurisdiction: {},
					isFixed: false,
					fix: {
						buildingIdentifier: "",
						city: "",
						countrySubentity: "",
						districtName: "",
						floorIdentifier: "",
						pob: "",
						postCode: "",
						street: "",
						suiteIdentifier: ""
					} as AddressFix,
					free: ""
				} as Address;
			}
		})
		public readonly address!: Address;

		public get value(): Address {
			return this.address;
		}

		public set value(address: Address) {
			if (this.$v!.address!.jurisdiction!.$invalid)
				this.$v!.address!.$touch();
			if (!address.isFixed && this.$v!.address!.free!.$invalid)
				this.$v!.address!.$touch();
			else if (address.isFixed && this.$v!.address!.fix!.$invalid)
				this.$v!.address!.$touch();
			else
				this.$emit("update:address", address);
		}

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction)) return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (this.value && country) this.save({jurisdiction: this.getCountryEnum(country)});
		}

		public get type(): ReferenceBook<AddressTypeEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.type)) return this.addressTypes.find(x => x.id === this.value.type)!;
		}

		public set type(type: ReferenceBook<AddressTypeEnum> | undefined) {
			if (this.value && type) this.save({type: type.id});
		}

		@Watch("address", {deep: true})
		public onChanged(value: Address, oldValue: Address) {
			this.value = value;
		}

		@Emit("remove")
		public onRemove() {
			return this.value.id;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.address!.jurisdiction!.$dirty) return errors;
			!this.$v!.address!.jurisdiction!.required && errors.push("The Jurisdiction is required");
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

		private save(address: Address | any) {
			this.value = Object.assign(this.value, address);
		}
	}
</script>