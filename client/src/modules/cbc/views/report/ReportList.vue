<template>
	<v-container class="pa-0" fluid>
		<ReportListComponent :reports="items" @create="onCreate"/>
	</v-container>
</template>
<script lang="ts">
	import ReportListComponent from "@/modules/cbc/components/form/list/report/ReportList.vue";
	import {Report, ReportCreateRequest, ReportDataCreateReportRequest, ReportRequest} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			ReportListComponent
		},
		mounted() {
			this.$store.dispatch("cbc/get", this.$route.params["id"]).then(() => {
				this.$store.dispatch("cbc/report/list", {reportDataId: this.$route.params["id"]} as ReportRequest);
			});
		}
	})
	export default class ReportListView extends Vue {
		public get items() {
			return this.$store.state.cbc.report.entities as Report[];
		}

		public onCreate(request: ReportCreateRequest) {
			this.$store.dispatch("cbc/report/create", request).then(id => {
				const reportDataCreateReportRequest = {
					id: request.reportDataId,
					report: Object.assign(request.report, {id: id}) as Report
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
		}
	}
</script>
