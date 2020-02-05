<template>
	<v-container fluid class="pa-0">
		<AdditionalInfoListComponent :additionalInfo="additionalInfo"/>
		<!--        <AdditionalInfoComponent-->
		<!--                :countries="this.$store.state.country.entities"-->
		<!--                :languages="this.$store.state.language.entities"-->
		<!--                :readonly="false"-->
		<!--        />-->
		<v-btn @click="onSaveAndContinue('report.body')" class="ma-2" color="success" outlined tile>
			<v-icon left>mdi-chevron-right-circle</v-icon>
			Continue
		</v-btn>
		<v-btn class="ma-2" color="warning" outlined tile to="reporting-entity">
			<v-icon left>mdi-arrow-left-circle</v-icon>
			Back
		</v-btn>
	</v-container>
</template>
<script lang="ts">
	import AdditionalInfoComponent from "@/modules/cbc/components/form/сbcBody/additionalInfo/AdditionalInfo.vue";
	import AdditionalInfoListComponent
		from "@/modules/cbc/components/form/сbcBody/additionalInfo/AdditionalInfoList.vue";
	import {AdditionalInfoRequest} from "@/modules/cbc/models";
	import {Component, Vue} from "vue-property-decorator";
	import {mapGetters} from "vuex";

	@Component({
		components: {
			AdditionalInfoListComponent,
			AdditionalInfoComponent
		},
		computed: {
			...mapGetters("country", ["countries"]),
			...mapGetters("language", ["languages"]),
			...mapGetters("cbc/report/additionalInformation", ["additionalInfo"])
		},
		mounted() {
			this.$store.dispatch("language/list");
			this.$store.dispatch("cbc/report/get", this.$route.params["reportId"]).then(() => {
				this.$store.dispatch("cbc/report/additionalInformation/list", {reportId: this.$route.params["reportId"]} as AdditionalInfoRequest);
			});
		}
	})
	export default class AdditionalInformationStepView extends Vue {
		public created() {

		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>