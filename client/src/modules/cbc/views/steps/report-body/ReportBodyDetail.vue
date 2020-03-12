<template>
	<v-card>
		<v-card-text class="pa-0">
			<ReportBodyComponent :countries="this.$store.state.country.entities" v-bind:reportBody.sync="item"
			                     :currencies="this.$store.state.currency.entities" :readonly="false"/>
		</v-card-text>
		<v-card-actions class="align-center justify-center">
			<v-btn @click="onSave()" class="ma-2" color="success" outlined tile>
				<v-icon left>mdi-plus-circle</v-icon>
				Save
			</v-btn>
			<v-btn to="list" class="ma-2" color="warning" outlined tile>
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</v-card-actions>
	</v-card>
</template>
<script lang="ts">
	import ReportBodyComponent from "@/modules/cbc/components/form/сbcBody/report-body/ReportBody.vue";
	import {
		ReportBody,
		ReportBodyRequest,
		ReportBodyUpdateRequest,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue, Watch} from "vue-property-decorator";

	@Component({
		components: {
			ReportBodyComponent
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as ReportBodyRequest;
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/constituentEntity/get", this.$route.params["reportBodyId"]);
			});
		}
	})
	export default class ReportBodyDetailView extends Vue {
		public get item(): ReportBody {
			return this.$store.state.cbc.report.reportBody.entity;
		}

		public set item(item: ReportBody) {
			const request = {
				reportId: this.$route.params["reportId"],
				reportBody: item
			} as ReportBodyUpdateRequest;
			this.$store.dispatch("cbc/report/reportBody/update", request);
		}

		@Watch("item", {deep: true})
		public onChanged(value: ReportBody, oldValue: ReportBody) {
			this.item = value;
		}

		public onSave() {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.$store.state.cbc.report.entity, {reports: this.$store.state.cbc.report.reportBody.entities})
			} as ReportDataUpdateReportRequest;
			this.$store.dispatch("cbc/update_report", reportDataUpdateReportRequest).then(() => {
				this.$store.dispatch("cbc/report/update", {
					reportDataId: reportDataUpdateReportRequest.id,
					report: reportDataUpdateReportRequest.report
				} as ReportUpdateRequest);
				this.$router.push({name: "report.body.list"});
			});
		}
	}
</script>