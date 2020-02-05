<template>
	<v-data-table
			dense
			:headers="headers"
			:items="constituentEntities"
			hide-default-footer
			class="elevation-0"
			@click:row="onClickRow"
	>
		<template v-slot:item.organisation.name="{ item }">{{ item.organisation.name.join(", ") }}</template>
		<template v-slot:item.role="{ item }">{{ onGetNameUltimateParentEntityRole(item.role) }}</template>
	</v-data-table>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {ConstituentEntity, UltimateParentEntityRoleEnum} from "@/modules/cbc/models";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class ConstituentEntityListComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly constituentEntities!: ConstituentEntity[];
		public headers: any[] = [
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
			}
		];

		@Emit("get-constituent-entity")
		public onClickRow(row: ConstituentEntity) {
			return row;
		}

		public onGetNameUltimateParentEntityRole(ultimateParentEntityRole: UltimateParentEntityRoleEnum): string | undefined {
			return this.ultimateParentEntityRoles.find(x => x.id === ultimateParentEntityRole)!.name!;
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
