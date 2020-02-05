<template>
	<v-data-table dense :headers="headers"
	              :items="additionalInfo"
	              :expanded.sync="expanded"
	              show-expand
	              hide-default-footer
	              class="elevation-0"
	              @click:row="onClickRow">
		<template v-slot:item.jurisdictions="{ item }">
			{{getNamesByCountries(getCountriesByCodes(item.jurisdictions))}}
		</template>
		<template v-slot:item.summaryTypes="{ item }">
			{{getSummaryTypeNames(item.summaryTypes)}}
		</template>
		<template v-slot:expanded-item="{ item }">
			<td colspan="12" class="pa-0" style="border: none">
				<v-container fluid class="pa-4">
					<v-data-table dense :headers="headersExpand" :items="item.otherInfo"
					              hide-default-footer
					              class="elevation-0">
						<template v-slot:item.language="{ item }">
							{{item.language}}
						</template>
					</v-data-table>
				</v-container>
			</td>
		</template>

	</v-data-table>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {AdditionalInfo, SummaryTypeEnum} from "@/modules/cbc/models";
	import {CountryMixin} from "@/modules/country/mixins";
	import {LanguageMixin} from "@/modules/language/mixins";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component
	export default class AdditionalInfoListComponent extends Mixins(CbcMixin, CountryMixin, LanguageMixin) {
		@Prop({default: () => []})
		public readonly additionalInfo!: AdditionalInfo[];
		public expanded: any[] = [];

		public headers: any[] = [
			{
				text: "jurisdictions",
				value: "jurisdictions"
			},
			{
				text: "Summary Types",
				value: "summaryTypes"
			}
		];
		public headersExpand: any[] = [
			{
				text: "Information",
				value: "info"
			},
			{
				text: "Language",
				value: "language"
			}
		];

		@Emit("get-additional-info")
		public onClickRow(row: AdditionalInfo) {
			return row;
		}

		public getSummaryTypeNames(ids: SummaryTypeEnum[]): string {
			return this.summaryTypes.filter(x => ids.find(y => x.id === y))!.map(x => x.name)!.join(", ");
		}
	}
</script>
<style lang="scss">
	table {
		tr {
			cursor: pointer;
		}
	}

	.v-data-table tbody tr.v-data-table__expanded__content {
		box-shadow: none !important;
	}
</style>