<template>
	<v-row>
		<v-col>
			<v-autocomplete
					:disabled="readonly"
					:error-messages="currencyErrors"
					:items="currencies"
					@blur="$v.summary.unrelated.currency.$touch()"
					@input="$v.summary.unrelated.currency.$touch()"
					dense
					filled
					item-text="name"
					label="Currency"
					required
					return-object
					v-model="currency"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.unrelated.value"
					type="number"
					step
					label="Unrelated"
					:disabled="readonly"
					:error-messages="unrelatedErrors"
					required
					@input="$v.summary.unrelated.value.$touch()"
					@blur="$v.summary.unrelated.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.related.value"
					type="number"
					step
					label="Related"
					:disabled="readonly"
					:error-messages="relatedErrors"
					required
					@input="$v.summary.related.value.$touch()"
					@blur="$v.summary.related.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.total.value"
					type="number"
					step
					label="Total"
					:disabled="readonly"
					:error-messages="totalErrors"
					required
					@input="$v.summary.total.value.$touch()"
					@blur="$v.summary.total.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.profitOrLoss.value"
					type="number"
					step
					label="Profit Or Loss"
					:disabled="readonly"
					:error-messages="profitOrLossErrors"
					required
					@input="$v.summary.profitOrLoss.value.$touch()"
					@blur="$v.summary.profitOrLoss.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.taxPaid.value"
					type="number"
					step
					label="Tax Paid"
					:disabled="readonly"
					:error-messages="taxPaidErrors"
					required
					@input="$v.summary.taxPaid.value.$touch()"
					@blur="$v.summary.taxPaid.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.taxAccrued.value"
					type="number"
					step
					label="Tax Accrued"
					:disabled="readonly"
					:error-messages="taxAccruedErrors"
					required
					@input="$v.summary.taxAccrued.value.$touch()"
					@blur="$v.summary.taxAccrued.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.capital.value"
					type="number"
					step
					label="Capital"
					:disabled="readonly"
					:error-messages="capitalErrors"
					required
					@input="$v.summary.capital.value.$touch()"
					@blur="$v.summary.capital.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.earnings.value"
					type="number"
					step
					label="Earnings"
					:disabled="readonly"
					:error-messages="earningsErrors"
					required
					@input="$v.summary.earnings.value.$touch()"
					@blur="$v.summary.earnings.value.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.nbEmployees"
					type="string"
					label="Nb Employees"
					:disabled="readonly"
					:error-messages="nbEmployeesErrors"
					required
					@input="$v.summary.nbEmployees.$touch()"
					@blur="$v.summary.nbEmployees.$touch()"
			></v-text-field>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="summary.assets.value"
					type="number"
					step
					label="Assets"
					:disabled="readonly"
					:error-messages="assetsErrors"
					required
					@input="$v.summary.assets.value.$touch()"
					@blur="$v.summary.assets.value.$touch()"
			></v-text-field>
		</v-col>
	</v-row>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {MonAmnt, Summary} from "@/modules/cbc/models";
	import {CurrencyEnumMixin} from "@/modules/currency/mixins/currency-enum";
	import {CurrencyEnum} from "@/modules/currency/models";
	import {Currency} from "@/modules/currency/models/dto.model";
	import _ from "lodash";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			summary: {
				unrelated: {
					value: {required},
					currency: {required}
				},
				related: {
					value: {required}
				},
				total: {
					value: {required}
				},
				profitOrLoss: {
					value: {required}
				},
				taxPaid: {
					value: {required}
				},
				taxAccrued: {
					value: {required}
				},
				capital: {
					value: {required}
				},
				earnings: {
					value: {required}
				},
				nbEmployees: {required},
				assets: {
					value: {required}
				}
			}
		}
	})
	export default class SummaryComponent extends Mixins(CbcMixin, CurrencyEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public currencies!: Currency[];

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					unrelated: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					related: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					total: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					profitOrLoss: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					taxPaid: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					taxAccrued: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					capital: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					earnings: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt,
					nbEmployees: "" as string,
					assets: {
						currency: {} as CurrencyEnum,
						value: ""
					} as MonAmnt
				} as Summary;
			}
		})
		public readonly summary!: Summary;

		public get value(): Summary {
			return this.summary;
		};

		public set value(summary: Summary) {
			if (this.$v!.summary!.$invalid) {
				this.$v!.summary!.$touch();
			} else
				this.$emit("update:summary", summary);
		};

		@Watch("summary", {deep: true})
		public onChanged(value: Summary, oldValue: Summary) {
			this.value = value;
		}

		public get currency(): Currency | undefined {
			if (this.value && this.value.unrelated && !_.isUndefined(this.value.unrelated.currency))
				return this.currencies.find(x => x.code === CurrencyEnum[this.value.unrelated.currency])!;
		}

		public set currency(currency: Currency | undefined) {
			if (currency) {
				const summary = this.value;
				summary.unrelated.currency = this.getCurrencyEnum(currency);
				summary.related.currency = this.getCurrencyEnum(currency);
				summary.total.currency = this.getCurrencyEnum(currency);
				summary.profitOrLoss.currency = this.getCurrencyEnum(currency);
				summary.taxPaid.currency = this.getCurrencyEnum(currency);
				summary.taxAccrued.currency = this.getCurrencyEnum(currency);
				summary.capital.currency = this.getCurrencyEnum(currency);
				summary.earnings.currency = this.getCurrencyEnum(currency);
				summary.assets.currency = this.getCurrencyEnum(currency);
				this.save(summary);
			}
		}

		public get currencyErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.unrelated!.currency!.$dirty) return errors;
			!this.$v!.summary!.unrelated!.currency!.required && errors.push("The Currency is required");
			return errors;
		}

		public get unrelatedErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.unrelated!.value!.$dirty) return errors;
			!this.$v!.summary!.unrelated!.value!.required && errors.push("The Unrelated is required");
			return errors;
		}

		public get relatedErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.related!.value!.$dirty) return errors;
			!this.$v!.summary!.related!.value!.required && errors.push("The Related is required");
			return errors;
		}

		public get totalErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.related!.value!.$dirty) return errors;
			!this.$v!.summary!.related!.value!.required && errors.push("The Total is required");
			return errors;
		}

		public get profitOrLossErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.profitOrLoss!.value!.$dirty) return errors;
			!this.$v!.summary!.profitOrLoss!.value!.required && errors.push("The Profit Or Loss is required");
			return errors;
		}

		public get taxPaidErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.taxPaid!.value!.$dirty) return errors;
			!this.$v!.summary!.taxPaid!.value!.required && errors.push("The Tax Paid is required");
			return errors;
		}

		public get taxAccruedErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.taxAccrued!.value!.$dirty) return errors;
			!this.$v!.summary!.taxAccrued!.value!.required && errors.push("The Tax Accrued is required");
			return errors;
		}

		public get capitalErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.capital!.value!.$dirty) return errors;
			!this.$v!.summary!.capital!.value!.required && errors.push("The Capital is required");
			return errors;
		}

		public get earningsErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.earnings!.value!.$dirty) return errors;
			!this.$v!.summary!.earnings!.value!.required && errors.push("The Earnings is required");
			return errors;
		}

		public get nbEmployeesErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.nbEmployees!.$dirty) return errors;
			!this.$v!.summary!.nbEmployees!.required && errors.push("The Nb Employees is required");
			return errors;
		}

		public get assetsErrors() {
			const errors: string[] = [];
			if (!this.$v!.summary!.assets!.value!.$dirty) return errors;
			!this.$v!.summary!.assets!.value!.required && errors.push("The Assets is required");
			return errors;
		}

		private save(summary: Summary | any) {
			this.value = Object.assign(this.value, summary);
		}
	}
</script>