<template>
	<v-container fluid class="pa-0">
        <OrganisationPartyComponent
				:organisation.sync="$v.constituentEntity.organisation.$model"
				:readonly="readonly"
				:countries="countries"
		/>
		<div class="subtitle-1 text-uppercase mb-2">Constituent Entity</div>
		<v-row>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-autocomplete
						dense
						filled
						v-model="jurisdiction"
						:items="countries"
						item-text="name"
						label="Jurisdiction"
						:disabled="readonly"
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="3" md="3" class="pa-0 px-3">
				<v-autocomplete
						dense
						filled
						v-model="ultimateParentEntityRole"
						:items="ultimateParentEntityRoles"
						item-text="name"
						label="Ultimate Parent Entity Role"
						:disabled="readonly"
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="6" md="6" class="pa-0 px-3">
				<v-autocomplete
						dense
						filled
						v-model="bizActivities"
						:items="bizActivityTypes"
						item-text="name"
						label="Biz Activity Types"
						:disabled="readonly"
						multiple
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="12" md="12">
				<v-textarea
						dense
						filled
						v-model="$v.constituentEntity.otherInfo.$model"
						label="Other Info"
						rows="5"
						:disabled="readonly"
				></v-textarea>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {ReferenceBook} from "@/core/models";
	import OrganisationPartyComponent from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {BizActivityTypeEnum, ConstituentEntity, UltimateParentEntityRoleEnum} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
    import _ from "lodash";
	import {Component, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";

	@Component({
		components: {
			OrganisationPartyComponent
		},
		mixins: [validationMixin],
		validations: {
			constituentEntity: {
				organisation: {},
				role: {},
				jurisdiction: {},
				bizActivities: {},
				otherInfo: {}
			}
		}
	})
	export default class ConstituentEntityComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@PropSync('constituentEntity', {
			type: Object, default: () => {
				return {
					id: "",
					organisation: {},
					role: {},
					otherInfo: "",
					jurisdiction: {},
					bizActivities: {}
				} as ConstituentEntity;
			}
		})
		public value!: ConstituentEntity;

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction))
				return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction]);
		}

		public set jurisdiction(country: Country | undefined) {
			if (country)
				this.value = Object.assign(this.value, {
					id: this.value.id,
					organisation: this.value.organisation,
					role: this.value.role,
					otherInfo: this.value.otherInfo,
					jurisdiction: this.getCountryEnum(country),
					bizActivities: this.value.bizActivities
				} as ConstituentEntity);
		}

		public get ultimateParentEntityRole(): ReferenceBook<UltimateParentEntityRoleEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.role))
				return this.ultimateParentEntityRoles.find(x => x.id === this.value.role);
		}

		public set ultimateParentEntityRole(role: ReferenceBook<UltimateParentEntityRoleEnum> | undefined) {
			if (this.value && role)
				this.value = Object.assign(this.value, {
					id: this.value.id,
					organisation: this.value.organisation,
					role: role.id,
					otherInfo: this.value.otherInfo,
					jurisdiction: this.value.jurisdiction,
					bizActivities: this.value.bizActivities
				} as ConstituentEntity);
		}

		public get bizActivities(): ReferenceBook<BizActivityTypeEnum>[] | undefined {
			if (this.value && this.value.bizActivities)
				return this.bizActivityTypes.filter(x => this.value.bizActivities.find(y => y === x.id));
		}

		public set bizActivities(bizActivities: ReferenceBook<BizActivityTypeEnum>[] | undefined) {
			if (this.value && bizActivities)
				this.value = Object.assign(this.value, {
					id: this.value.id,
					organisation: this.value.organisation,
					role: this.value.role,
					otherInfo: this.value.otherInfo,
					jurisdiction: this.value.jurisdiction,
					bizActivities: this.value.bizActivities
				} as ConstituentEntity);
		}
	}
</script>
