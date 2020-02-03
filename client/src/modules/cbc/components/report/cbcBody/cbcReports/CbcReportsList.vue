<template>
    <div class="cbc-reports-list">
        <v-tabs v-model="tab" dense>
            <v-tab href="#correctableCbCReport">Correctable CbC Report</v-tab>
            <v-tab href="#constituentEntity">Constituent Entity</v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab">
            <v-tab-item id="correctableCbCReport">
                <CorrectableCbCReportComponent :readonly="false" :countries="countries"/>
            </v-tab-item>
            <v-tab-item id="constituentEntity">
                <ConstituentEntityComponent :readonly="false" :countries="countries"/>
            </v-tab-item>
        </v-tabs-items>
    </div>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";
	import CorrectableCbCReportComponent from "./CorrectableCbCReport.vue";
	import {Country} from "@/modules/country/models/dto.model";

	@Component({
		components: {
			CorrectableCbCReportComponent
		}
	})
	export default class CbcReportsListComponent extends Vue {
		@Prop()
		public countries!: Country[];
		@Prop()
		public readonly!: boolean;

		public data() {
			return {
				tab: null
			};
		}
	}
</script>
<style lang="scss" scoped>
    .cbc-reports-list {
        width: 100%;

        &-toolbar {
            box-shadow: none;
        }
    }
</style>