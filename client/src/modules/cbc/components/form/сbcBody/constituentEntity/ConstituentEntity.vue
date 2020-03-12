<template>
	<v-container fluid>
		<OrganisationPartyComponent
				v-bind:organisation.sync="constituentEntity.organisation"
				:readonly="readonly"
				:countries="countries"
		/>
		<div class="subtitle-1 text-uppercase mb-2 text-center">Constituent Entity</div>
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
						v-model="constituentEntity.otherInfo"
						label="Other Info"
						rows="5"
						:disabled="readonly"
				></v-textarea>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {ReferenceBook} from "@/core/models";
	import OrganisationPartyComponent
		from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {
		BizActivityTypeEnum,
		ConstituentEntity,
		Organisation,
		UltimateParentEntityRoleEnum
	} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import _ from "lodash";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
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

		@Prop({
			type: Object, default: () => {
				return {
					id: Guid.create().toString(),
					organisation: {} as Organisation,
					role: undefined,
					jurisdiction: undefined,
					otherInfo: "",
					bizActivities: [] as BizActivityTypeEnum[]
				} as ConstituentEntity;
			}
		})
		public readonly constituentEntity!: ConstituentEntity;

		public get value() {
			return this.constituentEntity;
		}

		public set value(value: ConstituentEntity) {
			if (this.$v!.constituentEntity!.$invalid) {
				this.$v!.constituentEntity!.$touch();
			} else
				this.$emit("update:constituentEntity", value);
		}

		@Watch("constituentEntity", {deep: true})
		public onChanged(value: ConstituentEntity, oldValue: ConstituentEntity) {
			this.value = value;
		}

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction)) {
				const countryEnum = CountryEnum[this.value.jurisdiction];
				return this.countries.find(x => x.alpha2Code === countryEnum);
			}
		}

		public set jurisdiction(country: Country | undefined) {
			if (this.value && country) this.save({jurisdiction: this.getCountryEnum(country)});
		}

		public get ultimateParentEntityRole(): ReferenceBook<UltimateParentEntityRoleEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.role)) return this.ultimateParentEntityRoles.find(x => x.id === this.value.role);
		}

		public set ultimateParentEntityRole(role: ReferenceBook<UltimateParentEntityRoleEnum> | undefined) {
			if (this.value && role) this.save({role: role.id});
		}

		public get bizActivities(): ReferenceBook<BizActivityTypeEnum>[] {
			if (this.value && this.value.bizActivities)
				return this.bizActivityTypes.filter(x => this.value.bizActivities.find(y => y === x.id));
			return [];
		}

		public set bizActivities(bizActivities: ReferenceBook<BizActivityTypeEnum>[]) {
			if (this.value && bizActivities)
				this.save({bizActivities: bizActivities.map(x => x.id)});
		}

		private save(constituentEntity: ConstituentEntity | any) {
			this.value = Object.assign(this.value, constituentEntity);
		}
	}
</script>
