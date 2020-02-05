<template>
	<v-container fluid class="pa-0">
		<v-card>
			<v-card-text>
				<MessageSpecComponent
						:countries="this.$store.state.country.entities"
						:languages="this.$store.state.language.entities"
						:readonly="false"
				/>
			</v-card-text>
			<v-card-actions>

			</v-card-actions>
		</v-card>
	</v-container>
</template>
<script lang="ts">
	import MessageSpecComponent from "@/modules/cbc/components/form/messageSpec/MessageSpec.vue";
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {
			MessageSpecComponent
		},
		mounted() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("language/list");
		}
	})
	export default class ReportDataMessageView extends Vue {
		public created() {

		}
		public data() {
			return {};
		}
	}
</script>
