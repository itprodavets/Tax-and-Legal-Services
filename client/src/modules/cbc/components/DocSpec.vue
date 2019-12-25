<template>
	<v-row class="doc-spec">
		<v-col cols="12">
			<v-row>
				<v-col cols="12" lg="3" md="3">
					<v-autocomplete
						dense
						filled
						v-model="$v.docType.$model"
						:items="docTypes"
						label="Doc Type"
						:disabled="readonly"
						:error-messages="docTypeErrors"
						required
						@input="$v.docType.$touch()"
						@blur="$v.docType.$touch()"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="3" md="3">
					<v-text-field
						dense
						filled
						v-model="$v.docRefId.$model"
						label="Doc Ref Id"
						:disabled="readonly"
						:error-messages="docRefIdErrors"
						required
						@input="$v.docRefId.$touch()"
						@blur="$v.docRefId.$touch()"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="3" md="3">
					<v-text-field
						dense
						filled
						v-model="$v.corrMessageRefId.$model"
						label="Corr Message Ref Id"
						:disabled="readonly"
						:error-messages="corrMessageRefIdErrors"
						@input="$v.corrMessageRefId.$touch()"
						@blur="$v.corrMessageRefId.$touch()"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="3" md="3">
					<v-text-field
						dense
						filled
						v-model="$v.corrDocRefId.$model"
						label="Corr Doc Ref Id"
						:disabled="readonly"
						:error-messages="corrDocRefIdErrors"
						@input="$v.corrDocRefId.$touch()"
						@blur="$v.corrDocRefId.$touch()"
					></v-text-field>
				</v-col>
			</v-row>
		</v-col>
	</v-row>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
import { OECDDocTypeIndic_EnumType } from "@/modules/cbc/models";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {
    docType: {
      required
    },
    docRefId: {
      required,
      maxLength: maxLength(200)
    },
    corrMessageRefId: {
      maxLength: maxLength(200)
    },
    corrDocRefId: {
      maxLength: maxLength(170)
    }
  }
})
export default class DocSpecComponent extends Vue {
  @Prop()
  public readonly!: boolean;

  public docTypes = Object.values(OECDDocTypeIndic_EnumType).filter(
    value => typeof value === "string"
  );

  public data() {
    return {
      docType: OECDDocTypeIndic_EnumType[OECDDocTypeIndic_EnumType.OECD0],
      docRefId: "",
      corrMessageRefId: "",
      corrDocRefId: ""
    };
  }
  public get docTypeErrors() {
    const errors: string[] = [];
    if (this.$v.docType) {
      if (!this.$v.docType.$dirty) return errors;
      if (!this.$v.docType.required) {
        errors.push("The Doc Type is required");
      }
    }
    return errors;
  }
  public get docRefIdErrors() {
    const errors: string[] = [];
    if (this.$v.docRefId) {
      if (!this.$v.docRefId.$dirty) return errors;
      if (!this.$v.docRefId.required) {
        errors.push("The Doc Ref Id is required");
      }
      if (!this.$v.docRefId.maxLength) {
        errors.push("The maximum of 200 characters are allowed");
      }
    }
    return errors;
  }
  public get corrMessageRefIdErrors() {
    const errors: string[] = [];
    if (this.$v.corrMessageRefId) {
      if (!this.$v.corrMessageRefId.$dirty) return errors;
      if (!this.$v.corrMessageRefId.maxLength) {
        errors.push("The maximum of 200 characters are allowed");
      }
    }
    return errors;
  }
  public get corrDocRefIdErrors() {
    const errors: string[] = [];
    if (this.$v.corrDocRefId) {
      if (!this.$v.corrDocRefId.$dirty) return errors;
      if (!this.$v.corrDocRefId.maxLength) {
        errors.push("The maximum of 170 characters are allowed");
      }
    }
    return errors;
  }
}
</script>
<style lang="scss" scoped>
.cbc-reports {
	width: 100%;
	margin-bottom: 10px;
}
</style>
