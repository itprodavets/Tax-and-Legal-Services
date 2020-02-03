<template>
	<v-container>
        <AdditionalInfoComponent
                :countries="this.$store.state.country.entities"
                :languages="this.$store.state.language.entities"
                :readonly="false"
        />
        <v-btn @click="onSaveAndContinue('report.body')" class="ma-2" color="success" outlined tile>
            <v-icon left>mdi-chevron-right-circle</v-icon>
            Continue
        </v-btn>
        <v-btn class="ma-2" color="warning" outlined tile to="/cbc-report/list">
            <v-icon left>mdi-arrow-left-circle</v-icon>
            Back
        </v-btn>
    </v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";
	import AdditionalInfoComponent from "@/modules/cbc/components/form/сbcBody/additionalInfo/AdditionalInfo.vue";

	@Component({
		components: {
			AdditionalInfoComponent
		}
	})
	export default class AdditionalInformationStepView extends Vue {
		public created() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("language/list");
		}

		public onSaveAndContinue(name: string) {
			if (this.$router.app.$route.name !== name)
				this.$router.push({name: name});
		}
	}
</script>