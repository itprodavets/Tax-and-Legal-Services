<template>
	<v-card class="cbc-reports">
		<v-container>
			<v-row>
				<v-col align="center" justify="space-around">
					<v-btn class="ma-2" tile outlined color="success" v-if="readonly" @click="onEdit()">
						<v-icon left>mdi-pencil</v-icon>Edit
					</v-btn>
					<v-btn class="ma-2" tile outlined color="warning" v-if="!readonly" @click="onEdit()">
						<v-icon left>mdi-cancel</v-icon>Cancel
					</v-btn>
					<v-btn class="ma-2" tile outlined color="success" v-if="!readonly" @click="onEdit()">
						<v-icon left>mdi-content-save</v-icon>Save
					</v-btn>
				</v-col>
			</v-row>
			<DocSpecComponent :readonly="readonly" />
			<v-row>
				<v-col cols="12" lg="2" md="2">
					<v-autocomplete
						dense
						filled
						v-model="$v.resCountryCode.$model"
						:items="countries"
						item-text="name"
						label="Res Country Code"
						:disabled="readonly"
						return-object
					></v-autocomplete>
				</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { Country } from "@/modules/country/models/dto.model";
import DocSpecComponent from "@/modules/cbc/components/DocSpec.vue";
import { validationMixin } from "vuelidate";
import { required } from "vuelidate/lib/validators";

@Component({
  components: {
    DocSpecComponent
  },
  mixins: [validationMixin],
  validations: {
    resCountryCode: {
      required
    }
  }
})
export default class CbcReportsComponent extends Vue {
  @Prop()
  public countries!: Country[];

  public readonly: boolean = true;
  public onEdit() {
    this.readonly = !this.readonly;
  }
  public data() {
    return {
      resCountryCode: ""
    };
  }
}
</script>
<style lang="scss" scoped>
.cbc-reports {
	width: 100%;
	margin-bottom: 10px;
}
</style>
