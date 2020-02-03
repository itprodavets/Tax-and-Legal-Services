<template>
    <v-data-table
            dense
            :headers="headers"
            :items="items"
            :items-per-page="5"
            class="elevation-1"
            @click:row="onClickRow"
    ></v-data-table>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {Report} from "@/modules/cbc/models";

	@Component({
		components: {}
	})
	export default class ReportListComponent extends Mixins() {
		@Prop()
		reports!: Report[];

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
				items: this.reports
			};
		}

		public onClickRow(row: Report) {
			this.$router.push({
				name: "constituent.entity",
				params: {reportId: row.id.toString()}
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
