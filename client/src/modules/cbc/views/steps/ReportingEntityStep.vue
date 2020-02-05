<template>
	<v-container fluid class="pa-0">
		<ReportingEntityComponent :reportingEntity.sync="reportingEntity" :readonly="false"
		                          :countries="countries"/>
		<v-btn
				class="ma-2"
				tile
				outlined
				color="success"
				@click="onSaveAndContinue('additional.information')"
		>
			<v-icon left>mdi-chevron-right-circle</v-icon>
			Continue
		</v-btn>
		<v-btn class="ma-2" tile outlined color="warning" to="constituent-entity">
			<v-icon left>mdi-arrow-left-circle</v-icon>
			Back
		</v-btn>
	</v-container>
</template>
<script lang="ts">
	import ReportingEntityComponent from "@/modules/cbc/components/form/сbcBody/reportingEntity/ReportingEntity.vue";
	import {ReportingEntity, ReportingEntityAddRequest, ReportingEntityRequest} from "@/modules/cbc/models";
	import {CountryMixin} from "@/modules/country/mixins";
	import {Component, Mixins} from "vue-property-decorator";

	@Component({
		components: {
			ReportingEntityComponent
		},
		mounted() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("cbc/report/get", this.$route.params["reportId"]).then(() => {
				this.$store.dispatch("cbc/report/reportingEntity/list", {reportId: this.$route.params["reportId"]} as ReportingEntityRequest);
			});
		}
	})
	export default class ReportingEntityStepView extends Mixins(CountryMixin) {
		public get reportingEntity(): ReportingEntity {
			return this.$store.state.cbc.report.reportingEntity.entity;
		};

		public set reportingEntity(reportingEntity: ReportingEntity) {
			this.$store.dispatch("cbc/report/reportingEntity/add", {
				reportId: this.$route.params["reportId"],
				reportingEntity: reportingEntity
			} as ReportingEntityAddRequest)
		};

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>