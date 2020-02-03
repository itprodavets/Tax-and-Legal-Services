<template>
    <v-container class="pa-0">
        <v-row>
            <v-col cols="12" lg="3" md="3" class="pa-0 px-3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.in.jurisdiction.$model"
                        :items="countries"
                        item-text="name"
                        label="Jurisdiction"
                        :disabled="readonly"
                        return-object
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
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength} from "vuelidate/lib/validators";

	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Guid} from "@/core/common/guid";

	import {Country} from "@/modules/country/models/dto.model";
	import {CountryEnum, In} from "@/modules/cbc/models";

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
				jurisdiction: {}
			}
		}
	})
	export default class OrganisationInComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public country!: Country;
		@Prop()
		public countries!: Country[];

		@Watch("in", {deep: true})
		onInChanged(value: string, oldValue: string) {
			this.onSave();
		}

		public data() {
			return {
				in: {
					id: Guid.create().toString(),
					in: "",
					type: "",
					jurisdiction: {} as Country
				}
			};
		}

		public created() {
			this.$data.in.jurisdiction = this.country;
		}

		public onSave() {
			if (this.$v.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					in: this.$data.in,
					type: this.$data.type,
					jurisdiction: this.onGetCountryEnum(this.$data.in.jurisdiction)
				} as In;
				this.$emit("input", data);
			}
		}

		public get inErrors() {
			const errors: string[] = [];
			if (this.$v.in.in) {
				if (!this.$v.in.in.$dirty) return errors;
				if (!this.$v.in.in.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}

		public get typeErrors() {
			const errors: string[] = [];
			if (this.$v.in.type) {
				if (!this.$v.in.type.$dirty) return errors;
				if (!this.$v.in.type.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}
	}
</script>