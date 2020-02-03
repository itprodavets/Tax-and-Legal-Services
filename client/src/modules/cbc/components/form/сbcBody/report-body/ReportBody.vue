<template>
    <v-container class="pa-0">
        <DocSpecComponent v-model="$v.reportBody.doc.$model" :readonly="readonly"/>
        <v-row>
            <v-col cols="12" lg="3" md="3" class="pa-0 px-3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.reportBody.jurisdiction.$model"
                        :items="countries"
                        item-text="name"
                        label="Jurisdiction"
                        :disabled="readonly"
                        return-object
                ></v-autocomplete>
            </v-col>
        </v-row>
        <SummaryComponent v-model="$v.reportBody.summary.$model" :readonly="readonly"/>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";
	import SummaryComponent from "@/modules/cbc/components/form/сbcBody/report-body/Summary.vue";

	import {CbcMixin} from "@/modules/cbc/mixins";
	import {validationMixin} from "vuelidate";
	import {Country} from "@/modules/country/models/dto.model";
	import {Currency} from '@/modules/currency/models/dto.model';
	import {CountryEnum, Doc, ReportBody, Summary} from "@/modules/cbc/models";

	@Component({
		components: {
			DocSpecComponent,
			SummaryComponent
		},
		mixins: [validationMixin],
		validations: {
			reportBody: {
				doc: {},
				jurisdiction: {},
				summary: {}
			}
		}
	})
	export default class ReportBodyComponent extends Mixins(CbcMixin) {
		@Prop()
		public countries!: Country[];

		@Prop()
		public currencies!: Currency[];

		@Prop()
		public readonly!: boolean;

		@Watch("reportBody")
		onReportChanged(value: ReportBody, oldValue: ReportBody) {
			this.onSave();
		}

		public onSave() {
			if (this.$v.reportBody.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					doc: this.$data.reportBody.doc,
					jurisdiction: this.onGetCountryEnum(this.$data.reportBody.jurisdiction),
					summary: this.$data.reportBody.summary
				} as ReportBody;
				this.$emit("input", data);
			}
		}

		public data() {
			return {
				reportBody: {
					doc: {} as Doc,
					jurisdiction: {} as CountryEnum,
					summary: {} as Summary
				} as ReportBody
			};
		}
	}
</script>