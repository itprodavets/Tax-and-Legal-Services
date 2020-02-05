<template>
	<v-container fluid class="pa-0">
		<ReportBodyListComponent :reportBody="parts"/>
		<!--		<ReportBodyComponent-->
		<!--			:readonly="false"-->
		<!--			:countries="this.$store.state.country.entities"-->
		<!--			:currencies="this.$store.state.currency.entities"-->
		<!--		/>-->
				<v-btn
					class="ma-2"
					tile
					outlined
					color="success"
					@click="onSaveAndContinue('message')"
				>
					<v-icon left>mdi-chevron-right-circle</v-icon>Continue
				</v-btn>
				<v-btn class="ma-2" tile outlined color="warning" to="additional-information">
					<v-icon left>mdi-arrow-left-circle</v-icon>Back
				</v-btn>
	</v-container>
</template>
<script lang="ts">
	import ReportBodyComponent from "@/modules/cbc/components/form/сbcBody/report-body/ReportBody.vue";
	import ReportBodyListComponent from "@/modules/cbc/components/form/сbcBody/report-body/ReportBodyList.vue";
	import {ReportBody, ReportBodyRequest, ReportingEntity, ReportingEntityAddRequest} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			ReportBodyListComponent,
			ReportBodyComponent
		},
		mounted() {
			this.$store.dispatch("cbc/report/get", this.$route.params["reportId"]).then(() => {
				this.$store.dispatch("cbc/report/reportBody/list", {reportId: this.$route.params["reportId"]} as ReportBodyRequest);
			});
		}
	})
	export default class ReportBodyStepView extends Vue {
		public get parts(): ReportBody[] {
			return this.$store.state.cbc.report.reportBody.entities;
		};
		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>