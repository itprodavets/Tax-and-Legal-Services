import {SupportedSchema} from "@/modules/cbc/models";
<template>
    <v-container class="pa-0">
        <v-toolbar class="cbc-data-table-toolbar elevation-1" dense>
            <template>
                <v-btn @click="onCreate()" dense icon>
                    <v-icon>mdi-plus-circle</v-icon>
                </v-btn>
            </template>
            <v-toolbar-title>CbC Reporting</v-toolbar-title>
            <v-spacer></v-spacer>
            <template>
                <ReportDataImportComponent @import-file="onImport" />
            </template>
        </v-toolbar>
        <ReportDataListComponent :reportData="this.$store.state.cbc.entities" />
    </v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";
	import ReportDataListComponent from "@/modules/cbc/components/list/ReportDataList.vue";
	import ReportDataImportComponent from "@/modules/cbc/components/import/ReportDataImport.vue";
	import {ReportCreateRequest, ReportData, ReportDataImportRequest, SupportedSchema} from "../models";
	import {Guid} from "@/core/common/guid";

	@Component({
		components: {
			ReportDataListComponent,
			ReportDataImportComponent
		}
	})
	export default class ReportDataListView extends Vue {
		public data() {
			return {};
		}

		public created() {
			this.$store.dispatch("cbc/list");
		}

		public onCreate() {
			const reportData = {
				id: Guid.create().toString(),
				version: SupportedSchema.OECD_200
			} as ReportData;
			this.$store.dispatch("cbc/create", reportData).then(() => {
				const report = {
					id: Guid.create().toString(),
					reportDataId: reportData.id
				} as ReportCreateRequest;
				this.$store.dispatch("cbc/report/create", report).then(() => {
					this.$router.push({
						name: "constituent.entity",
						params: {id: reportData.id.toString(), reportId: report.id.toString()}
					});
				});
			});
		}

		public onImport(file: File) {
			const request = {
				schema: SupportedSchema.OECD_200,
				file: file
			} as ReportDataImportRequest;
			this.$store.dispatch("cbc/import", request).then(() => {});
		}
	}
</script>
