<template>
	<v-container fluid class="pa-0">
		<v-stepper v-model="stepper">
			<v-stepper-header style="box-shadow: none;">
				<template v-for="step in steppers">
					<v-stepper-step
							:key="step.id"
							:complete="stepper > step.id"
							:step="step.id"
					>{{step.name}}
					</v-stepper-step>
					<v-divider v-if="step !== steppers[steppers.length - 1]" :key="step.route"></v-divider>
				</template>
			</v-stepper-header>
			<v-stepper-items>
				<v-stepper-content v-for="step in steppers" :key="step.id" :step="step.id" class="pa-0">
					<router-view v-if="step.id === stepper"></router-view>
				</v-stepper-content>
			</v-stepper-items>
		</v-stepper>
	</v-container>
</template>
<script lang="ts">
	import _ from "lodash";
	import {Component, Mixins} from "vue-property-decorator";
	import {Route} from "vue-router";

	@Component
	export default class ReportDetailView extends Mixins() {
		public stepper: number = 1;
		public steppers: any[] = [
			{
				id: 1,
				name: "Constituent Entities",
				route: "constituent.entity"
			},
			{id: 2, name: "Reporting Entity", route: "reporting.entity"},
			{
				id: 3,
				name: "Additional Information",
				route: "additional.information"
			},
			{id: 4, name: "Reports", route: "report.body"}
		];

		public created() {
			this.setStepper(this.$route.name);
			this.$router.afterEach((to: Route, from: Route) => this.setStepper(to.name));
		}

		private setStepper(name: string | null | undefined) {
			if (name) {
				const step = _.find(this.steppers, x => name.search(x.route) === 0);
				if (step) this.stepper = step.id;
			}
		}
	}
</script>

