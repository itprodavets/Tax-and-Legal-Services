<template>
	<v-container fluid>
		<DocSpecComponent :readonly="readonly" v-bind:doc.sync="additionalInfo.doc"/>
		<v-row>
			<v-col cols="12" lg="6" md="6">
				<v-autocomplete
						:disabled="readonly"
						:items="countries"
						dense
						filled
						item-text="name"
						label="Jurisdictions"
						multiple
						return-object
						v-model="jurisdictions"
				>
				</v-autocomplete>
			</v-col>
			<v-col cols="12" lg="6" md="6">
				<v-autocomplete
						:disabled="readonly"
						:items="summaryTypes"
						dense
						filled
						item-text="name"
						label="Summary Ref"
						multiple
						return-object
						v-model="summaries"
				></v-autocomplete>
			</v-col>
			<v-col cols="12">
				<v-data-iterator :items="otherInfo" v-bind:items-per-page.sync="itemsPerPage"
				                 hide-default-footer>
					<template v-slot:header>
						<div class="d-flex justify-start align-center">
							<v-btn @click="onAdd()" dense icon>
								<v-icon>mdi-plus-circle</v-icon>
							</v-btn>
							<div class="subtitle-1 text-uppercase" style="width: 100%">Info</div>
						</div>
						<v-divider class="mb-4"></v-divider>
					</template>
					<template v-slot:default="{ items }">
						<OtherInfoComponent :key="item.id" :languages="languages"
						                    :readonly="readonly" v-bind:otherInfo.sync="item"
						                    @remove="onRemove" v-for="item in items"/>
					</template>
				</v-data-iterator>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {ReferenceBook} from "@/core/models";
	import OtherInfoComponent from "@/modules/cbc/components/form/сbcBody/additionalInfo/OtherInfo.vue";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {AdditionalInfo, OtherInfo, SummaryTypeEnum} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {LanguageEnumMixin} from "@/modules/language/mixins";
	import {LanguageEnum} from "@/modules/language/models";
	import {Language} from "@/modules/language/models/dto.model";
	import _ from "lodash";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";

	@Component({
		components: {
			DocSpecComponent,
			OtherInfoComponent
		},
		mixins: [validationMixin],
		validations: {
			additionalInfo: {
				doc: {},
				jurisdictions: {},
				otherInfo: {},
				summaryTypes: {}
			}
		}
	})
	export default class AdditionalInfoComponent extends Mixins(CbcMixin, CountryEnumMixin, LanguageEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly languages!: Language[];
		@Prop()
		public readonly countries!: Country[];

		@Prop({
			default: () => {
				return {
					id: Guid.create().toString(),
					doc: {},
					jurisdictions: [] as CountryEnum[],
					otherInfo: [] as OtherInfo[],
					summaryTypes: [] as SummaryTypeEnum[]
				} as AdditionalInfo;
			}
		})
		public readonly additionalInfo!: AdditionalInfo;

		public get value(): AdditionalInfo {
			return this.additionalInfo
		}

		public set value(additionalInfo: AdditionalInfo) {
			if (this.$v!.additionalInfo!.$invalid) {
				this.$v!.additionalInfo!.$touch();
			} else
				this.$emit("update:additionalInfo", additionalInfo);
		}

		@Watch("additionalInfo", {deep: true})
		public onChanged(value: AdditionalInfo, oldValue: AdditionalInfo) {
			this.value = value;
		}

		public itemsPerPage: number = 5;
		public otherInfo: OtherInfo[] = this.value.otherInfo || new Array<OtherInfo>();

		@Watch("otherInfo", {deep: true})
		public onOtherInfoChanged(values: OtherInfo[], oldValues: OtherInfo[]) {
			this.save({otherInfo: values.filter(x => !_.isUndefined(x.language))});
		}

		public get jurisdictions(): Country[] | undefined {
			if (this.value && this.value.jurisdictions)
				return this.countries.filter(x => this.value.jurisdictions.find(y => CountryEnum[y] === x.alpha2Code));
		}

		public set jurisdictions(countries: Country[] | undefined) {
			this.save({jurisdictions: (countries && countries.length > 0) ? this.getCountryEnums(countries) : []});
		}

		public get summaries(): ReferenceBook<SummaryTypeEnum>[] | undefined {
			if (this.value && this.value.summaryTypes)
				return this.summaryTypes.filter(x => this.value.summaryTypes.find(y => y === x.id));
		}

		public set summaries(summaryTypes: ReferenceBook<SummaryTypeEnum>[] | undefined) {
			this.save({summaryTypes: (summaryTypes && summaryTypes.length > 0) ? summaryTypes.map(x => x.id) : []});
		}

		public onAdd() {
			this.otherInfo.unshift({
				id: Guid.create().toString(),
				info: ""
			} as OtherInfo);
		}

		public onRemove(id: string | number) {
			this.save({otherInfo: this.otherInfo.filter(x => x.id !== id)})
		}

		private save(additionalInfo: AdditionalInfo | any) {
			this.value = Object.assign(this.value, additionalInfo);
		}
	}
</script>
