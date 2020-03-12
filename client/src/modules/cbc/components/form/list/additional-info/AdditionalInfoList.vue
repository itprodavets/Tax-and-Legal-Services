<template>
	<v-data-table dense :headers="headers"
	              :items="additionalInfo"
	              v-bind:expanded.sync="expanded"
	              show-expand
	              hide-default-footer
	              class="elevation-0"
	              @click:row="onClickRow">
		<template v-slot:top>
			<v-toolbar dense class="elevation-0">
				<template>
					<v-btn dense icon @click="onCreate()">
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
				<v-toolbar-title>Additional Info</v-toolbar-title>
			</v-toolbar>
		</template>
		<template v-slot:item.jurisdictions="{ item }">
			<CompanyDisplayComponent :countries="getCountriesByCodes(item.jurisdictions)"/>
		</template>
		<template v-slot:item.summaryTypes="{ item }">
			{{getSummaryTypeNames(item.summaryTypes)}}
		</template>
		<template v-slot:expanded-item="{ item }">
			<td colspan="12" class="pa-0" style="border: none">
				<v-container fluid class="pa-2">
					<v-data-table dense :headers="headersExpand" :items="item.otherInfo"
					              class="elevation-0"
					              hide-default-header
					              hide-default-footer>
						<template v-slot:item.language="{ item }">
							{{getNamesByLanguages(getLanguageByCode(item.language))}}
						</template>
					</v-data-table>
				</v-container>
			</td>
		</template>
	</v-data-table>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {AdditionalInfo, AdditionalInfoCreateRequest, SummaryTypeEnum} from "@/modules/cbc/models";
	import CompanyDisplayComponent from "@/modules/country/components/CompanyDisplay.vue";
	import {CountryMixin} from "@/modules/country/mixins";
	import {LanguageMixin} from "@/modules/language/mixins";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {
			CompanyDisplayComponent
		}
	})
	export default class AdditionalInfoListComponent extends Mixins(CbcMixin, CountryMixin, LanguageMixin) {

		@Prop({default: () => []})
		public readonly additionalInfo!: AdditionalInfo[];

		public expanded: any[] = [];

		public headers: any[] = [
			{
				text: "jurisdictions",
				value: "jurisdictions",
				align: "start"
			},
			{
				text: "Summary Types",
				value: "summaryTypes",
				align: "start"
			}
		];
		public headersExpand: any[] = [
			{
				text: "Information",
				value: "info",
				align: "start"
			},
			{
				text: "Language",
				value: "language",
				align: "start"
			}
		];

		@Emit("create")
		public onCreate() {
			return {
				reportId: this.$route.params["reportId"],
				additionalInfo: {}
			} as AdditionalInfoCreateRequest
		}

		@Emit("get-additional-info")
		public onClickRow(row: AdditionalInfo) {
			return row;
		}

		public getSummaryTypeNames(ids: SummaryTypeEnum[]): string {
			if (ids && ids.length > 0)
				return this.summaryTypes.filter(x => ids.find(y => x.id === y))!.map(x => x.name)!.join(", ");
			else return "";
		}
	}
</script>
<style lang="scss">
	.v-data-table tbody tr.v-data-table__expanded__content {
		box-shadow: none !important;
	}
</style>