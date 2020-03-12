<template>
	<v-card>
		<v-card-text class="pa-0">
			<AdditionalInfoComponent :countries="this.$store.state.country.entities" v-bind:additionalInfo.sync="item"
			                         :languages="this.$store.state.language.entities"
			                         :readonly="false"
			/>
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
	import AdditionalInfoComponent from "@/modules/cbc/components/form/сbcBody/additionalInfo/AdditionalInfo.vue";
	import {
		AdditionalInfo,
		AdditionalInfoRequest,
		AdditionalInfoUpdateRequest,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue, Watch} from "vue-property-decorator";

	@Component({
		components: {
			AdditionalInfoComponent
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as AdditionalInfoRequest;
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/additionalInformation/get", this.$route.params["additionalInfoId"]);
			});
		}
	})
	export default class AdditionalInformationDetailView extends Vue {

		public get item(): AdditionalInfo {
			return this.$store.state.cbc.report.additionalInformation.entity;
		}

		public set item(item: AdditionalInfo) {
			const request = {
				reportId: this.$route.params["reportId"],
				additionalInfo: item
			} as AdditionalInfoUpdateRequest;
			this.$store.dispatch("cbc/report/additionalInformation/update", request);
		}

		@Watch("item", {deep: true})
		public onChanged(value: AdditionalInfo, oldValue: AdditionalInfo) {
			this.item = value;
		}

		public onSave() {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.$store.state.cbc.report.entity, {additionalInfo: this.$store.state.cbc.report.additionalInformation.entities})
			} as ReportDataUpdateReportRequest;
			this.$store.dispatch("cbc/update_report", reportDataUpdateReportRequest).then(() => {
				this.$store.dispatch("cbc/report/update", {
					reportDataId: reportDataUpdateReportRequest.id,
					report: reportDataUpdateReportRequest.report
				} as ReportUpdateRequest);
				this.$router.push({name: "additional.information.list"});
			});
		}
	}
</script>