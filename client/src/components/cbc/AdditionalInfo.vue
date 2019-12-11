<template>
	<v-card class="additional-info">
		<v-container>
			<v-row>
				<v-col cols="12">
					<v-text-field dense filled v-model="$v.docSpec.$model" label="Doc Spec" :disabled="readonly"></v-text-field>
				</v-col>
			</v-row>
			<v-row>
				<v-col cols="12">
					<v-text-field
						dense
						filled
						v-model="$v.otherInfo.$model"
						label="Other Info"
						:disabled="readonly"
					></v-text-field>
				</v-col>
			</v-row>
			<v-row>
				<v-col cols="12">
					<v-text-field
						dense
						filled
						v-model="$v.resCountryCode.$model"
						label="Res Country Code"
						:disabled="readonly"
					></v-text-field>
				</v-col>
			</v-row>
			<v-row>
				<v-col cols="12">
					<v-text-field
						dense
						filled
						v-model="$v.summaryRef.$model"
						label="Summary Ref"
						:disabled="readonly"
					></v-text-field>
				</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { validationMixin } from "vuelidate";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {
    docSpec: {},
    otherInfo: {},
    resCountryCode: {},
    summaryRef: {}
  }
})
export default class AdditionalInfoComponent extends Vue {
  private _readonly!: boolean;
  get readonly() {
    return !this._readonly;
  }
}
</script>
<style lang="scss" scoped>
.additional-info {
	width: 100%;
	margin-bottom: 10px;
}
</style>
