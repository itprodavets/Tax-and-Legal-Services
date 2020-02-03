<template>
    <v-container class="pa-0">
        <div v-if="drawer">
            <v-toolbar dense class="elevation-1">
                <template>
                    <v-btn dense icon @click.stop="drawer = !drawer">
                        <v-icon>mdi-plus-circle</v-icon>
                    </v-btn>
                </template>
            </v-toolbar>
            <ConstituentEntityListComponent class="pb-10"
                                            :constituentEntities="constituentEntities"
                                            @get-constituent-entity="onEdit"/>
            <v-btn class="ma-2" tile outlined color="success"
                   :disabled="invalid"
                   @click="onSaveAndContinue('reporting.entity')">
                <v-icon left>mdi-chevron-right-circle</v-icon>
                Continue
            </v-btn>
            <v-btn class="ma-2" tile outlined color="warning" to="/cbc-report/list">
                <v-icon left>mdi-arrow-left-circle</v-icon>
                Back
            </v-btn>
        </div>
        <v-navigation-drawer v-model="drawer" permanent class="constituent-entity">
            <v-form class="pa-4" v-if="!drawer">
                <ConstituentEntityComponent v-model="constituentEntity" :readonly="false"
                                            :countries="this.$store.state.country.entities"/>
                <v-btn class="ma-2" tile outlined color="success"
                       @click="onCreate()">
                    <v-icon left>mdi-plus-circle</v-icon>
                    {{constituentEntity.id ? "Save" : "Add" }}
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
	import {Component, Vue} from "vue-property-decorator";
	import ConstituentEntityListComponent
		from "@/modules/cbc/components/form/сbcBody/constituentEntity/ConstituentEntityList.vue";
	import ConstituentEntityComponent
		from "@/modules/cbc/components/form/сbcBody/constituentEntity/ConstituentEntity.vue";
	import {ConstituentEntity, ConstituentEntityCreateRequest, ConstituentEntityRequest} from "@/modules/cbc/models";

	@Component({
		components: {
			ConstituentEntityListComponent,
			ConstituentEntityComponent
		}
	})
	export default class ConstituentEntityStepView extends Vue {
		public get invalid() {
			return this.constituentEntities.length === 0;
		}

		public get constituentEntities(): Array<ConstituentEntity> {
			return this.$store.state.cbc.report.constituentEntity.entities;
		}

		public get reportId() {
			return this.$route.params["reportId"];
		}

		public data() {
			return {
				drawer: false as boolean,
				constituentEntity: {} as ConstituentEntity
			};
		}

		public created() {
			const request = {
				reportId: this.reportId
			} as ConstituentEntityRequest;
			this.$store.dispatch("country/list");
			this.$store.dispatch("cbc/report/constituentEntity/list", request);
		}

		public onCreate() {
			const createRequest = {
				reportId: this.reportId,
				constituentEntity: this.$data.constituentEntity
			} as ConstituentEntityCreateRequest;
			this.$data.constituentEntity = null;
			if (createRequest.constituentEntity)
				this.$store.dispatch("cbc/report/constituentEntity/create", createRequest);
		}

		public onEdit(c: ConstituentEntity) {
			this.$store.dispatch("cbc/report/constituentEntity/get", c.id).then(() => {
				this.$data.constituentEntity = this.$store.state.cbc.report.constituentEntity.entity;
				this.$data.drawer = !this.$data.drawer;
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