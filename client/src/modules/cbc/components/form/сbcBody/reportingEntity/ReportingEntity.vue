<template>
	<v-container fluid>
		<OrganisationPartyComponent
				:organisation.sync="$v.reportingEntity.organisation.$model"
				:readonly="readonly"
				:countries="countries"
		/>
		<div class="subtitle-1 text-uppercase mb-2">Reporting Entity</div>
		<v-divider class="mb-4"></v-divider>
		<v-row>
			<v-col cols="12" lg="5" md="5">
				<v-text-field
						dense
						filled
						v-model="$v.reportingEntity.nameMNEGroup.$model"
						label="Name MNE Group"
						:disabled="readonly"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="3" md="3">
				<v-autocomplete
						dense
						filled
						v-model="role"
						:items="reportingRoles"
						item-text="name"
						label="Reporting Roles"
						:disabled="readonly"
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="2" md="2">
				<v-menu
						v-model="startDateMenu"
						:close-on-content-click="false"
						:nudge-right="40"
						transition="scale-transition"
						offset-y
				>
					<template v-slot:activator="{ on }">
						<v-text-field
								dense
								filled
								v-model="startDate"
								label="Start Date"
								:disabled="readonly"
								v-on="on"
						></v-text-field>
					</template>
					<v-date-picker v-model="startDate"
					               @input="startDateMenu = false"></v-date-picker>
				</v-menu>
			</v-col>
			<v-col cols="12" lg="2" md="2">
				<v-menu
						v-model="endDateMenu"
						:close-on-content-click="false"
						:nudge-right="40"
						transition="scale-transition"
						offset-y
				>
					<template v-slot:activator="{ on }">
						<v-text-field
								dense
								filled
								v-model="endDate"
								label="End Date"
								:disabled="readonly"
								v-on="on"
						></v-text-field>
					</template>
					<v-date-picker v-model="endDate"
					               @input="endDateMenu = false"></v-date-picker>
				</v-menu>
			</v-col>
		</v-row>
		<DocSpecComponent :doc.sync="reportingEntity.doc" :readonly="readonly"/>
	</v-container>
</template>
<script lang="ts">
	import {ReferenceBook} from "@/core/models";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";
	import OrganisationPartyComponent from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {ReportingEntity, ReportingRoleEnum} from "@/modules/cbc/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import moment from "moment";
	import {Component, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";

	@Component({
		components: {
			OrganisationPartyComponent,
			DocSpecComponent
		},
		mixins: [validationMixin],
		validations: {
			reportingEntity: {
				doc: {},
				organisation: {},
				nameMNEGroup: {},
				role: {},
				startDate: {},
				endDate: {}
			}
		}
	})
	export default class ReportingEntityComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@PropSync('reportingEntity', {
			type: Object, default: () => {
				return {
					id: "",
					organisation: {},
					nameMNEGroup: "",
					doc: {},
					role: {},
					startDate: "",
					endDate: ""
				} as ReportingEntity;
			}
		})
		public value!: ReportingEntity;

		public startDateMenu: boolean = false;
		public endDateMenu: boolean = false;

		public get role(): ReferenceBook<ReportingRoleEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.role))
				return this.reportingRoles.find(x => x.id === this.value.role);
		}

		public set role(role: ReferenceBook<ReportingRoleEnum> | undefined) {
			if (this.value && role)
				this.value = Object.assign(this.value, {
					id: this.value.id,
					organisation: this.value.organisation,
					nameMNEGroup: this.value.nameMNEGroup,
					doc: this.value.doc,
					role: role.id,
					startDate: this.value.startDate,
					endDate: this.value.endDate
				} as ReportingEntity);
		}

		public get startDate(): string | undefined {
			if (this.value && this.value.startDate)
				return moment(this.value.startDate).format("YYYY-MM-DD");
		}

		public set startDate(startDate: string | undefined) {
			if (startDate)
				this.value = {
					id: this.value.id,
					organisation: this.value.organisation,
					nameMNEGroup: this.value.nameMNEGroup,
					doc: this.value.doc,
					role: this.value.role,
					startDate: startDate,
					endDate: this.value.endDate
				} as ReportingEntity;
		}

		public get endDate(): string | undefined {
			if (this.value && this.value.endDate)
				return moment(this.value.endDate).format("YYYY-MM-DD");
		}

		public set endDate(endDate: string | undefined) {
			if (endDate)
				this.value = {
					id: this.value.id,
					organisation: this.value.organisation,
					nameMNEGroup: this.value.nameMNEGroup,
					doc: this.value.doc,
					role: this.value.role,
					startDate: this.value.startDate,
					endDate: endDate
				} as ReportingEntity;
		}
	}
</script>