<template>
    <v-data-table
            dense
            :headers="headers"
            :items="items"
            :items-per-page="5"
            class="elevation-1"
            @click:row="onClickRow"
    >
        <template v-slot:item.version="{ item }">{{ onGetNameSupportedSchema(item.version) }}</template>
    </v-data-table>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {ReportData} from "../../models";
	import {CbcMixin} from "../../mixins";

	@Component({
		components: {}
	})
	export default class ReportDataListComponent extends Mixins(CbcMixin) {
		@Prop()
		reportData: ReportData[] | undefined;

		public data() {
			return {
				headers: [
					{
						text: "Id",
						sortable: false,
						value: "id"
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
