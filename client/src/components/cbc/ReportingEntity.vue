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
					<v-col cols="12" lg="3" md="2">
						<v-text-field
							dense
							filled
							v-model="$v.resCountryCode.$model"
							label="Res Country Code"
							:disabled="readonly"
						></v-text-field>
					</v-col>
					<v-col class="tin" cols="12" lg="2" md="2">
						<v-text-field dense filled v-model="$v.tin.$model" label="TIN" :disabled="readonly"></v-text-field>
					</v-col>
					<v-col class="in" cols="12" lg="2" md="2">
						<v-text-field dense filled v-model="$v.in.$model" label="IN" :disabled="readonly"></v-text-field>
					</v-col>
					<v-col class="name" cols="12" lg="5" md="2">
						<v-text-field dense filled v-model="$v.name.$model" label="Name" :disabled="readonly"></v-text-field>
					</v-col>
					<v-col class="address" cols="12">
						<v-row>
							<v-col class="country-code" cols="12" lg="2" md="2">
								<v-text-field
									dense
									filled
									v-model="$v.countryCode.$model"
									label="Country Code"
									:disabled="readonly"
								></v-text-field>
							</v-col>
							<v-col class="address-free" cols="12" lg="10" md="2">
								<v-textarea
									dense
									filled
									v-model="$v.addressFree.$model"
									label="Address Free"
									rows="1"
									:disabled="readonly"
								></v-textarea>
							</v-col>
						</v-row>
					</v-col>
				</v-row>
				<v-row class="reporting-role">
					<v-col cols="12">
						<v-text-field
							dense
							filled
							v-model="$v.reportingRole.$model"
							label="Reporting Role"
							:disabled="readonly"
						></v-text-field>
					</v-col>
				</v-row>
				<v-row class="doc-spec">
					<v-col cols="12">
						<v-row>
							<v-col cols="12">
								<v-text-field
									dense
									filled
									v-model="$v.docType.$model"
									label="Doc Type"
									:disabled="readonly"
								></v-text-field>
							</v-col>
						</v-row>
						<v-row>
							<v-col cols="12">
								<v-text-field
									dense
									filled
									v-model="$v.docRefId.$model"
									label="Doc Ref Id"
									:disabled="readonly"
								></v-text-field>
							</v-col>
						</v-row>
						<v-row>
							<v-col cols="12">
								<v-text-field
									dense
									filled
									v-model="$v.corrMessageRefId.$model"
									label="Corr Message Ref Id"
									:disabled="readonly"
								></v-text-field>
							</v-col>
						</v-row>
						<v-row>
							<v-col cols="12">
								<v-text-field
									dense
									filled
									v-model="$v.corrDocRefId.$model"
									label="Corr Doc Ref Id"
									:disabled="readonly"
								></v-text-field>
							</v-col>
						</v-row>
					</v-col>
				</v-row>
			</v-container>
		</v-card>
	</div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import { validationMixin } from "vuelidate";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {
    resCountryCode: {},
    tin: {},
    in: {},
    name: {},
    countryCode: {},
    addressFree: {},
    reportingRole: {},
    docType: {},
    docRefId: {},
    corrMessageRefId: {},
    corrDocRefId: {}
  }
})
export default class ReportingEntityComponent extends Vue {
  public readonly: boolean = true;

  public onEdit() {
    this.readonly = !this.readonly;
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
