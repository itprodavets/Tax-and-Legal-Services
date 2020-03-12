<template>
	<v-data-table
			dense
			:headers="headers"
			:items="reports"
			class="elevation-1"
			hide-default-footer
			@click:row="onClickRow"
	>

		<template v-slot:top>
			<v-toolbar dense class="elevation-0">
				<template>
					<v-btn dense icon @click="onCreate()">
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
				<v-toolbar-title>Reports</v-toolbar-title>
			</v-toolbar>
		</template>

		<template v-slot:item.reportingEntity.organisation.name="{ item }">
			{{item.reportingEntity ? item.reportingEntity.organisation ? item.reportingEntity.organisation.name.join(",
			"): "" : ""}}
		</template>
		<template v-slot:item.reportingEntity.role="{ item }">
			{{ item.reportingEntity ? onGetNameReportingRoleEnum(item.reportingEntity.role) : ""}}
		</template>
		<template v-slot:item.reportingEntity.startDate="{ item }">
			{{ item.reportingEntity ? onGetDate(item.reportingEntity.startDate) : "" }}
		</template>
		<template v-slot:item.reportingEntity.endDate="{ item }">
			{{ item.reportingEntity ? onGetDate(item.reportingEntity.endDate) : "" }}
		</template>
	</v-data-table>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Report, ReportCreateRequest, ReportingEntity, ReportingRoleEnum} from "@/modules/cbc/models";
	import _ from "lodash";
	import moment from "moment";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class ReportListComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly reports!: Report[];

		public headers: any[] = [
			{
				text: "Organisation",
				value: "reportingEntity.organisation.name",
				align: "start"
			},
			{
				text: "Tin",
				value: "reportingEntity.organisation.tin.tin",
				align: "start"
			},
			{
				text: "Name MNE Group",
				value: "reportingEntity.nameMNEGroup",
				align: "start"
			},
			{
				text: "Role",
				value: "reportingEntity.role",
				align: "start"
			},
			{
				text: "Start Date",
				value: "reportingEntity.startDate",
				align: "start"
			},
			{
				text: "End Date",
				value: "reportingEntity.endDate",
				align: "start"
			}
		];

		public onClickRow(row: Report) {
			this.$router.push({
				name: "constituent.entity",
				params: {reportId: row.id.toString()}
			});
		}

		public onGetDate(date: Date) {
			return moment(date).format('L')!;
		}

		public onGetNameReportingRoleEnum(reportingRoleEnum: ReportingRoleEnum): string | undefined {
			if (!_.isUndefined(reportingRoleEnum))
				return this.reportingRoles.find(x => x.id === reportingRoleEnum)!.name!;
		}

		@Emit("create")
		public onCreate() {
			return {
				reportDataId: this.$route.params["id"],
				report: {
					id: Guid.create().toString(),
					reportingEntity: {} as ReportingEntity,
					reports: [],
					additionalInfo: [],
					constituentEntities: []
				}
			} as ReportCreateRequest;
		}
	}
</script>
