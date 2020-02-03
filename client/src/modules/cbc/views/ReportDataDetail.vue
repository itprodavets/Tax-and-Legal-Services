<template>
    <v-container fluid class="pa-0" v-if="reportData.id">
        <v-toolbar dense class="mb-3 elevation-1">
            <template>
                <v-btn dense icon to="/cbc-report/list">
                    <v-icon>mdi-arrow-left-circle</v-icon>
                </v-btn>
            </template>
            <v-toolbar-title>CbC Reporting</v-toolbar-title>
            <v-spacer></v-spacer>
            <SupportedSchemaSelectComponent v-model="reportData.version"/>
        </v-toolbar>
        <router-view></router-view>
    </v-container>
</template>
<script lang="ts">
	import SupportedSchemaSelectComponent from "@/modules/cbc/components/shared/SupportedSchemaSelect.vue";
	import {ReportData, ReportDataUpdateRequest} from "@/modules/cbc/models";
	import {Component, Vue, Watch} from "vue-property-decorator";

	@Component({
		components: {
			SupportedSchemaSelectComponent
		}
	})
	export default class ReportDataDetailView extends Vue {

		@Watch("reportData", {deep: true})
		onReportDataChanged(value: ReportData, oldValue: ReportData) {
			this.onUpdate();
		}

		public data() {
			return {
				reportData: {
					version: {}
				} as ReportData
			};
		}

		public created() {
			this.$store.dispatch("cbc/get", this.$route.params["id"]).then(() => {
				this.$data.reportData = this.$store.state.cbc.entity as ReportData;
			});
		}

		public onUpdate() {
			this.$store.dispatch("cbc/update", {
				data: this.$data.reportData as ReportData
			} as ReportDataUpdateRequest);
		}
	}
</script>
