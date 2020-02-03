<template>
    <v-data-table
            dense
            :headers="headers"
            :items="items"
            hide-default-footer
            class="elevation-0"
            @click:row="onClickRow"
    >
        <template v-slot:item.organisation.name="{ item }">{{ item.organisation.name.join(", ") }}</template>
        <template v-slot:item.role="{ item }">{{ onGetNameUltimateParentEntityRole(item.role) }}</template>
    </v-data-table>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {ConstituentEntity} from "@/modules/cbc/models";
	import {CbcMixin} from "@/modules/cbc/mixins";

	@Component({
		components: {}
	})
	export default class ConstituentEntityListComponent extends Mixins(CbcMixin) {
		@Prop()
		constituentEntities!: ConstituentEntity[];

		public data() {
			return {
				headers: [
					{
						text: "Organisation",
						value: "organisation.name"
					},
					{
						text: "TIN",
						value: "organisation.tin.tin"
					},
					{
						text: "Role",
						value: "role"
					},

				],
				items: this.constituentEntities
			};
		}

		public onClickRow(row: ConstituentEntity) {
			this.$emit('get-constituent-entity', row);
		}
	}
</script>
<style lang="scss">
    table {
        tr {
            cursor: pointer;
        }
    }
</style>
