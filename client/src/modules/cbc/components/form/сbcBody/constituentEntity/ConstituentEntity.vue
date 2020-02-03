<template>
    <v-container fluid class="pa-0">
        <OrganisationPartyComponent
                v-model="$v.constituentEntity.organisation.$model"
                :readonly="readonly"
                :countries="countries"
        />
        <div class="subtitle-1 text-uppercase mb-2">Constituent Entity</div>
        <v-row>
            <v-col cols="12" lg="3" md="3" class="pa-0 px-3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.constituentEntity.jurisdiction.$model"
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
                        v-model="$v.constituentEntity.role.$model"
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
                        v-model="$v.constituentEntity.bizActivities.$model"
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
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
    import {CountryEnum} from "@/modules/country/models";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
    import {BizActivityTypeEnum, ConstituentEntity, Organisation, UltimateParentEntityRoleEnum} from "@/modules/cbc/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {CbcMixin} from "@/modules/cbc/mixins";

	import OrganisationPartyComponent
		from "@/modules/cbc/components/form/сbcBody/organisationParty/OrganisationParty.vue";
	import {ReferenceBook} from "@/core/models";

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
		public readonly countries!: Country[];

		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public value!: ConstituentEntity;

		@Watch("constituentEntity", {deep: true})
		onConstituentEntityChanged(value: ConstituentEntity, oldValue: ConstituentEntity) {
			this.onSave();
		}

		public data() {
			return {
				constituentEntity: {
					id: "",
					role: {} as ReferenceBook<UltimateParentEntityRoleEnum>,
					jurisdiction: {} as Country,
					bizActivities: [] as ReferenceBook<BizActivityTypeEnum>[],
					otherInfo: "",
					organisation: {} as Organisation
				}
			};
		}

		public created() {
			if (this.value && this.value.id)
				this.$data.constituentEntity = {
					id: this.value.id,
					role: this.ultimateParentEntityRoles.find(x => x.id == this.value.role),
					jurisdiction: this.countries.find(x => x.alpha2Code == CountryEnum[this.value.jurisdiction]),
					bizActivities: this.bizActivityTypes.filter(x => this.value.bizActivities.find(y => y === x.id)),
					otherInfo: this.value.otherInfo,
					organisation: this.value.organisation
				};
		}

		public onSave() {
			if (this.$v.constituentEntity.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const organisation = this.$data.constituentEntity.organisation as Organisation;
				if (organisation) {
					const data = {
						id: this.$data.constituentEntity.id,
						role: (this.$data.constituentEntity.role as ReferenceBook<UltimateParentEntityRoleEnum>).id,
						jurisdiction: this.getCountryEnum(this.$data.constituentEntity.jurisdiction),
						bizActivities: (this.$data.constituentEntity.bizActivities as ReferenceBook<BizActivityTypeEnum>[]).map(x => x.id),
						otherInfo: this.$data.constituentEntity.otherInfo,
						organisation: organisation
					} as ConstituentEntity;
					this.$emit("input", data);
				}
			}
		}
	}
</script>
