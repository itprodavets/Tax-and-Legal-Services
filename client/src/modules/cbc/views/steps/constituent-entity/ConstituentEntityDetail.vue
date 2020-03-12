<template>
	<v-card>
		<v-card-text class="pa-0">
			<ConstituentEntityComponent :countries="this.$store.state.country.entities" v-bind:constituentEntity.sync="item"
			                            :readonly="false"/>
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
	import ConstituentEntityComponent
		from "@/modules/cbc/components/form/сbcBody/constituentEntity/ConstituentEntity.vue";
	import {
		ConstituentEntity,
		ConstituentEntityRequest,
		ConstituentEntityUpdateRequest,
		ReportDataUpdateReportRequest,
		ReportUpdateRequest
	} from "@/modules/cbc/models";
	import {Component, Vue, Watch} from "vue-property-decorator";

	@Component({
		components: {
			ConstituentEntityComponent
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as ConstituentEntityRequest;
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/constituentEntity/get", this.$route.params["constituentEntityId"]);
			});
		}
	})
	export default class ConstituentEntityDetailView extends Vue {
		public get item(): ConstituentEntity {
			return this.$store.state.cbc.report.constituentEntity.entity;
		}

		public set item(item: ConstituentEntity) {
			this.$store.dispatch("cbc/report/constituentEntity/update", {
				reportId: this.$route.params["reportId"],
				constituentEntity: item
			} as ConstituentEntityUpdateRequest);
		}

		@Watch("item", {deep: true})
		public onChanged(value: ConstituentEntity, oldValue: ConstituentEntity) {
			this.item = value;
		}

		public onSave() {
			const reportDataUpdateReportRequest = {
				id: this.$route.params["id"],
				report: Object.assign(this.$store.state.cbc.report.entity, {constituentEntities: this.$store.state.cbc.report.constituentEntity.entities})
			} as ReportDataUpdateReportRequest;
			this.$store.dispatch("cbc/update_report", reportDataUpdateReportRequest).then(() => {
				this.$store.dispatch("cbc/report/update", {
					reportDataId: reportDataUpdateReportRequest.id,
					report: reportDataUpdateReportRequest.report
				} as ReportUpdateRequest);
				this.$router.push({name: "constituent.entity.list"});
			});
		}
	}
</script>