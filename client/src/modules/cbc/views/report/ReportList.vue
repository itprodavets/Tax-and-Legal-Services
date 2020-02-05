<template>
    <v-container fluid class="pa-0 pt-2">
        <v-toolbar dense class="elevation-1">
            <template>
                <v-btn dense icon @click="onCreate()">
                    <v-icon>mdi-plus-circle</v-icon>
                </v-btn>
            </template>
            <v-toolbar-title>Reports</v-toolbar-title>
        </v-toolbar>
        <ReportListComponent :reports="reports"/>
    </v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";
	import {ReportCreateRequest, ReportRequest} from "@/modules/cbc/models";
	import ReportListComponent from "@/modules/cbc/components/list/report/ReportList.vue";
	import {Guid} from "@/core/common/guid";
    import {mapGetters} from "vuex";

	@Component({
		components: {
			ReportListComponent
		},
        computed: {
		    ...mapGetters("cbc/report", ["reports"])
        },
        mounted() {
            this.$store.dispatch("cbc/report/list", {reportDataId: this.$route.params["id"]} as ReportRequest);
        }
	})
	export default class ReportListView extends Vue {
		public onCreate() {
			const report = {
				id: Guid.create().toString(),
				reportDataId: this.$route.params["id"]
			} as ReportCreateRequest;
			this.$store.dispatch("cbc/report/create", report).then(() => {
				this.$router.push({
					name: "constituent.entity",
					params: {id: report.reportDataId.toString(), reportId: report.id.toString()}
				});
			});
		}
	}
</script>
