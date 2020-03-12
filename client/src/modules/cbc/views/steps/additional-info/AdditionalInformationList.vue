<template>
	<v-card>
		<v-card-text class="pa-0">
			<AdditionalInfoListComponent :additionalInfo="items" @create="onCreate" @get-additional-info="onEdit"/>
		</v-card-text>
		<v-card-actions class="align-center justify-center">
			<v-btn @click="onGoToRoute('report.body')" class="ma-2" color="success" outlined tile>
				<v-icon left>mdi-chevron-right-circle</v-icon>
				Continue
			</v-btn>
			<v-btn @click="onGoToRoute('reporting.entity')" class="ma-2" color="warning" outlined tile>
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</v-card-actions>
	</v-card>
</template>
<script lang="ts">
	import AdditionalInfoListComponent from "@/modules/cbc/components/form/list/additional-info/AdditionalInfoList.vue";
	import {
		AdditionalInfo,
		AdditionalInfoCreateRequest,
		AdditionalInfoRequest,
		Report,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			AdditionalInfoListComponent
		},
		mounted() {
			this.$store.dispatch("cbc/report/get", this.$route.params["reportId"]).then(() => {
				this.$store.dispatch("cbc/report/additionalInformation/list", {reportId: this.$route.params["reportId"]} as AdditionalInfoRequest);
			});
		}
	})
	export default class AdditionalInformationListView extends Vue {
		public get items(): AdditionalInfo[] {
			return this.$store.state.cbc.report.additionalInformation.entities;
		}

		public get report(): Report {
			return this.$store.state.cbc.report.entity as Report;
		}

		public onCreate(request: AdditionalInfoCreateRequest) {
			this.$store.dispatch("cbc/report/additionalInformation/create", request)
				.then((id: string) => {
					this.$store.dispatch("cbc/report/additionalInformation/get", id)
						.then(() => {
							this.$router.push({
								name: 'additional.information.detail',
								params: {additionalInfoId: id}
							});
						});
				});
		}

		public onEdit(ai: AdditionalInfo) {
			this.$store.dispatch("cbc/report/additionalInformation/get", ai.id)
				.then(() => {
					this.$router.push({
						name: 'additional.information.detail',
						params: {additionalInfoId: ai.id.toString()}
					});
				});
		}

		public onGoToRoute(name: string) {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.report, {additionalInfo: this.items})
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