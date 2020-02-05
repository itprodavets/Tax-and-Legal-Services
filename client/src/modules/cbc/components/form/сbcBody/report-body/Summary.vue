<template>
	<v-row>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.unrelated.value.$model"
					type="number"
					step
					label="Unrelated"
					:disabled="readonly"
					:error-messages="unrelatedErrors"
					required
					@input="$v.summary.unrelated.value.$touch()"
					@blur="$v.summary.unrelated.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.unrelated.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="unrelatedCurrencyErrors"
					required
					@input="$v.summary.unrelated.currency.$touch()"
					@blur="$v.summary.unrelated.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.related.value.$model"
					type="number"
					step
					label="Related"
					:disabled="readonly"
					:error-messages="relatedErrors"
					required
					@input="$v.summary.related.value.$touch()"
					@blur="$v.summary.related.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.related.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="relatedCurrencyErrors"
					required
					@input="$v.summary.related.currency.$touch()"
					@blur="$v.summary.related.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.total.value.$model"
					type="number"
					step
					label="Total"
					:disabled="readonly"
					:error-messages="totalErrors"
					required
					@input="$v.summary.total.value.$touch()"
					@blur="$v.summary.total.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.total.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="totalCurrencyErrors"
					required
					@input="$v.summary.total.currency.$touch()"
					@blur="$v.summary.total.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.profitOrLoss.value.$model"
					type="number"
					step
					label="Profit Or Loss"
					:disabled="readonly"
					:error-messages="profitOrLossErrors"
					required
					@input="$v.summary.profitOrLoss.value.$touch()"
					@blur="$v.summary.profitOrLoss.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.profitOrLoss.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="profitOrLossCurrencyErrors"
					required
					@input="$v.summary.profitOrLoss.currency.$touch()"
					@blur="$v.summary.profitOrLoss.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.taxPaid.value.$model"
					type="number"
					step
					label="Tax Paid"
					:disabled="readonly"
					:error-messages="taxPaidErrors"
					required
					@input="$v.summary.taxPaid.value.$touch()"
					@blur="$v.summary.taxPaid.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.taxPaid.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="taxPaidCurrencyErrors"
					required
					@input="$v.summary.taxPaid.currency.$touch()"
					@blur="$v.summary.taxPaid.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.taxAccrued.value.$model"
					type="number"
					step
					label="Tax Accrued"
					:disabled="readonly"
					:error-messages="taxAccruedErrors"
					required
					@input="$v.summary.taxAccrued.value.$touch()"
					@blur="$v.summary.taxAccrued.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.taxAccrued.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="taxAccruedCurrencyErrors"
					required
					@input="$v.summary.taxAccrued.currency.$touch()"
					@blur="$v.summary.taxAccrued.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.capital.value.$model"
					type="number"
					step
					label="Capital"
					:disabled="readonly"
					:error-messages="capitalErrors"
					required
					@input="$v.summary.capital.value.$touch()"
					@blur="$v.summary.capital.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.capital.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="capitalCurrencyErrors"
					required
					@input="$v.summary.capital.currency.$touch()"
					@blur="$v.summary.capital.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.earnings.value.$model"
					type="number"
					step
					label="Earnings"
					:disabled="readonly"
					:error-messages="earningsErrors"
					required
					@input="$v.summary.earnings.value.$touch()"
					@blur="$v.summary.earnings.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.earnings.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="earningsCurrencyErrors"
					required
					@input="$v.summary.earnings.currency.$touch()"
					@blur="$v.summary.earnings.currency.$touch()"
			></v-autocomplete>
		</v-col>
		<v-col cols="12" lg="3" md="3">
			<v-text-field
					dense
					filled
					v-model="$v.summary.nbEmployees.$model"
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
					v-model="$v.summary.assets.value.$model"
					type="number"
					step
					label="Assets"
					:disabled="readonly"
					:error-messages="assetsErrors"
					required
					@input="$v.summary.assets.value.$touch()"
					@blur="$v.summary.assets.value.$touch()"
			></v-text-field>
			<v-autocomplete
					dense
					filled
					v-model="$v.summary.assets.currency.$model"
					:items="currencies"
					label="Currency"
					:disabled="readonly"
					:error-messages="assetsCurrencyErrors"
					required
					@input="$v.summary.assets.currency.$touch()"
					@blur="$v.summary.assets.currency.$touch()"
			></v-autocomplete>
		</v-col>
	</v-row>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {MonAmnt, Summary} from "@/modules/cbc/models";
	import {CurrencyEnum} from "@/modules/currency/models";
	import {Currency} from "@/modules/currency/models/dto.model";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			summary: {
				unrelated: {
					currency: {required},
					value: {required}
				},
				related: {
					currency: {required},
					value: {required}
				},
				total: {
					currency: {required},
					value: {required}
				},
				profitOrLoss: {
					currency: {required},
					value: {required}
				},
				taxPaid: {
					currency: {required},
					value: {required}
				},
				taxAccrued: {
					currency: {required},
					value: {required}
				},
				capital: {
					currency: {required},
					value: {required}
				},
				earnings: {
					currency: {required},
					value: {required}
				},
				nbEmployees: {required},
				assets: {
					currency: {required},
					value: {required}
				}
			}
		}
	})
	export default class SummaryComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public currencies!: Currency[];

		@Watch("summary")
		onSummaryChanged(value: Summary, oldValue: Summary) {
			this.onSave();
		}

		public data() {
			return {
				summary: {
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
				} as Summary
			};
		}

		public onSave() {
			if (this.$v.summary.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					unrelated: {
						currency: this.$data.summary.unrelated.currency,
						value: this.$data.summary.unrelated.value
					},
					related: {
						currency: this.$data.summary.related.currency,
						value: this.$data.summary.related.value
					},
					total: {
						currency: this.$data.summary.total.currency,
						value: this.$data.summary.total.value
					},
					profitOrLoss: {
						currency: this.$data.summary.profitOrLoss.currency,
						value: this.$data.summary.profitOrLoss.value
					},
					taxPaid: {
						currency: this.$data.summary.taxPaid.currency,
						value: this.$data.summary.taxPaid.value
					},
					taxAccrued: {
						currency:
						this.$data.summary.taxAccrued.currency,
						value: this.$data.summary.taxAccrued.value
					},
					capital: {
						currency: this.$data.summary.capital.currency,
						value: this.$data.summary.capital.value
					},
					earnings: {
						currency:
						this.$data.summary.earnings.currency,
						value: this.$data.summary.earnings.value
					},
					nbEmployees: this.$data.summary.nbEmployees,
					assets: {
						currency: this.$data.summary.assets.currency,
						value: this.$data.summary.assets.value
					}
				} as Summary;
				this.$emit("input", data);
			}
		}

		public get unrelatedErrors() {
			const errors: string[] = [];
			if (this.$v.summary.unrelated && this.$v.summary.unrelated.value) {
				if (!this.$v.summary.unrelated.value.$dirty) return errors;
				if (!this.$v.summary.unrelated.value.required) {
					errors.push("The Unrelated is required");
				}
			}
			return errors;
		}

		public get unrelatedCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.unrelated && this.$v.summary.unrelated.currency) {
				if (!this.$v.summary.unrelated.currency.$dirty) return errors;
				if (!this.$v.summary.unrelated.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get relatedErrors() {
			const errors: string[] = [];
			if (this.$v.summary.related && this.$v.summary.related.value) {
				if (!this.$v.summary.related.value.$dirty) return errors;
				if (!this.$v.summary.related.value.required) {
					errors.push("The Related is required");
				}
			}
			return errors;
		}

		public get relatedCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.related && this.$v.summary.related.currency) {
				if (!this.$v.summary.related.currency.$dirty) return errors;
				if (!this.$v.summary.related.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get totalErrors() {
			const errors: string[] = [];
			if (this.$v.summary.total && this.$v.summary.total.value) {
				if (!this.$v.summary.total.value.$dirty) return errors;
				if (!this.$v.summary.total.value.required) {
					errors.push("The Total is required");
				}
			}
			return errors;
		}

		public get totalCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.total && this.$v.summary.total.currency) {
				if (!this.$v.summary.total.currency.$dirty) return errors;
				if (!this.$v.summary.total.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get profitOrLossErrors() {
			const errors: string[] = [];
			if (this.$v.summary.profitOrLoss && this.$v.summary.profitOrLoss.value) {
				if (!this.$v.summary.profitOrLoss.value.$dirty) return errors;
				if (!this.$v.summary.profitOrLoss.value.required) {
					errors.push("The Profit Or Loss is required");
				}
			}
			return errors;
		}

		public get profitOrLossCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.profitOrLoss && this.$v.summary.profitOrLoss.currency) {
				if (!this.$v.summary.profitOrLoss.currency.$dirty) return errors;
				if (!this.$v.summary.profitOrLoss.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get taxPaidErrors() {
			const errors: string[] = [];
			if (this.$v.summary.taxPaid && this.$v.summary.taxPaid.value) {
				if (!this.$v.summary.taxPaid.value.$dirty) return errors;
				if (!this.$v.summary.taxPaid.value.required) {
					errors.push("The Tax Paid is required");
				}
			}
			return errors;
		}

		public get taxPaidCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.taxPaid && this.$v.summary.taxPaid.currency) {
				if (!this.$v.summary.taxPaid.currency.$dirty) return errors;
				if (!this.$v.summary.taxPaid.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get taxAccruedErrors() {
			const errors: string[] = [];
			if (this.$v.summary.taxAccrued && this.$v.summary.taxAccrued.value) {
				if (!this.$v.summary.taxAccrued.value.$dirty) return errors;
				if (!this.$v.summary.taxAccrued.value.required) {
					errors.push("The Tax Accrued is required");
				}
			}
			return errors;
		}

		public get taxAccruedCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.taxAccrued && this.$v.summary.taxAccrued.currency) {
				if (!this.$v.summary.taxAccrued.currency.$dirty) return errors;
				if (!this.$v.summary.taxAccrued.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get capitalErrors() {
			const errors: string[] = [];
			if (this.$v.summary.capital && this.$v.summary.capital.value) {
				if (!this.$v.summary.capital.value.$dirty) return errors;
				if (!this.$v.summary.capital.value.required) {
					errors.push("The Capital is required");
				}
			}
			return errors;
		}

		public get capitalCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.capital && this.$v.summary.capital.currency) {
				if (!this.$v.summary.capital.currency.$dirty) return errors;
				if (!this.$v.summary.capital.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get earningsErrors() {
			const errors: string[] = [];
			if (this.$v.summary.earnings && this.$v.summary.earnings.value) {
				if (!this.$v.summary.earnings.value.$dirty) return errors;
				if (!this.$v.summary.earnings.value.required) {
					errors.push("The Earnings is required");
				}
			}
			return errors;
		}

		public get earningsCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.earnings && this.$v.summary.earnings.currency) {
				if (!this.$v.summary.earnings.currency.$dirty) return errors;
				if (!this.$v.summary.earnings.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}

		public get nbEmployeesErrors() {
			const errors: string[] = [];
			if (this.$v.summary.nbEmployees) {
				if (!this.$v.summary.nbEmployees.$dirty) return errors;
				if (!this.$v.summary.nbEmployees.required) {
					errors.push("The Nb Employees is required");
				}
			}
			return errors;
		}

		public get assetsErrors() {
			const errors: string[] = [];
			if (this.$v.summary.assets && this.$v.summary.assets.value) {
				if (!this.$v.summary.assets.value.$dirty) return errors;
				if (!this.$v.summary.assets.value.required) {
					errors.push("The Assets is required");
				}
			}
			return errors;
		}

		public get assetsCurrencyErrors() {
			const errors: string[] = [];
			if (this.$v.summary.assets && this.$v.summary.assets.currency) {
				if (!this.$v.summary.assets.currency.$dirty) return errors;
				if (!this.$v.summary.assets.currency.required) {
					errors.push("The Currency is required");
				}
			}
			return errors;
		}
	}
</script>