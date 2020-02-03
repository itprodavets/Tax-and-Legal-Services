<template>
    <v-container class="pa-0">
        <OrganisationPartyComponent
                v-model="$v.reportingEntity.organisation.$model"
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
                        v-model="$v.reportingEntity.role.$model"
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
                                v-model="$v.reportingEntity.startDate.$model"
                                label="Start Date"
                                :disabled="readonly"
                                v-on="on"
                        ></v-text-field>
                    </template>
                    <v-date-picker v-model="$v.reportingEntity.startDate.$model"
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
                                v-model="$v.reportingEntity.endDate.$model"
                                label="End Date"
                                :disabled="readonly"
                                v-on="on"
                        ></v-text-field>
                    </template>
                    <v-date-picker v-model="$v.reportingEntity.endDate.$model"
                                   @input="endDateMenu = false"></v-date-picker>
                </v-menu>
            </v-col>
        </v-row>
        <DocSpecComponent v-model="$v.reportingEntity.doc.$model" :readonly="readonly"/>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {CbcMixin} from "@/modules/cbc/mixins";

	import OrganisationPartyComponent
		from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import DocSpecComponent from "@/modules/cbc/components/form/сbcBody/docSpec/DocSpec.vue";

	import moment from "moment";

	import {ReferenceBook} from "@/core/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {Doc, Organisation, ReportingEntity, ReportingRoleEnum} from "@/modules/cbc/models";

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
		public countries!: Country[];

		@Prop()
		public readonly!: boolean;

		@Watch("reportingEntity", {deep: true})
		onReportingEntityChanged(value: ReportingEntity, oldValue: ReportingEntity) {
			this.onSave();
		}

		@Watch("reportingEntity.organisation", {deep: true})
		onOrganisationChanged(value: Organisation, oldValue: Organisation) {
			this.onCreateDocRefId();
		}

		@Watch("reportingEntity.startDate", {deep: true})
		onStartDate(value: Organisation, oldValue: Organisation) {
			this.onCreateDocRefId();
		}

		private onCreateDocRefId() {
			const organisation = this.$data.reportingEntity
				.organisation as Organisation;

			const jurisdiction =
				organisation.tin.jurisdiction !== undefined
					? organisation.tin.jurisdiction
					: organisation.in.map(x => x.jurisdiction);

			this.$data.reportingEntity.doc = {
				type: this.$data.reportingEntity.doc.type,
				refId: this.onGenerateDocRefId(
					Array.isArray(jurisdiction) ? jurisdiction[0] : jurisdiction,
					this.onGetYear(this.$data.reportingEntity.startDate),
					organisation.jurisdictions
				),
				corrMessageRefId: this.$data.reportingEntity.doc.corrMessageRefId,
				corrDocRefId: this.$data.reportingEntity.doc.corrDocRefId
			} as Doc;
		}

		public data() {
			return {
				startDateMenu: false as boolean,
				endDateMenu: false as boolean,
				reportingEntity: {
					doc: {} as Doc,
					organisation: {} as Organisation,
					nameMNEGroup: "" as string,
					role: {} as ReportingRoleEnum,
					startDate: "" as string,
					endDate: moment().format("YYYY-MM-DD") as string
				} as ReportingEntity
			};
		}

		public onSave() {
			if (this.$v.reportingEntity.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					doc: this.$data.reportingEntity.doc as Doc,
					organisation: this.$data.reportingEntity.organisation as Organisation,
					nameMNEGroup: this.$data.reportingEntity.nameMNEGroup as string,
					role: (this.$data.reportingEntity.role as ReferenceBook)
						.id as ReportingRoleEnum,
					startDate: this.$data.reportingEntity.startDate as string,
					endDate: this.$data.reportingEntity.endDate as string
				} as ReportingEntity;
				this.$emit("input", data);
			}
		}
	}
</script>