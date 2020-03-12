<template>
	<v-container fluid>
		<OrganisationPartyComponent
				v-bind:organisation.sync="reportingEntity.organisation"
				:readonly="readonly"
				:countries="countries"
		/>
		<div class="subtitle-1 text-uppercase mb-2 text-center">Reporting Entity</div>
		<v-divider class="mb-4"></v-divider>
		<v-row>
			<v-col cols="12" lg="5" md="5">
				<v-text-field
						dense
						filled
						v-model="reportingEntity.nameMNEGroup"
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
								v-model="startDateText"
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
								v-model="endDateText"
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
		<DocSpecComponent :readonly="readonly" v-bind:doc.sync="reportingEntity.doc"/>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {ReferenceBook} from "@/core/models";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";
	import OrganisationPartyComponent
		from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {ReportingEntity, ReportingRoleEnum} from "@/modules/cbc/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import moment from "moment";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
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
		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					organisation: {},
					nameMNEGroup: "",
					doc: {},
					role: {},
					startDate: "",
					endDate: ""
				} as ReportingEntity;
			}
		})
		public readonly reportingEntity!: ReportingEntity;

		public get value() {
			return this.reportingEntity;
		}

		public set value(value: ReportingEntity) {
			if (this.$v!.reportingEntity!.$invalid) {
				this.$v!.reportingEntity!.$touch();
			} else
				this.$emit("update:reportingEntity", value);
		}

		@Watch("reportingEntity", {deep: true})
		public onChanged(value: ReportingEntity, oldValue: ReportingEntity) {
			this.value = value;
		}

		public startDateMenu: boolean = false;
		public endDateMenu: boolean = false;

		public get role(): ReferenceBook<ReportingRoleEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.role)) return this.reportingRoles.find(x => x.id === this.value.role);
		}

		public set role(role: ReferenceBook<ReportingRoleEnum> | undefined) {
			if (this.value && role) this.save({role: role.id});
		}

		public get startDate(): string | undefined {
			if (this.value && this.value.startDate) {
				this.startDateText = moment(this.value.startDate).format("YYYY-MM-DD");
				return this.startDateText;
			}
		}

		public startDateText: string = "";

		public set startDate(startDate: string | undefined) {
			if (this.value && startDate) {
				this.save({startDate: startDate});
				this.startDateText = startDate;
			}
		}

		public get endDate(): string | undefined {
			if (this.value && this.value.endDate) {
				this.endDateText = moment(this.value.endDate).format("YYYY-MM-DD");;
				return this.endDateText;
			}
		}

		public endDateText: string = "";

		public set endDate(endDate: string | undefined) {
			if (this.value && endDate) {
				this.save({endDate: endDate});
				this.endDateText = endDate;
			}
		}

		private save(reportingEntity: ReportingEntity | any) {
			this.value = Object.assign(this.value, reportingEntity);
		}
	}
</script>