<template>
	<v-card>
		<v-card-text class="pa-0">
			<ReportingEntityComponent :countries="countries" :readonly="false"
			                          v-bind:reportingEntity.sync="item"/>
		</v-card-text>
		<v-card-actions class="align-center justify-center">
			<v-btn @click="onGoToRoute('additional.information')" class="ma-2" color="success" outlined tile>
				<v-icon left>mdi-chevron-right-circle</v-icon>
				Continue
			</v-btn>
			<v-btn @click="onGoToRoute('constituent.entity')" class="ma-2" color="warning" outlined tile>
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</v-card-actions>
	</v-card>
</template>
<script lang="ts">
	import ReportingEntityComponent from "@/modules/cbc/components/form/сbcBody/reportingEntity/ReportingEntity.vue";
	import {
		Report,
		ReportDataUpdateReportRequest,
		ReportingEntity,
		ReportingEntityAddRequest,
		ReportingEntityRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {CountryMixin} from "@/modules/country/mixins";
	import {Component, Mixins, Watch} from "vue-property-decorator";

	@Component({
		components: {
			ReportingEntityComponent
		},
		mounted() {
			this.$store.dispatch("cbc/report/get", this.$route.params["reportId"]).then(() => {
				this.$store.dispatch("cbc/report/reportingEntity/list", {reportId: this.$route.params["reportId"]} as ReportingEntityRequest);
			});
		}
	})
	export default class ReportingEntityStepView extends Mixins(CountryMixin) {
		public get item(): ReportingEntity {
			return this.$store.state.cbc.report.reportingEntity.entity;
		};

		public set item(item: ReportingEntity) {
			this.$store.dispatch("cbc/report/reportingEntity/add", {
				reportId: this.$route.params["reportId"],
				reportingEntity: item
			} as ReportingEntityAddRequest)
		};

		@Watch("item", {deep: true})
		public onChanged(value: ReportingEntity, oldValue: ReportingEntity) {
			this.item = value;
		}

		public get report(): Report {
			return this.$store.state.cbc.report.entity as Report;
		}

		public onGoToRoute(name: string) {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.report, {reportingEntity: this.item})
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