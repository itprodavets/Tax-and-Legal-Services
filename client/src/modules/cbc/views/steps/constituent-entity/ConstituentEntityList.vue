<template>
	<v-card>
		<v-card-text class="pa-0">
			<ConstituentEntityListComponent :constituentEntities="items" @create="onCreate"
			                                @get-constituent-entity="onEdit"/>
		</v-card-text>
		<v-card-actions class="align-center justify-center">
			<v-btn @click="onGoToRoute('reporting.entity')" class="ma-2" color="success"
			       outlined
			       tile>
				<v-icon left>mdi-chevron-right-circle</v-icon>
				Continue
			</v-btn>
			<v-btn @click="onGoToRoute('cbc.report')" class="ma-2" color="warning" outlined tile>
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</v-card-actions>
	</v-card>
</template>
<script lang="ts">
	import ConstituentEntityListComponent
		from "@/modules/cbc/components/form/list/constituent-entity/ConstituentEntityList.vue";
	import {
		ConstituentEntity,
		ConstituentEntityCreateRequest,
		ConstituentEntityRequest,
		Report,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			ConstituentEntityListComponent
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as ConstituentEntityRequest;
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/constituentEntity/list", request);
			});
		}
	})
	export default class ConstituentEntityListView extends Vue {
		public get items(): ConstituentEntity[] {
			return this.$store.state.cbc.report.constituentEntity.entities as ConstituentEntity[];
		}

		public get report(): Report {
			return this.$store.state.cbc.report.entity as Report;
		}

		public onCreate(request: ConstituentEntityCreateRequest) {
			this.$store.dispatch("cbc/report/constituentEntity/create", request)
				.then((id: string) => {
					this.$store.dispatch("cbc/report/constituentEntity/get", id)
						.then(() => {
							this.$router.push({
								name: 'constituent.entity.detail',
								params: {constituentEntityId: id}
							});
						});
				});
		}

		public onEdit(ce: ConstituentEntity) {
			this.$store.dispatch("cbc/report/constituentEntity/get", ce.id)
				.then(() => {
					this.$router.push({
						name: 'constituent.entity.detail',
						params: {constituentEntityId: ce.id.toString()}
					});
				});
		}

		public onGoToRoute(name: string) {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.report, {constituentEntities: this.items})
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