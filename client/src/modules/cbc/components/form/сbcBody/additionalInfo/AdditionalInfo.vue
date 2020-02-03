<template>
    <v-container fluid>
        <DocSpecComponent :readonly="readonly"/>
        <v-row>
            <v-col cols="12" lg="4" md="4">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.language.$model"
                        :items="languages"
                        item-text="name"
                        label="Language"
                        :disabled="readonly"
                        :error-messages="languageErrors"
                        return-object
                        required
                        @input="$v.language.$touch()"
                        @blur="$v.language.$touch()"
                ></v-autocomplete>
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.resCountryCode.$model"
                        :items="countries"
                        item-text="name"
                        label="Res Country Code"
                        :disabled="readonly"
                        multiple
                        return-object
                ></v-autocomplete>
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.summaryRef.$model"
                        :items="summaryTypes"
                        item-text="name"
                        label="Summary Ref"
                        :disabled="readonly"
                        multiple
                        return-object
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="8" md="8">
                <v-textarea
                        dense
                        filled
                        v-model="$v.otherInfo.$model"
                        label="Other Info"
                        rows="10"
                        :error-messages="otherInfoErrors"
                        :disabled="readonly"
                ></v-textarea>
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";

	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

	import {Country} from "@/modules/country/models/dto.model";
	import {Language} from "@/modules/language/models/dto.model";
	import {CbcMixin} from "@/modules/cbc/mixins";

	@Component({
		components: {
			DocSpecComponent
		},
		mixins: [validationMixin],
		validations: {
			otherInfo: {
				maxLength: maxLength(4000)
			},
			language: {
				required
			},
			resCountryCode: {},
			summaryRef: {}
		}
	})
	export default class AdditionalInfoComponent extends Mixins(CbcMixin) {
		@Prop()
		public languages!: Language[];

		@Prop()
		public readonly countries!: Country[];

		@Prop()
		public readonly readonly!: boolean;

		public data() {
			return {
				otherInfo: "",
				language: "",
				resCountryCode: "",
				summaryRef: []
			};
		}

		public get languageErrors() {
			const errors: string[] = [];
			if (this.$v.language) {
				if (!this.$v.language.$dirty) return errors;
				if (!this.$v.language.required) {
					errors.push("The Language is required");
				}
			}
			return errors;
		}

		public get otherInfoErrors() {
			const errors: string[] = [];
			if (this.$v.otherInfo) {
				if (!this.$v.otherInfo.$dirty) return errors;
				if (!this.$v.otherInfo.maxLength) {
					errors.push("The maximum of 4’000 characters are allowed");
				}
			}
			return errors;
		}
	}
</script>
