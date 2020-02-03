<template>
    <v-container class="pa-0">
        <div class="subtitle-1 text-uppercase mb-2">Taxpayer Identification Number</div>
        <v-divider class="mb-4"></v-divider>
        <v-row>
            <v-col cols="12" lg="3" md="3" class="pa-0 px-3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.tin.jurisdiction.$model"
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
                        v-model="$v.tin.tin.$model"
                        label="TIN"
                        required
                        :disabled="readonly"
                ></v-text-field>
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Guid} from "@/core/common/guid";

	import {Country} from "@/modules/country/models/dto.model";
	import {CountryEnum, Tin} from "@/modules/cbc/models";

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
	export default class OrganisationInComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public country!: Country;
		@Prop()
		public countries!: Country[];

		@Prop()
		public value!: Tin;

		@Watch("tin", {deep: true})
		onTinChanged(value: Tin, oldValue: Tin) {
			this.onSave();
		}

		public data() {
			return {
				tin: {
					id: Guid.create().toString(),
					tin: "",
					jurisdiction: {} as Country
				}
			};
		}

		public created() {
			if (this.value && this.value.id)
				this.$data.tin = {
					id: this.value.id,
					tin: this.value.tin,
					jurisdiction: this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction]),
				};
			else
				this.$data.tin.jurisdiction = this.country;
		}

		public onSave() {
			if (this.$v.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					id: this.$data.tin.id,
					tin: this.$data.tin.tin,
					jurisdiction: this.onGetCountryEnum(this.$data.tin.jurisdiction)
				} as Tin;
				this.$emit("input", data);
			}
		}

		public get tinErrors() {
			const errors: string[] = [];
			if (this.$v.tin.tin) {
				if (!this.$v.tin.tin.$dirty) return errors;
				if (!this.$v.tin.tin.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}

		public get typeErrors() {
			const errors: string[] = [];
			if (this.$v.tin.type) {
				if (!this.$v.tin.type.$dirty) return errors;
				if (!this.$v.tin.type.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}
	}
</script>