<template>
	<v-data-table
			dense
			:headers="headers"
			:items="constituentEntities"
			class="elevation-0"
			hide-default-footer
			@click:row="onClickRow"
	>
		<template v-slot:top>
			<v-toolbar dense class="elevation-0">
				<template>
					<v-btn dense icon @click="onCreate()">
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
				<v-toolbar-title>Constituent Entities</v-toolbar-title>
			</v-toolbar>
		</template>
		<template v-slot:item.organisation.name="{ item }">{{ item.organisation ? item.organisation.name.join(", ") : ""
			}}
		</template>
		<template v-slot:item.role="{ item }">{{ onGetNameUltimateParentEntityRole(item.role) }}</template>
	</v-data-table>
</template>
<script lang="ts">
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {
		ConstituentEntity,
		ConstituentEntityCreateRequest,
		Organisation,
		UltimateParentEntityRoleEnum
	} from "@/modules/cbc/models";
	import _ from "lodash";
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
				value: "organisation.name",
				align: "start"
			},
			{
				text: "TIN",
				value: "organisation.tin.tin",
				align: "start"
			},
			{
				text: "Role",
				value: "role",
				align: "start"
			}
		];

		@Emit("create")
		public onCreate() {
			return {
				reportId: this.$route.params["reportId"],
				constituentEntity: {} as ConstituentEntity
			} as ConstituentEntityCreateRequest
		}

		@Emit("get-constituent-entity")
		public onClickRow(row: ConstituentEntity) {
			return row;
		}

		public onGetNameUltimateParentEntityRole(ultimateParentEntityRole: UltimateParentEntityRoleEnum): string | undefined {
			if (!_.isUndefined(ultimateParentEntityRole))
				return this.ultimateParentEntityRoles.find(x => x.id === ultimateParentEntityRole)!.name!;
			return "";
		}
	}
</script>
