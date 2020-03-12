<template>
	<v-data-table
			dense
			:headers="headers"
			:items="reportData"
			class="elevation-1"
			hide-default-footer
			@click:row="onClickRow"
	>
		<template v-slot:top>
			<v-toolbar class="cbc-data-table-toolbar elevation-0" dense>
				<template>
					<v-btn dense icon @click="onCreate()">
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
				<v-spacer></v-spacer>
				<template>
					<ReportDataParseComponent @parse-file="onParse"/>
					<ReportDataValidateComponent @validate-file="onValidate"/>
				</template>
			</v-toolbar>
		</template>
		<template v-slot:item.message.jurisdiction="{ item }">
			<CompanyDisplayComponent :country="getCountryByCode(item.message.jurisdiction)"
			                         v-if="item.message && item.message.jurisdiction"/>
		</template>
		<template v-slot:item.message.reportingPeriod="{ item }">
			{{ item.message ? new Date(item.message.reportingPeriod).getFullYear() : "" }}
		</template>
		<template v-slot:item.version="{ item }">
			{{ onGetNameSupportedSchema(item.version) }}
		</template>
		<template v-slot:item.action="{ item }">
			<v-btn v-if="item.message && item.message.refId" text small :to="item.id + '/message'">
				<v-icon>mdi-file-export</v-icon>
			</v-btn>
		</template>
	</v-data-table>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import ReportDataParseComponent from "@/modules/cbc/components/form/ReportDataParse.vue";
	import ReportDataValidateComponent from "@/modules/cbc/components/form/ReportDataValidate.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {
		Message,
		ReportData,
		ReportDataCreateRequest,
		ReportDataParseRequest,
		ReportDataValidationRequest,
		SupportedSchema
	} from "@/modules/cbc/models";
	import CompanyDisplayComponent from "@/modules/country/components/CompanyDisplay.vue";
	import {CountryMixin} from "@/modules/country/mixins";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {
			CompanyDisplayComponent,
			ReportDataParseComponent,
			ReportDataValidateComponent
		}
	})
	export default class ReportDataListComponent extends Mixins(CbcMixin, CountryMixin) {
		@Prop()
		public readonly reportData!: ReportData[];
		public headers: any[] = [
			{
				text: "Name",
				value: "message.refId",
				align: "start"
			},
			{
				text: "Jurisdiction",
				value: "message.jurisdiction",
				align: "start"
			},
			{
				text: "Reporting Period",
				value: "message.reportingPeriod",
				align: "start"
			},
			{
				text: "Version",
				value: "version"
			},
			{
				text: 'Action',
				value: 'action',
				sortable: false
			}
		];

		public onClickRow(row: ReportData) {
			this.$router.push({
				name: "cbc.report.detail",
				params: {id: row.id.toString()}
			});
		}

		public onGetNameSupportedSchema(supportedSchema: SupportedSchema): string | undefined {
			return this.supportedSchemas.find(x => x.id === supportedSchema)!.name!;
		}

		@Emit("create")
		public onCreate() {
			return {
				data: {
					id: Guid.create().toString(),
					version: SupportedSchema.OECD_200,
					reports: [],
					message: {} as Message
				} as ReportData
			} as ReportDataCreateRequest;
		}

		@Emit("parse")
		public onParse(request: ReportDataParseRequest) {
			return request;
		}

		@Emit("validate")
		public onValidate(request: ReportDataValidationRequest) {
			return request;
		}
	}
</script>