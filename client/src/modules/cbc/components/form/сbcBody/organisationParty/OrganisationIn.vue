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
						@input="$v.in.jurisdiction.$touch()"
						@blur="$v.in.jurisdiction.$touch()"
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-text-field
						dense
						filled
						v-model="$v.in.in.$model"
						label="IN"
						:disabled="readonly"
						:error-messages="inErrors"
						@input="$v.in.in.$touch()"
						@blur="$v.in.in.$touch()"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-text-field
						dense
						filled
						v-model="$v.in.type.$model"
						label="Type"
						:disabled="readonly"
						:error-messages="typeErrors"
						@input="$v.in.type.$touch()"
						@blur="$v.in.type.$touch()"
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
	import {In} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Emit, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			in: {
				in: {
					maxLength: maxLength(200)
				},
				type: {
					maxLength: maxLength(200)
				},
				jurisdiction: {
					required
				}
			}
		}
	})
	export default class OrganisationInComponent extends Mixins(CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public readonly countries!: Country[];

		@PropSync('in', {
			type: Object, default: () => {
				return {
					id: "",
					type: {},
					jurisdiction: {},
					in: ""
				} as In;
			}
		})
		public value!: In;

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction))
				return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (country)
				this.value = {
					id: this.value.id,
					jurisdiction: this.getCountryEnum(country),
					in: this.value.in
				} as In;
		}

		@Emit("remove")
		public onRemove() {
			return this.value.id;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.in!.jurisdiction!.$dirty) return errors;
			!this.$v!.in!.jurisdiction!.required && errors.push("The jurisdiction is required");
			return errors;
		}

		public get inErrors() {
			const errors: string[] = [];
			if (!this.$v!.in!.in!.$dirty) return errors;
			!this.$v!.in!.in!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		public get typeErrors() {
			const errors: string[] = [];
			if (!this.$v!.in!.type!.$dirty) return errors;
			!this.$v!.in!.type!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}
	}
</script>