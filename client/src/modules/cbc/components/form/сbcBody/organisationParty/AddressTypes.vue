<template>
    <v-container class="pa-0">
        <div class="subtitle-1 text-uppercase mb-2">addresses</div>
        <v-divider class="mb-4"></v-divider>
        <v-row>
            <v-col cols="12">
                <AddressTypeComponent
                        v-for="country in countries"
                        :key="country.alpha2Code"
                        v-model="address"
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
	import AddressTypeComponent from "./AddressType.vue";
	import {Country} from "@/modules/country/models/dto.model";
	import {Address} from "@/modules/cbc/models";

	@Component({
		components: {
			AddressTypeComponent
		}
	})
	export default class AddressTypesComponent extends Vue {
		@Prop()
		public readonly!: boolean;
		@Prop()
		public countries!: Country[];

		public data() {
			return {
				address: {} as Address,
				addresses: [] as Array<Address>
			};
		}

		@Watch("countries")
		onCountriesChanged(values: Country[], oldValues: Country[]) {
			_.forEach<Country>(oldValues, oldValue => {
				if (!_.some(values, value => value.alpha2Code === oldValue.alpha2Code)) {
					const addresses = _.filter(
						this.$data.addresses as Address[],
						x => x.jurisdiction.toString() === oldValue.alpha2Code
					);
					_.forEach(addresses, address => {
						const element = document.getElementById(`${address.id}`);
						if (element) element.remove();
						_.remove(this.$data.addresses, (a: Address) => a.id === address.id);
						this.$emit("input", this.$data.addresses);
					});
				}
			});
		}

		public onAddComponent() {}

		@Watch("address")
		onAddressChanged(value: Address, oldValue: Address) {
			const addresses = this.$data.addresses as Address[];
			const index = _.findIndex(addresses, x => x.id === value.id);

			if (index === -1) addresses.push(value);
			else {
				addresses[index].jurisdiction = value.jurisdiction;
				addresses[index].type = value.type;
				addresses[index].fix = value.fix;
				addresses[index].free = value.free;
			}
		}

		@Watch("addresses", {deep: true})
		onAddressesChanged(values: Address[], oldValues: Address[]) {
			this.onSave();
		}

		public onSave() {
			this.$emit("input", this.$data.addresses);
		}
	}
</script>