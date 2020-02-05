<template>
	<v-container fluid class="pa-0">
		<div v-if="drawer">
			<v-toolbar dense class="elevation-1">
				<template>
					<v-btn dense icon @click.stop="drawer = !drawer">
						<v-icon>mdi-plus-circle</v-icon>
					</v-btn>
				</template>
			</v-toolbar>
			<ConstituentEntityListComponent class="pb-10" :constituentEntities="constituentEntities" @get-constituent-entity="onEdit"/>
			<v-btn class="ma-2" tile outlined color="success" :disabled="constituentEntities.length === 0" @click="onSaveAndContinue('reporting.entity')">
				<v-icon left>mdi-chevron-right-circle</v-icon>
				Continue
			</v-btn>
			<v-btn class="ma-2" tile outlined color="warning" to="/cbc-report/list">
				<v-icon left>mdi-arrow-left-circle</v-icon>
				Back
			</v-btn>
		</div>
		<v-navigation-drawer v-model="drawer" permanent class="constituent-entity">
			<v-form class="pa-4" v-if="!drawer && constituentEntity">
				<ConstituentEntityComponent :constituentEntity.sync="constituentEntity" :readonly="false" :countries="this.$store.state.country.entities"/>
				<v-btn class="ma-2" tile outlined color="success" @click="onCreate()">
					<v-icon left>mdi-plus-circle</v-icon>
					{{ constituentEntity.id ? "Save" : "Add" }}
				</v-btn>
				<v-btn class="ma-2" tile outlined color="warning" @click="drawer = !drawer">
					<v-icon left>mdi-arrow-left-circle</v-icon>
					Back
				</v-btn>
			</v-form>
		</v-navigation-drawer>
	</v-container>
</template>
<script lang="ts">
	import ConstituentEntityComponent from "@/modules/cbc/components/form/сbcBody/constituentEntity/ConstituentEntity.vue";
	import ConstituentEntityListComponent from "@/modules/cbc/components/form/сbcBody/constituentEntity/ConstituentEntityList.vue";
	import {ConstituentEntity, ConstituentEntityCreateRequest, ConstituentEntityRequest} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";
	import {mapGetters} from "vuex";

	@Component({
		components: {
			ConstituentEntityListComponent,
			ConstituentEntityComponent
		},
		computed: {
			...mapGetters("cbc/report/constituentEntity", ["constituentEntities"])
		},
		mounted() {
			const request = {reportId: this.$route.params["reportId"]} as ConstituentEntityRequest;
			this.$store.dispatch("country/list");
			this.$store.dispatch("cbc/report/get", request.reportId).then(() => {
				this.$store.dispatch("cbc/report/constituentEntity/list", request);
			});
		}
	})
	export default class ConstituentEntityStepView extends Vue {
		public drawer: boolean = false;
		public constituentEntity: ConstituentEntity = {} as ConstituentEntity;

		public onCreate() {
			const createRequest = {reportId: this.$route.params["reportId"], constituentEntity: this.constituentEntity} as ConstituentEntityCreateRequest;
			this.constituentEntity = {} as ConstituentEntity;
			if (createRequest.constituentEntity)
				if (createRequest.constituentEntity.id)
					this.$store.dispatch("cbc/report/constituentEntity/update", createRequest).then(() => {
						this.drawer = !this.drawer;
					});
				else
					this.$store.dispatch("cbc/report/constituentEntity/create", createRequest).then(() => {
						this.drawer = !this.drawer;
					});

		}

		public onEdit(c: ConstituentEntity) {
			this.$store.dispatch("cbc/report/constituentEntity/get", c.id).then(() => {
				this.constituentEntity = this.$store.state.cbc.report.constituentEntity.entity;
				this.drawer = !this.drawer;
			});
		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>
<style lang="scss" scoped>
	.constituent-entity {
		width: 100% !important;
	}
</style>