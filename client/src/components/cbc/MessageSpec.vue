<template>
	<v-card class="message-spec">
		<v-container>
			<v-row>
				<v-col align="center" justify="space-around">
					<v-btn class="ma-2" tile outlined color="success" v-if="readonly" @click="onEdit()">
						<v-icon left>mdi-pencil</v-icon>Edit
					</v-btn>
					<v-btn class="ma-2" tile outlined color="warning" v-if="!readonly" @click="onEdit()">
						<v-icon left>mdi-cancel</v-icon>Cancel
					</v-btn>
					<v-btn class="ma-2" tile outlined color="success" v-if="!readonly" @click="onEdit()">
						<v-icon left>mdi-content-save</v-icon>Save
					</v-btn>
				</v-col>
			</v-row>
			<v-row align="center" justify="space-between">
				<v-col cols="12" lg="1" md="2">
					<v-autocomplete
						dense
						filled
						v-model="$v.messageType.$model"
						:items="messageTypes"
						label="Message Type"
						:disabled="true"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="4" md="6">
					<v-text-field
						dense
						filled
						v-model="$v.sendingEntityIN.$model"
						label="Sending Entity IN"
						:disabled="readonly"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="2" md="4">
					<v-autocomplete
						dense
						filled
						v-model="$v.language.$model"
						:items="[]"
						label="Language"
						:disabled="readonly"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="2" md="4">
					<v-autocomplete
						dense
						filled
						v-model="$v.transmittingCountry.$model"
						:items="[]"
						label="Transmitting Country"
						:disabled="readonly"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="3" md="8">
					<v-autocomplete
						dense
						filled
						v-model="$v.receivingCountry.$model"
						:items="[]"
						chips
						small-chips
						label="Receiving Country"
						multiple
						:disabled="readonly"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="6" md="6">
					<v-textarea
						dense
						filled
						v-model="$v.warning.$model"
						label="Warning"
						rows="10"
						:disabled="readonly"
					></v-textarea>
				</v-col>
				<v-col cols="12" lg="6" md="6">
					<v-textarea
						dense
						filled
						v-model="$v.contact.$model"
						label="Contact"
						rows="10"
						:disabled="readonly"
					></v-textarea>
				</v-col>
				<v-col cols="12" lg="2" md="4">
					<v-text-field
						dense
						filled
						v-model="$v.messageRefId.$model"
						label="Message Ref Id"
						:disabled="readonly"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="2" md="4">
					<v-text-field
						dense
						filled
						v-model="$v.messageTypeIndic.$model"
						label="Message Type Indic"
						:disabled="readonly"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="2" md="4">
					<v-text-field
						dense
						filled
						v-model="$v.corrMessageRefId.$model"
						label="Corr Message Ref Id"
						:disabled="readonly"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="3" md="4">
					<v-text-field
						dense
						filled
						v-model="$v.reportingPeriod.$model"
						label="Reporting Period"
						:disabled="readonly"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="3" md="4">
					<v-text-field
						dense
						filled
						v-model="$v.timestamp.$model"
						label="Timestamp"
						:disabled="readonly"
					></v-text-field>
				</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { MessageType_EnumType } from "@/models/enums.model";
import { Core } from "@/core/mixins/core";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {
    sendingEntityIN: {},
    transmittingCountry: {},
    receivingCountry: {},
    messageType: {},
    language: {},
    warning: {},
    contact: {},
    messageRefId: {},
    messageTypeIndic: {},
    corrMessageRefId: {},
    reportingPeriod: {},
    timestamp: {}
  }
})
export default class MessageSpecComponent extends Vue {
  public readonly: boolean = true;

  public messageTypes = Object.values(MessageType_EnumType).filter(
    value => typeof value === "string"
  );

  public onEdit() {
    this.readonly = !this.readonly;
  }
  public data() {
    return {
      messageType: MessageType_EnumType[MessageType_EnumType.CBC]
    };
  }
}
</script>
<style lang="scss" scoped>
.message-spec {
	width: 100%;
	margin-bottom: 10px;
}
</style>
