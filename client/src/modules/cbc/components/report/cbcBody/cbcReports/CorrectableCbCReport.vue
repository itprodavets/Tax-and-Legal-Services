<template>
    <div class="correctable-cbc-report">
        <v-toolbar dense class="correctable-cbc-report-toolbar">
            <template>
                <v-btn dense icon @click.stop="isOpenDialog = true">
                    <v-icon>mdi-plus-circle</v-icon>
                </v-btn>
            </template>
            <v-toolbar-title>Correctable CbC Report</v-toolbar-title>
        </v-toolbar>

        <v-data-table dense :headers="headers" :items="items"></v-data-table>
        <v-dialog v-model="isOpenDialog" max-width="95vw">
            <v-card align="center">
                <v-card-title class="headline">Create Correctable CbC Report</v-card-title>
                <v-card-text>
                    <CbcReportsComponent :readonly="false" :countries="countries"/>
                </v-card-text>

                <v-card-actions class="justify-center">
                    <v-btn class="ma-2" tile outlined color="success" @click="isOpenDialog = false">
                        <v-icon left>mdi-content-save</v-icon>
                        Save
                    </v-btn>
                    <v-btn class="ma-2" tile outlined color="success" @click="isOpenDialog = false">
                        <v-icon left>mdi-content-save-move</v-icon>
                        Save & Next
                    </v-btn>
                    <v-btn class="ma-2" tile outlined color="warning" @click="isOpenDialog = false">
                        <v-icon left>mdi-cancel</v-icon>
                        Cancel
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";
	import CbcReportsComponent from "./CbcReports.vue";
	import {Country} from "@/modules/country/models/dto.model";

	@Component({
		components: {
			CbcReportsComponent
		}
	})
	export default class CorrectableCbCReportComponent extends Vue {
		@Prop()
		public countries!: Country[];

		public data() {
			return {
				tab: null,
				isOpenDialog: false as boolean,
				headers: [
					{
						text: "Res Country Code",
						value: "resCountryCode"
					},
					{
						text: "Currency",
						value: "currency"
					},
					{
						text: "Unrelated",
						value: "unrelated"
					},
					{
						text: "Related",
						value: "related"
					},
					{
						text: "Total",
						value: "total"
					},
					{
						text: "Profit Or Loss",
						value: "profitOrLoss"
					},
					{
						text: "Tax Paid",
						value: "taxPaid"
					},
					{
						text: "Tax Accrued",
						value: "taxAccrued"
					},
					{
						text: "Capital",
						value: "capital"
					},
					{
						text: "Earnings",
						value: "earnings"
					},
					{
						text: "Nb Employees",
						value: "nbEmployees"
					},
					{
						text: "Assets",
						value: "assets"
					}
				],
				items: []
			};
		}
	}
</script>
<style lang="scss" scoped>
    .correctable-cbc-report {
        width: 100%;

        &-toolbar {
            box-shadow: none;
        }
    }
</style>