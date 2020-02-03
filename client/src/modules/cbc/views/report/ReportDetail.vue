<template>
	<v-container class="pa-0">
		<v-stepper v-model="stepper">
			<v-stepper-header>
				<template v-for="step in steppers">
					<v-stepper-step
						:key="step.id"
						:complete="stepper > step.id"
						:step="step.id"
						@click="onGoToRoute(step.route)"
						editable
					>{{step.name}}</v-stepper-step>
					<v-divider v-if="step !== steppers[steppers.length - 1]" :key="step.route"></v-divider>
				</template>
			</v-stepper-header>
			<v-stepper-items align="center">
				<v-stepper-content v-for="step in steppers" :key="step.id" :step="step.id" class="pa-0 pt-2">
					<router-view v-if="step.id === stepper"></router-view>
				</v-stepper-content>
			</v-stepper-items>
		</v-stepper>
	</v-container>
</template>
<script lang="ts">
	import {Component, Mixins} from "vue-property-decorator";
	import {Route} from "vue-router";

	import {ReportData} from "@/modules/cbc/models";

	import _ from "lodash";

	@Component
export default class ReportDetailView extends Mixins() {
  public data() {
    return {
      stepper: 1,
      steppers: [
        {
          id: 1,
          name: "Constituent Entities",
          route: "constituent.entity"
        },
        { id: 2, name: "Reporting Entity", route: "reporting.entity" },
        {
          id: 3,
          name: "Additional Information",
          route: "additional.information"
        },
        { id: 4, name: "Reports", route: "report.body" }
      ],
      ReportData: {
        version: 1
      } as ReportData
    };
  }
  public created() {
    this.$router.afterEach((to: Route, from: Route) => {
      const step = _.find(this.$data.steppers, x => x.route === to.name);
      if (step) {
        this.$data.stepper = step.id;
      }
    });
  }

  public onGoToRoute(name: string) {
    if (this.$router.app.$route.name !== name)
      this.$router.push({ name: name });
  }
}
</script>

