<template>
	<v-container class="pa-0">
		<MessageSpecComponent
                :countries="this.$store.state.country.entities"
                :languages="this.$store.state.language.entities"
                :readonly="false"
        />
	</v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";
	import MessageSpecComponent from "@/modules/cbc/components/form/messageSpec/MessageSpec.vue";

	@Component({
		components: {
			MessageSpecComponent
		}
	})
	export default class ReportDataMessageView extends Vue {
		public created() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("language/list");
		}

		public data() {
			return {};
		}
	}
</script>
