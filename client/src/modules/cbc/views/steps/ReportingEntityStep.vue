<template>
    <v-container fluid class="pa-0">
        <ReportingEntityComponent :reportingEntity.sync="reportingEntity" :readonly="false"
                                  :countries="this.$store.state.country.entities"/>
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
        <v-btn class="ma-2" tile outlined color="warning" to="/cbc-report/list">
            <v-icon left>mdi-arrow-left-circle</v-icon>
            Back
        </v-btn>
    </v-container>
</template>
<script lang="ts">
	import ReportingEntityComponent from "@/modules/cbc/components/form/сbcBody/reportingEntity/ReportingEntity.vue";
	import {ReportingEntityRequest} from "@/modules/cbc/models";
    import {ReportingEntityState} from "@/modules/cbc/store/report/reporting-entity/reporting-entity.state";
	import {Component, Vue} from "vue-property-decorator";
    import {Getter} from "vuex";

	@Component({
		components: {
			ReportingEntityComponent
		}
	})
	export default class ReportingEntityStepView extends Vue {
		public get reportId() {
			return this.$route.params["reportId"];
		}

		public data() {
			return {
				reportingEntity: {}
			};
		}

		public created() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("cbc/report/reportingEntity/list", {
				reportId: this.reportId
			} as ReportingEntityRequest).then(() => {
				this.$data.reportingEntity = this.$store.getters["cbc/report/reportingEntity/reportingEntity"];
			});
		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>