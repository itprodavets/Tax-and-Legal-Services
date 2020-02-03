<template>
    <v-data-table
            dense
            :headers="headers"
            :items="items"
            :items-per-page="5"
            class="elevation-1"
            @click:row="onClickRow"
    >
        <template v-slot:item.reportingEntity.organisation.name="{ item }">
            {{item.reportingEntity.organisation.name.join(", ")}}
        </template>
        <template v-slot:item.reportingEntity.role="{ item }">
            {{ onGetNameReportingRoleEnum(item.reportingEntity.role)}}
        </template>
        <template v-slot:item.reportingEntity.startDate="{ item }">{{ onGetDate(item.reportingEntity.startDate) }}
        </template>
        <template v-slot:item.reportingEntity.endDate="{ item }">{{ onGetDate(item.reportingEntity.endDate) }}
        </template>
    </v-data-table>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Report} from "@/modules/cbc/models";
	import moment from "moment";
	import {Component, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class ReportListComponent extends Mixins(CbcMixin) {
		@Prop()
		reports!: Report[];

		public data() {
			return {
				headers: [
					{
						text: "Organisation",
						value: "reportingEntity.organisation.name"
					},
					{
						text: "Tin",
						value: "reportingEntity.organisation.tin.tin"
					},
					{
						text: "Name MNE Group",
						value: "reportingEntity.nameMNEGroup"
					},
					{
						text: "Role",
						value: "reportingEntity.role"
					},
					{
						text: "Start Date",
						value: "reportingEntity.startDate"
					},
					{
						text: "End Date",
						value: "reportingEntity.endDate"
					},

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

		public onGetDate(date: Date) {
			return moment(date).format('L');
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
