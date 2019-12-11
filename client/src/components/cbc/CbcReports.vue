<template>
	<v-card class="cbc-reports">
		<v-container>
			<v-row>
				<v-col cols="12">CbcReports</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component({
  components: {}
})
export default class CbcReportsComponent extends Vue {}
</script>
<style lang="scss" scoped>
.cbc-reports {
	width: 100%;
	margin-bottom: 10px;
}
</style>
