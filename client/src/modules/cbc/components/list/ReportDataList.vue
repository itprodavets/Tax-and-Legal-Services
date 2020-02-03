<template>
    <v-data-table
            dense
            :headers="headers"
            :items="items"
            :items-per-page="5"
            class="elevation-1"
            @click:row="onClickRow"
    >
        <template v-slot:item.message.jurisdiction="{ item }">{{
            getNamesByCountries(getCountriesByCodes(item.message.jurisdiction)) }}
        </template>
        <template v-slot:item.message.reportingPeriod="{ item }">
            {{new Date(item.message.reportingPeriod).getFullYear()}}
        </template>
        <template v-slot:item.version="{ item }">{{ onGetNameSupportedSchema(item.version) }}</template>
    </v-data-table>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {ReportData} from "../../models";
	import {CbcMixin} from "../../mixins";
	import {CountryMixin} from "@/modules/country/mixins";

	@Component({
		components: {}
	})
	export default class ReportDataListComponent extends Mixins(CbcMixin, CountryMixin) {
		@Prop()
		reportData!: ReportData[];

		public data() {
			return {
				headers: [
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
				],
				items: this.reportData
			};
		}

		public onClickRow(row: ReportData) {
			this.$router.push({
				name: "cbc.report.detail",
				params: {id: row.id.toString()}
			});
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
