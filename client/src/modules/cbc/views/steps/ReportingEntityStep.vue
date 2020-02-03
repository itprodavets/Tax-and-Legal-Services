<template>
	<v-container>
		<ReportingEntityComponent :readonly="false" :countries="this.$store.state.country.entities" />
		<v-btn
			class="ma-2"
			tile
			outlined
			color="success"
			@click="onSaveAndContinue('additional.information')"
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
	import ReportingEntityComponent from "@/modules/cbc/components/form/сbcBody/reportingEntity/ReportingEntity.vue";

	@Component({
		components: {
			ReportingEntityComponent
		}
	})
	export default class ReportingEntityStepView extends Vue {
		public created() {
			this.$store.dispatch("country/list");
		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>