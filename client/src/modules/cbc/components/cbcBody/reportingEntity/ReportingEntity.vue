<template>
	<div class="reporting-entity">
		<v-card class="entity">
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
				<v-row class="res-country-code">
					<v-col class="name" cols="12" lg="4" md="4">
						<v-text-field dense filled v-model="$v.name.$model" label="Name" :disabled="readonly"></v-text-field>
					</v-col>
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
					<v-col class="tin" cols="12" lg="2" md="2">
						<v-text-field dense filled v-model="$v.tin.$model" label="TIN" :disabled="readonly"></v-text-field>
					</v-col>
					<v-col class="in" cols="12" lg="2" md="2">
						<v-text-field dense filled v-model="$v.in.$model" label="IN" :disabled="readonly"></v-text-field>
					</v-col>
					<v-col class="in" cols="12" lg="2" md="2">
						<v-autocomplete
							dense
							filled
							v-model="$v.reportingRole.$model"
							:items="reportingRoles"
							label="Reporting Role"
							:disabled="readonly"
						></v-autocomplete>
					</v-col>
					<v-col class="address" cols="12">
						<v-row>
							<v-col class="country-code" cols="12" lg="2" md="2">
								<v-autocomplete
									dense
									filled
									v-model="$v.addressType.$model"
									:items="addressTypes"
									label="Address Type"
									:disabled="readonly"
								></v-autocomplete>

								<v-autocomplete
									dense
									filled
									v-model="$v.countryCode.$model"
									:items="countries"
									item-text="name"
									label="Country Code"
									:disabled="readonly"
									return-object
								></v-autocomplete>
							</v-col>
							<v-col class="address-free" cols="12" lg="10" md="2">
								<v-textarea
									dense
									filled
									v-model="$v.addressFree.$model"
									label="Address Free"
									rows="5"
									:disabled="readonly"
								></v-textarea>
							</v-col>
						</v-row>
					</v-col>
				</v-row>
				<DocSpecComponent :readonly="readonly" />
			</v-container>
		</v-card>
	</div>
</template>
<script lang="ts">
import { Component, Prop, Vue, Watch } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { Country } from "@/modules/country/models/dto.model";
import DocSpecComponent from "@/modules/cbc/components/DocSpec.vue";
import {
  CbcReportingRole_EnumType,
  OECDLegalAddressType_EnumType
} from "@/modules/cbc/models";
import { required, maxLength } from "vuelidate/lib/validators";

@Component({
  components: {
    DocSpecComponent
  },
  mixins: [validationMixin],
  validations: {
    resCountryCode: {
      required
    },
    tin: {
      required,
      maxLength: maxLength(200)
    },
    in: {
      maxLength: maxLength(200)
    },
    name: {
      maxLength: maxLength(200)
    },
    countryCode: {
      required
    },
    addressType: {},
    addressFree: {
      maxLength: maxLength(4000)
    },
    reportingRole: {}
  }
})
export default class ReportingEntityComponent extends Vue {
  @Prop()
  public countries!: Country[];

  public readonly: boolean = true;

  public reportingRoles = Object.values(CbcReportingRole_EnumType).filter(
    value => typeof value === "string"
  );

  public addressTypes = Object.values(OECDLegalAddressType_EnumType).filter(
    value => typeof value === "string"
  );

  @Watch("resCountryCode")
  onResCountryCodeChanged(value: Country, oldValue: Country) {
    this.$data.countryCode = value;
  }

  public onEdit() {
    this.readonly = !this.readonly;
  }
  public data() {
    return {
      resCountryCode: "",
      tin: "NOTIN",
      in: "",
      name: "",
      countryCode: "",
      addressType:
        OECDLegalAddressType_EnumType[OECDLegalAddressType_EnumType.OECD301],
      addressFree: "",
      reportingRole: ""
    };
  }
}
</script>
<style lang="scss" scoped>
.reporting-entity {
	margin-bottom: 10px;
	.entity {
		margin-bottom: 10px;
	}
	.reporting-role {
		margin-bottom: 10px;
	}
	.doc-spec {
		margin-bottom: 10px;
	}
}
</style>
