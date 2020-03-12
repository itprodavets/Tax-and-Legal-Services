<template>
	<v-data-table
			dense
			:headers="headers"
			:items="reportBody"
			class="elevation-0"
			hide-default-footer
			@click:row="onClickRow"
	>
		<template v-slot:top>
			<v-toolbar dense class="elevation-0">
				<template>
					<v-btn @click="onCreate()" dense icon>
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
				<v-toolbar-title>Reports</v-toolbar-title>
			</v-toolbar>
		</template>
		<template v-slot:item.jurisdiction="{ item }">
			<CompanyDisplayComponent :country="getCountryByCode(item.jurisdiction)" v-if="item.jurisdiction"/>
		</template>

		<template v-slot:item.summary.unrelated="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.unrelated" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.related="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.related" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.total="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.total" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.profitOrLoss="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.profitOrLoss" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.taxPaid="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.taxPaid" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.taxAccrued="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.taxAccrued" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.capital="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.capital" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.earnings="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.earnings" v-if="item.summary"/>
		</template>

		<template v-slot:item.summary.nbEmployees="{ item }">
			{{ item.summary ? Number(item.summary.nbEmployees).toLocaleString() : "" }}
		</template>

		<template v-slot:item.summary.assets="{ item }">
			<CurrencyDisplayComponent :monAmnt="item.summary.assets" v-if="item.summary"/>
		</template>
	</v-data-table>
</template>
<script lang="ts">
	import {ReportBody, ReportBodyCreateRequest} from "@/modules/cbc/models";
	import CompanyDisplayComponent from "@/modules/country/components/CompanyDisplay.vue";
	import {CountryMixin} from "@/modules/country/mixins";
	import CurrencyDisplayComponent from "@/modules/currency/components/CurrencyDisplay.vue";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {
			CompanyDisplayComponent,
			CurrencyDisplayComponent
		}
	})
	export default class ReportBodyListComponent extends Mixins(CountryMixin) {
		@Prop({default: () => []})
		public readonly reportBody!: ReportBody[];
		public headers: any[] = [
			{
				text: "Jurisdiction",
				value: "jurisdiction",
				align: "start",
				divider: true,
				width: 150
			},
			{
				text: "Unrelated",
				value: "summary.unrelated",
				align: "end"
			},
			{
				text: "Related",
				value: "summary.related",
				align: "end"
			},
			{
				text: "Total",
				value: "summary.total",
				align: "end"
			},
			{
				text: "Profit Or Loss",
				value: "summary.profitOrLoss",
				align: "end"
			},
			{
				text: "Tax Paid",
				value: "summary.taxPaid",
				align: "end"
			},
			{
				text: "Tax Accrued",
				value: "summary.taxAccrued",
				align: "end"
			},
			{
				text: "Capital",
				value: "summary.capital",
				align: "end"
			},
			{
				text: "Earnings",
				value: "summary.earnings",
				align: "end"
			},
			{
				text: "NB Employees",
				value: "summary.nbEmployees",
				align: "end"
			},
			{
				text: "Assets",
				value: "summary.assets",
				align: "end"
			}
		];

		@Emit("create")
		public onCreate() {
			return {
				reportId: this.$route.params["reportId"],
				reportBody: {} as ReportBody
			} as ReportBodyCreateRequest
		}

		@Emit("get-report-body")
		public onClickRow(row: ReportBody) {
			return row;
		}
	}
</script>