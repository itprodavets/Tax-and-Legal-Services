<template>
	<v-container fluid>
		<DocSpecComponent v-bind:doc.sync="reportBody.doc" :readonly="readonly"/>
		<v-row>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-autocomplete
						dense
						filled
						:error-messages="jurisdictionErrors"
						:items="countries"
						item-text="name"
						label="Jurisdiction"
						:disabled="readonly"
						@blur="$v.reportBody.jurisdiction.$touch()"
						return-object
						@input="$v.reportBody.jurisdiction.$touch()"
						required
						v-model="jurisdiction"
				></v-autocomplete>
			</v-col>
		</v-row>
		<SummaryComponent :currencies="currencies" :readonly="readonly"
		                  v-bind:summary.sync="reportBody.summary"/>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";
	import SummaryComponent from "@/modules/cbc/components/form/сbcBody/report-body/Summary.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {ReportBody} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {Currency} from '@/modules/currency/models/dto.model';
	import _ from "lodash";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	@Component({
		components: {
			DocSpecComponent,
			SummaryComponent
		},
		mixins: [validationMixin],
		validations: {
			reportBody: {
				doc: {},
				jurisdiction: {
					required
				},
				summary: {}
			}
		}
	})
	export default class ReportBodyComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public readonly countries!: Country[];

		@Prop()
		public readonly currencies!: Currency[];

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					doc: {},
					jurisdiction: {},
					summary: {}
				} as ReportBody;
			}
		})
		public readonly reportBody!: ReportBody;

		public get value(): ReportBody {
			return this.reportBody;
		}

		public set value(reportBody: ReportBody) {
			if (this.$v!.reportBody!.$invalid) {
				this.$v!.reportBody!.$touch();
			} else
				this.$emit("update:reportBody", reportBody);
		}

		@Watch("reportBody", {deep: true})
		public onChanged(value: ReportBody, oldValue: ReportBody) {
			this.value = value;
		}

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction))
				return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (this.value && country) this.save({jurisdiction: this.getCountryEnum(country)});
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.reportBody!.jurisdiction!.$dirty) return errors;
			!this.$v!.reportBody!.jurisdiction!.required && errors.push("The Jurisdiction is required");
			return errors;
		}

		private save(reportBody: ReportBody | any) {
			this.value = Object.assign(this.value, reportBody);
		}
	}
</script>