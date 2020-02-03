<template>
    <v-container class="pa-0">
        <div class="subtitle-1 text-uppercase mb-2">Identification Numbers</div>
        <v-divider class="mb-4"></v-divider>
        <v-row>
            <v-col cols="12">
                <OrganisationInComponent
                        v-for="country in countries"
                        :key="country.alpha2Code"
                        v-model="iN"
                        :readonly="readonly"
                        :country="country"
                        :countries="countries"
                />
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import _ from "lodash";
	import {Component, Prop, Vue, Watch} from "vue-property-decorator";
	import {Country} from "@/modules/country/models/dto.model";
	import {In} from "@/modules/cbc/models";

	import OrganisationInComponent from "./OrganisationIn.vue";

	@Component({
		components: {
			OrganisationInComponent
		}
	})
	export default class OrganisationInsComponent extends Vue {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public countries!: Country[];

		@Prop()
		public value!: In[];

		public data() {
			return {
				iN: {} as In,
				ins: [] as Array<In>
			};
		}

		@Watch("countries")
		onCountriesChanged(values: Country[], oldValues: Country[]) {
			_.forEach<Country>(oldValues, oldValue => {
				if (!_.some(values, value => value.alpha2Code === oldValue.alpha2Code)) {
					const ins = _.filter(
						this.$data.ins as In[],
						x => x.jurisdiction.toString() == oldValue.alpha2Code
					);
					_.forEach(ins, iN => {
						const element = document.getElementById(`${iN.id}`);
						if (element) element.remove();
						_.remove(this.$data.ins, (a: any) => a.id === iN.id);
						this.$emit("input", this.$data.ins);
					});
				}
			});
		}

		public onAddComponent() {}

		@Watch("iN")
		onInChanged(value: In, oldValue: In) {
			const ins = this.$data.ins as In[];
			const index = _.findIndex(ins, x => x.id === value.id);

			if (index === -1) ins.push(value);
			else {
				ins[index].jurisdiction = value.jurisdiction;
				ins[index].in = value.in;
				ins[index].type = value.type;
			}
		}

		@Watch("ins", {deep: true})
		onInsChanged(values: In[], oldValues: In[]) {
			this.onSave();
		}

		public onSave() {
			this.$emit("input", this.$data.ins);
		}
	}
</script>