<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col class="pa-0">
				<v-tooltip bottom>
					<template v-slot:activator="{ on }">
					<span v-on="on">{{ `${value} ${symbol}`}}</span>
					</template>
					<span>{{`${value} ${name}`}}</span>
				</v-tooltip>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {CurrencyMixin} from "@/modules/currency/mixins";
	import {Currency, CurrencyEnum} from "@/modules/currency/models";
	import {Component, Mixins, Prop} from "vue-property-decorator";

	interface MonAmnt {
		currency: CurrencyEnum;
		value: string;
	}

	@Component
	export default class CurrencyDisplayComponent extends Mixins(CurrencyMixin) {
		@Prop()
		public readonly monAmnt!: MonAmnt;

		public get value() {
			return Number(this.monAmnt.value).toLocaleString();
		}

		public get symbol() {
			return this.onGetCurrencyByCode(this.monAmnt.currency).symbol;
		}

		public get name() {
			const currency = this.onGetCurrencyByCode(this.monAmnt.currency);
			return `${currency.name} (${currency.code})`;
		}

		private _currencyEnum!: CurrencyEnum;
		private _currency!: Currency;

		public onGetCurrencyByCode(currencyEnum: CurrencyEnum): Currency {
			if (this._currencyEnum !== currencyEnum) {
				this._currencyEnum = currencyEnum;
				this._currency = this.getCurrencyByCode(currencyEnum);
			}
			return this._currency;
		}
	}
</script>