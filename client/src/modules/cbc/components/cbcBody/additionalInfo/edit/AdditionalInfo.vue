<template>
	<v-card class="additional-info">
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
				<v-col cols="12" lg="4" md="4">
					<v-autocomplete
						dense
						filled
						v-model="$v.language.$model"
						:items="languages"
						item-text="name"
						label="Language"
						:disabled="readonly"
						:error-messages="languageErrors"
						return-object
						required
						@input="$v.language.$touch()"
						@blur="$v.language.$touch()"
					></v-autocomplete>
					<v-autocomplete
						dense
						filled
						v-model="$v.resCountryCode.$model"
						:items="countries"
						item-text="name"
						label="Res Country Code"
						:disabled="readonly"
						multiple
						return-object
					></v-autocomplete>
					<v-autocomplete
						dense
						filled
						v-model="$v.summaryRef.$model"
						:items="summaryRefs"
						item-text="name"
						label="Summary Ref"
						:disabled="readonly"
						multiple
						return-object
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="8" md="8">
					<v-textarea
						dense
						filled
						v-model="$v.otherInfo.$model"
						label="Other Info"
						rows="10"
						:error-messages="otherInfoErrors"
						:disabled="readonly"
					></v-textarea>
				</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { Country } from "@/modules/country/models/dto.model";
import DocSpecComponent from "@/modules/cbc/components/DocSpec.vue";
import { CbcSummaryListElementsType_EnumType } from "@/modules/cbc/models";
import { required, maxLength } from "vuelidate/lib/validators";
import { Language } from "@/modules/language/models/dto.model";

@Component({
  components: {
    DocSpecComponent
  },
  mixins: [validationMixin],
  validations: {
    otherInfo: {
      maxLength: maxLength(4000)
    },
    language: {
      required
    },
    resCountryCode: {},
    summaryRef: {}
  }
})
export default class AdditionalInfoComponent extends Vue {
  @Prop()
  public languages!: Language[];

  @Prop()
  public countries!: Country[];

  public summaryRefs = Object.values(
    CbcSummaryListElementsType_EnumType
  ).filter(value => typeof value === "string");

  public get languageErrors() {
    const errors: string[] = [];
    if (this.$v.language) {
      if (!this.$v.language.$dirty) return errors;
      if (!this.$v.language.required) {
        errors.push("The Language is required");
      }
    }
    return errors;
  }
  public get otherInfoErrors() {
    const errors: string[] = [];
    if (this.$v.otherInfo) {
      if (!this.$v.otherInfo.$dirty) return errors;
      if (!this.$v.otherInfo.maxLength) {
        errors.push("The maximum of 4’000 characters are allowed");
      }
    }
    return errors;
  }

  public readonly: boolean = true;
  public onEdit() {
    this.readonly = !this.readonly;
  }
  public data() {
    return {
      otherInfo: "",
      language: "",
      resCountryCode: "",
      summaryRef: []
    };
  }
}
</script>
<style lang="scss" scoped>
.additional-info {
	width: 100%;
	margin-bottom: 10px;
}
</style>
