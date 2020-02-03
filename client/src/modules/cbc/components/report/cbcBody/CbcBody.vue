<template>
    <div class="cbc-body">
        <ReportingEntityComponent :countries="this.countries"/>
        <CbcReportsComponent :countries="this.countries"/>
        <AdditionalInfoComponent :countries="this.countries" :languages="this.languages"/>
    </div>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";
	import ReportingEntityComponent from "@/modules/cbc/components/cbcBody/reportingEntity/ReportingEntity.vue";
	import CbcReportsComponent from "@/modules/cbc/components/cbcBody/cbcReports/CbcReports.vue";
	import AdditionalInfoComponent from "@/modules/cbc/components/cbcBody/additionalInfo/edit/AdditionalInfo.vue";
	import {Country} from "@/modules/country/models/dto.model";
	import {Language} from "@/modules/language/models/dto.model";

	@Component({
		components: {
			ReportingEntityComponent,
			CbcReportsComponent,
			AdditionalInfoComponent
		}
	})
	export default class CbcBodyComponent extends Vue {
		@Prop()
		public languages!: Language[];

		@Prop()
		public readonly countries!: Country[];
	}
</script>
<style lang="scss" scoped>
    .cbc-body {
        width: 100%;
    }
</style>
