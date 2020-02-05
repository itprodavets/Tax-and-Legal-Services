<template>
	<v-data-table
			dense
			:headers="headers"
			:items="reportBody"
			hide-default-footer
			class="elevation-0"
	>
		<template v-slot:item.jurisdiction="{ item }">{{
			getNamesByCountries(getCountryByCode(item.jurisdiction)) }}
		</template>

		<template v-slot:item.summary.unrelated="{ item }">
			{{ Number(item.summary.unrelated.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.unrelated.currency))}}
		</template>

		<template v-slot:item.summary.related="{ item }">
			{{ Number(item.summary.related.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.related.currency))}}
		</template>

		<template v-slot:item.summary.total="{ item }">
			{{ Number(item.summary.total.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.total.currency))}}
		</template>

		<template v-slot:item.summary.profitOrLoss="{ item }">
			{{ Number(item.summary.profitOrLoss.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.profitOrLoss.currency))}}
		</template>

		<template v-slot:item.summary.taxPaid="{ item }">
			{{ Number(item.summary.taxPaid.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.taxPaid.currency))}}
		</template>

		<template v-slot:item.summary.taxAccrued="{ item }">
			{{ Number(item.summary.taxAccrued.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.taxAccrued.currency))}}
		</template>

		<template v-slot:item.summary.capital="{ item }">
			{{ Number(item.summary.capital.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.capital.currency))}}
		</template>

		<template v-slot:item.summary.earnings="{ item }">
			{{ Number(item.summary.earnings.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.earnings.currency))}}
		</template>

		<template v-slot:item.summary.nbEmployees="{ item }">
			{{ Number(item.summary.nbEmployees).toLocaleString() }}
		</template>

		<template v-slot:item.summary.assets="{ item }">
			{{ Number(item.summary.assets.value).toLocaleString() }}
			{{getNamesByCurrencies(getCurrencyByCode(item.summary.assets.currency))}}
		</template>
	</v-data-table>
</template>
<script lang="ts">
	import {ReportBody} from "@/modules/cbc/models";
	import {CountryMixin} from "@/modules/country/mixins";
	import {CurrencyMixin} from "@/modules/currency/mixins";
	import {Component, Mixins, Prop} from "vue-property-decorator";

	@Component
	export default class ReportBodyListComponent extends Mixins(CountryMixin, CurrencyMixin) {
		@Prop({default: () => []})
		public readonly reportBody!: ReportBody[];
		public headers: any[] = [
			{
				text: "Jurisdiction",
				value: "jurisdiction",
				align: "center"
			},
			{
				text: "Unrelated",
				value: "summary.unrelated",
				align: "right"
			},
			{
				text: "Related",
				value: "summary.related",
				align: "right"
			},
			{
				text: "Total",
				value: "summary.total",
				align: "right"
			},
			{
				text: "Profit Or Loss",
				value: "summary.profitOrLoss",
				align: "right"
			},
			{
				text: "Tax Paid",
				value: "summary.taxPaid",
				align: "right"
			},
			{
				text: "Tax Accrued",
				value: "summary.taxAccrued",
				align: "right"
			},
			{
				text: "Capital",
				value: "summary.capital",
				align: "right"
			},
			{
				text: "Earnings",
				value: "summary.earnings",
				align: "right"
			},
			{
				text: "NB Employees",
				value: "summary.nbEmployees",
				align: "right"
			},
			{
				text: "Assets",
				value: "summary.assets",
				align: "right"
			}
		];
	}
</script>