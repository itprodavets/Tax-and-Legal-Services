<template>
	<v-data-table
			dense
			:headers="headers"
			:items="reportData"
			:items-per-page="5"
			class="elevation-1"
			@click:row="onClickRow"
	>
		<template v-slot:item.message.jurisdiction="{ item }">
			{{ getNamesByCountries(getCountryByCode(item.message.jurisdiction))}}
		</template>
		<template v-slot:item.message.reportingPeriod="{ item }">
			{{new Date(item.message.reportingPeriod).getFullYear()}}
		</template>
		<template v-slot:item.version="{ item }">{{ onGetNameSupportedSchema(item.version) }}</template>
	</v-data-table>
</template>
<script lang="ts">
	import {CountryMixin} from "@/modules/country/mixins";
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {CbcMixin} from "../../mixins";
	import {ReportData, SupportedSchema} from "../../models";

	@Component({
		components: {}
	})
	export default class ReportDataListComponent extends Mixins(CbcMixin, CountryMixin) {
		@Prop()
		public readonly reportData!: ReportData[];
		public headers: any[] = [
			{
				text: "Name",
				value: "message.refId"
			},
			{
				text: "Jurisdiction",
				value: "message.jurisdiction"
			},
			{
				text: "Reporting Period",
				value: "message.reportingPeriod"
			},
			{
				text: "Version",
				value: "version"
			}
		];

		public onClickRow(row: ReportData) {
			this.$router.push({
				name: "cbc.report.detail",
				params: {id: row.id.toString()}
			});
		}

		public onGetNameSupportedSchema(supportedSchema: SupportedSchema): string | undefined {
			return this.supportedSchemas.find(x => x.id === supportedSchema)!.name!;
		}
	}
</script>
<style lang="scss">
	table {
		tr {
			cursor: pointer;
		}
	}
</style>
