<template>
	<v-container fluid class="pa-0">
		<ReportDataListComponent
				:reportData="this.$store.state.cbc.entities"
				@create="onCreate"
				@parse="onParse"
				@validate="onValidate"
		/>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import ReportDataListComponent from "@/modules/cbc/components/form/list/ReportDataList.vue";
	import {
		Report,
		ReportCreateRequest,
		ReportDataCreateReportRequest,
		ReportDataCreateRequest,
		ReportDataParseRequest,
		ReportDataValidationRequest,
		ReportingEntity
	} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			ReportDataListComponent
		},
		mounted() {
			this.$store.dispatch("cbc/list");
		}
	})
	export default class ReportDataListView extends Vue {

		public onCreate(request: ReportDataCreateRequest) {
			this.$store.dispatch("cbc/create", request).then(id => {
				const reportCreateRequest = {
					reportDataId: id,
					report: {
						id: Guid.create().toString(),
						reportingEntity: {} as ReportingEntity,
						reports: [],
						additionalInfo: [],
						constituentEntities: []
					}
				} as ReportCreateRequest;
				this.$store.dispatch("cbc/report/create", reportCreateRequest).then(id => {
					const reportDataCreateReportRequest = {
						id: reportCreateRequest.reportDataId,
						report: Object.assign(reportCreateRequest.report, {id: id}) as Report
					} as ReportDataCreateReportRequest;
					this.$store.dispatch("cbc/create_report", reportDataCreateReportRequest).then(() => {
						this.$router.push({
							name: "constituent.entity",
							params: {
								id: reportDataCreateReportRequest.id.toString(),
								reportId: reportDataCreateReportRequest.report.id.toString()
							}
						});
					});
				});
			});
		}

		public onParse(request: ReportDataParseRequest) {
			this.$store.dispatch("cbc/parse", request).then(() => {});
		}

		public onValidate(request: ReportDataValidationRequest) {
			this.$store.dispatch("cbc/validate", request).then(() => {});
		}
	}
</script>
