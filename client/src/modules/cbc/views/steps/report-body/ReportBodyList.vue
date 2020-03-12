<template>
	<v-card>
		<v-card-text class="pa-0">
			<ReportBodyListComponent :reportBody="items" @create="onCreate"
			                         @get-report-body="onEdit"/>
		</v-card-text>
		<v-card-actions class="align-center justify-center">
			<v-btn @click="onGoToRoute('message')" class="ma-2" color="success"
			       outlined
			       tile>
				<v-icon left>mdi-chevron-right-circle</v-icon>
				Continue
			</v-btn>
			<v-btn @click="onGoToRoute('additional.information')" class="ma-2" color="warning" outlined tile>
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</v-card-actions>
	</v-card>
</template>
<script lang="ts">
	import ReportBodyListComponent from "@/modules/cbc/components/form/list/report-body/ReportBodyList.vue";
	import {
		Report,
		ReportBody,
		ReportBodyCreateRequest,
		ReportBodyRequest,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			ReportBodyListComponent
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as ReportBodyRequest;
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/reportBody/list", request);
			});
		}
	})
	export default class ReportBodyListView extends Vue {
		public get items(): ReportBody[] {
			return this.$store.state.cbc.report.reportBody.entities;
		}

		public get report(): Report {
			return this.$store.state.cbc.report.entity as Report;
		}

		public onCreate(request: ReportBodyCreateRequest) {
			this.$store.dispatch("cbc/report/reportBody/create", request)
				.then((id: string) => {
					this.$store.dispatch("cbc/report/reportBody/get", id)
						.then(() => {
							this.$router.push({
								name: 'report.body.detail',
								params: {reportBodyId: id}
							});
						});
				});
		}

		public onEdit(rb: ReportBody) {
			this.$store.dispatch("cbc/report/reportBody/get", rb.id)
				.then(() => {
					this.$router.push({
						name: 'report.body.detail',
						params: {reportBodyId: rb.id.toString()}
					});
				});
		}

		public onGoToRoute(name: string) {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.report, {reports: this.items})
			} as ReportDataUpdateReportRequest;

			this.$store.dispatch("cbc/update_report", reportDataUpdateReportRequest).then(() => {
				this.$store.dispatch("cbc/report/update", {
					reportDataId: reportDataUpdateReportRequest.id,
					report: reportDataUpdateReportRequest.report
				} as ReportUpdateRequest);
				if (this.$router.app.$route.name !== name)
					this.$router.push({name: name});
			});
		}
	}
</script>