<template>
	<v-container fluid class="pa-0">
		<v-card>
			<v-card-text>
				<MessageSpecComponent
						v-bind:message.sync="message"
						:countries="this.$store.state.country.entities"
						:languages="this.$store.state.language.entities"
						:readonly="false"
				/>
			</v-card-text>
			<v-card-actions class="justify-center">
				<v-btn class="ma-2" tile outlined color="warning" to="report/list">
					<v-icon left>mdi-arrow-left-circle</v-icon>
					Back
				</v-btn>
				<v-btn class="ma-2" tile outlined color="success" @click="onGenerate()">
					<v-icon left>mdi-chevron-right-circle</v-icon>
					Get XML
				</v-btn>
			</v-card-actions>
		</v-card>
	</v-container>
</template>
<script lang="ts">
	import MessageSpecComponent from "@/modules/cbc/components/form/messageSpec/MessageSpec.vue";
	import {
		ConstituentEntity,
		Message,
		ReportData,
		ReportDataGenerateRequest,
		ReportDataUpdateMessageRequest
	} from "@/modules/cbc/models";
	import {Component, Vue, Watch} from "vue-property-decorator";

	@Component({
		components: {
			MessageSpecComponent
		},
		mounted() {
			this.$store.dispatch("cbc/get_message", this.$route.params["id"]);
		}
	})
	export default class ReportDataMessageView extends Vue {
		public get message(): Message {
			return this.$store.state.cbc.entity.message as Message;
		}

		public set message(message: Message) {
			this.$store.dispatch("cbc/update_message", {
				message: message,
				reportDataId: this.$route.params["id"]
			} as ReportDataUpdateMessageRequest);
		}

		@Watch("message", {deep: true})
		public onChanged(value: Message, oldValue: Message) {
			this.message = value;
		}

		public onGenerate() {
			this.$store.dispatch("cbc/get", this.$route.params["id"]).then(() => {
				this.$store.dispatch("cbc/generate", {
					data: this.$store.state.cbc.entity as ReportData
				} as ReportDataGenerateRequest);
			});
		}
	}
</script>
