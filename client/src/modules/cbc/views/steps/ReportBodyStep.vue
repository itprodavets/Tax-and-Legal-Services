<template>
	<v-container fluid class="pa-0">
		<ReportBodyComponent
			:readonly="false"
			:countries="this.$store.state.country.entities"
			:currencies="this.$store.state.currency.entities"
		/>
		<v-btn
			class="ma-2"
			tile
			outlined
			color="success"
			@click="onSaveAndContinue('message')"
		>
			<v-icon left>mdi-chevron-right-circle</v-icon>Continue
		</v-btn>
		<v-btn class="ma-2" tile outlined color="warning" to="/cbc-report/list">
			<v-icon left>mdi-arrow-left-circle</v-icon>Back
		</v-btn>
	</v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";
	import ReportBodyComponent from "@/modules/cbc/components/form/сbcBody/report-body/ReportBody.vue";

	@Component({
		components: {
			ReportBodyComponent
		}
	})
	export default class ReportBodyStepView extends Vue {
		public created() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("currency/list");
		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>