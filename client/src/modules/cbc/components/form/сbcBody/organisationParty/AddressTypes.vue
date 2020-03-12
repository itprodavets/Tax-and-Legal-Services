<template>
	<v-data-iterator
			:items="addresses"
			v-bind:items-per-page.sync="itemsPerPage"
			hide-default-footer
	>
		<template v-slot:header>
			<div class="d-flex justify-start align-center">
				<v-btn @click="onAdd()" dense icon>
					<v-icon>mdi-plus-circle</v-icon>
				</v-btn>
				<div class="subtitle-1 text-uppercase text-center" style="width: 100%">Addresses</div>
			</div>
			<v-divider class="mb-4"></v-divider>
		</template>

		<template v-slot:default="{ items }">
			<AddressTypeComponent v-for="item in items" :key="item.id"
			                      :readonly="readonly" v-bind:address.sync="item"
			                      :countries="countries" @remove="onRemove"/>
		</template>
	</v-data-iterator>
</template>
<script lang="ts">
	import {Guid} from "@/core/common/guid";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Address, AddressFix} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins";
	import {Country} from "@/modules/country/models/dto.model";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import AddressTypeComponent from "./AddressType.vue";

	@Component({
		components: {
			AddressTypeComponent
		}
	})
	export default class AddressTypesComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;
		@Prop()
		public readonly countries!: Country[];

		@Prop({
			type: Array, default: () => {
				return [] as Address[];
			}
		})
		public readonly addresses!: Address[];

		public get values(): Address[] {
			return this.addresses;
		}

		public set values(addresses: Address[]) {
			this.$emit("update:addresses", addresses);
		}

		@Watch("addresses", {deep: true})
		public onChanged(values: Address[], oldValues: Address[]) {
			this.values = values;
		}

		public itemsPerPage: number = 5;

		public onAdd() {
			this.values = Object.assign(this.values, this.values.unshift({
				id: Guid.create().toString(),
				type: undefined,
				jurisdiction: {},
				isFixed: false,
				fix: {
					buildingIdentifier: "",
					city: "",
					countrySubentity: "",
					districtName: "",
					floorIdentifier: "",
					pob: "",
					postCode: "",
					street: "",
					suiteIdentifier: ""
				} as AddressFix,
				free: ""
			} as Address));
		}

		public onRemove(id: string | number) {
			this.values = this.values.filter(x => x.id !== id);
		}
	}
</script>