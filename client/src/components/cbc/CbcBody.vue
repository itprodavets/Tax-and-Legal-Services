<template>
	<div class="cbc-body">
		<ReportingEntityComponent />
		<CbcReportsComponent />
		<AdditionalInfoComponent />
	</div>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import ReportingEntityComponent from "../cbc/ReportingEntity.vue";
import CbcReportsComponent from "../cbc/CbcReports.vue";
import AdditionalInfoComponent from "../cbc/AdditionalInfo.vue";

@Component({
  components: {
    ReportingEntityComponent,
    CbcReportsComponent,
    AdditionalInfoComponent
  }
})
export default class CbcBodyComponent extends Vue {}
</script>
<style lang="scss" scoped>
.cbc-body {
	width: 100%;
}
</style>
