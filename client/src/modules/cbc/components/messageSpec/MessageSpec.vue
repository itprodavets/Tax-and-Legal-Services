<template>
	<v-card class="message-spec">
		<v-container>
			<v-row>
				<v-col align="center" justify="space-around">
					<v-btn class="ma-2" tile outlined color="success" v-if="readonly" @click="onEdit()">
						<v-icon left>mdi-pencil</v-icon>Edit
					</v-btn>
					<v-btn class="ma-2" tile outlined color="warning" v-if="!readonly" @click="onCancel()">
						<v-icon left>mdi-cancel</v-icon>Cancel
					</v-btn>
					<v-btn class="ma-2" tile outlined color="success" v-if="!readonly" @click="onSave()">
						<v-icon left>mdi-content-save</v-icon>Save
					</v-btn>
				</v-col>
			</v-row>
			<v-row align="center" justify="space-between">
				<v-col cols="12" lg="2" md="2">
					<v-autocomplete
						dense
						filled
						v-model="$v.messageType.$model"
						:items="messageTypes"
						label="Message Type"
						:disabled="true"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="5" md="5">
					<v-text-field
						dense
						filled
						v-model="$v.sendingEntityIN.$model"
						label="Sending Entity IN"
						:disabled="readonly"
						:error-messages="sendingEntityInErrors"
						required
						@input="$v.sendingEntityIN.$touch()"
						@blur="$v.sendingEntityIN.$touch()"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="2" md="2">
					<v-autocomplete
						dense
						filled
						v-model="$v.language.$model"
						:items="languages"
						item-text="name"
						label="Language"
						:disabled="readonly"
						:error-messages="languageErrors"
						return-object
						required
						@input="$v.language.$touch()"
						@blur="$v.language.$touch()"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="3" md="3">
					<v-autocomplete
						dense
						filled
						v-model="$v.transmittingCountry.$model"
						:items="countries"
						item-text="name"
						label="Transmitting Country"
						:disabled="readonly"
						:error-messages="transmittingCountryErrors"
						return-object
						required
						@input="$v.transmittingCountry.$touch()"
						@blur="$v.transmittingCountry.$touch()"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="12" md="12">
					<v-autocomplete
						dense
						filled
						v-model="$v.receivingCountry.$model"
						:items="countries"
						item-text="name"
						label="Receiving Country"
						multiple
						:disabled="readonly"
						:error-messages="receivingCountryErrors"
						return-object
						required
						@input="$v.receivingCountry.$touch()"
						@blur="$v.receivingCountry.$touch()"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="2" md="2">
					<v-autocomplete
						dense
						filled
						v-model="$v.messageTypeIndic.$model"
						:items="messageTypeIndics"
						label="Message Type Indic"
						:disabled="readonly"
					></v-autocomplete>
				</v-col>
				<v-col cols="12" lg="5" md="5">
					<v-text-field
						dense
						filled
						v-model="$v.messageRefId.$model"
						label="Message Ref Id"
						:disabled="true"
					></v-text-field>
				</v-col>
				<v-col cols="12" lg="2" md="2">
					<v-menu
						v-model="reportingPeriodMenu"
						:close-on-content-click="false"
						:nudge-right="40"
						transition="scale-transition"
						offset-y
					>
						<template v-slot:activator="{ on }">
							<v-text-field
								dense
								filled
								v-model="$v.reportingPeriod.$model"
								label="Reporting Period"
								prepend-icon="event"
								:disabled="readonly"
								:error-messages="reportingPeriodErrors"
								v-on="on"
								required
								@input="$v.reportingPeriod.$touch()"
								@blur="$v.reportingPeriod.$touch()"
							></v-text-field>
						</template>
						<v-date-picker v-model="$v.reportingPeriod.$model" @input="reportingPeriodMenu = false"></v-date-picker>
					</v-menu>
				</v-col>
				<v-col cols="12" lg="3" md="3">
					<v-menu
						v-model="timestampMenu"
						:close-on-content-click="false"
						:nudge-right="40"
						transition="scale-transition"
						offset-y
					>
						<template v-slot:activator="{ on }">
							<v-text-field
								dense
								filled
								:value="computedTimestamp"
								label="Timestamp"
								prepend-icon="event"
								:disabled="readonly"
								v-on="on"
							></v-text-field>
						</template>
						<v-date-picker v-model="$v.timestamp.$model" @input="timestampMenu = false"></v-date-picker>
					</v-menu>
				</v-col>
				<v-col cols="12" lg="6" md="6">
					<v-textarea
						dense
						filled
						v-model="$v.warning.$model"
						label="Warning"
						rows="10"
						:disabled="readonly"
						:error-messages="warningErrors"
						@input="$v.warning.$touch()"
						@blur="$v.warning.$touch()"
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
						:error-messages="contactErrors"
						@input="$v.contact.$touch()"
						@blur="$v.contact.$touch()"
					></v-textarea>
				</v-col>
				<v-col cols="12" lg="5" md="5" v-if="isUsedField">
					<v-text-field
						dense
						filled
						v-model="$v.corrMessageRefId.$model"
						label="Corr Message Ref Id"
						:disabled="readonly"
					></v-text-field>
				</v-col>
			</v-row>
		</v-container>
	</v-card>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { Core } from "@/core/mixins/core";
import moment from "moment";
import { required, maxLength } from "vuelidate/lib/validators";
import {
  MessageType_EnumType,
  CbcMessageTypeIndic_EnumType
} from "@/modules/cbc/models";
import { Language } from "@/modules/language/models/dto.model";
import { Country } from "@/modules/country/models/dto.model";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {
    sendingEntityIN: {
      required
    },
    transmittingCountry: {
      required
    },
    receivingCountry: {
      required
    },
    messageType: {},
    language: {
      required
    },
    warning: {
      maxLength: maxLength(4000)
    },
    contact: {
      maxLength: maxLength(4000)
    },
    messageRefId: {},
    messageTypeIndic: {},
    corrMessageRefId: {},
    reportingPeriod: {
      required
    },
    timestamp: {}
  }
})
export default class MessageSpecComponent extends Vue {
  @Prop()
  public languages!: Language[];

  @Prop()
  public countries!: Country[];

  @Watch("transmittingCountry")
  onTransmittingCountryChanged(value: Country, oldValue: Country) {
    if ((this.$data.receivingCountry as Country[]).indexOf(value) === -1) {
      this.$data.receivingCountry.push(value);
    }
    this.$data.receivingCountry = this.$data.receivingCountry
      .filter((x: Country) => x !== oldValue)
      .sort((x: Country) => x.alpha2Code);
    this.onSetMessageRefId();
  }

  @Watch("receivingCountry")
  onReceivingCountryChanged(values: Country[], oldValues: Country[]) {
    this.onSetMessageRefId();
  }

  @Watch("reportingPeriod")
  onReportingPeriodChanged(value: string, oldValue: string) {
    this.$data.timestamp = moment().toISOString();
    this.onSetMessageRefId();
  }

  @Watch("timestamp")
  onTimestampChanged(value: string, oldValue: string) {
    this.onSetMessageRefId();
  }

  private onSetMessageRefId() {
    this.$data.messageRefId =
      (this.$data.transmittingCountry
        ? this.$data.transmittingCountry.alpha2Code
        : "") +
      (this.$data.reportingPeriod
        ? moment(this.$data.reportingPeriod).year()
        : "") +
      this.onGetReceivingCountryCode(this.$data.receivingCountry) +
      this.onGetDateTimeToString() +
      Math.floor(Math.random() * (999999999 - 0 + 1) + 0);
  }
  private onGetReceivingCountryCode(receivingCountry: Array<Country>): string {
    if (!receivingCountry) return "";
    if (receivingCountry.length === 0) return "";
    return receivingCountry.map(x => x.alpha2Code).join("");
  }
  private onGetDateTimeToString(): string {
    var date = moment(new Date());
    return `${date.year()}${date.month()}${date.day()}T${date.hour()}${date.minute()}${date.second()}`;
  }

  public readonly: boolean = true;
  public isUsedField: boolean = false;

  public messageTypes = Object.values(MessageType_EnumType).filter(
    value => typeof value === "string"
  );

  public messageTypeIndics = Object.values(CbcMessageTypeIndic_EnumType).filter(
    value => typeof value === "string"
  );

  public get sendingEntityInErrors() {
    const errors: string[] = [];
    if (this.$v.sendingEntityIN) {
      if (!this.$v.sendingEntityIN.$dirty) return errors;
      if (!this.$v.sendingEntityIN.required) {
        errors.push("The Sending Entity In is required");
      }
    }
    return errors;
  }
  public get transmittingCountryErrors() {
    const errors: string[] = [];
    if (this.$v.transmittingCountry) {
      if (!this.$v.transmittingCountry.$dirty) return errors;
      if (!this.$v.transmittingCountry.required) {
        errors.push("The Transmitting Country is required");
      }
    }
    return errors;
  }
  public get receivingCountryErrors() {
    const errors: string[] = [];
    if (this.$v.receivingCountry) {
      if (!this.$v.receivingCountry.$dirty) return errors;
      if (!this.$v.receivingCountry.required) {
        errors.push("The Receiving Country is required");
      }
    }
    return errors;
  }
  public get languageErrors() {
    const errors: string[] = [];
    if (this.$v.language) {
      if (!this.$v.language.$dirty) return errors;
      if (!this.$v.language.required) {
        errors.push("The Language is required");
      }
    }
    return errors;
  }
  public get warningErrors() {
    const errors: string[] = [];
    if (this.$v.warning) {
      if (!this.$v.warning.$dirty) return errors;
      if (!this.$v.warning.maxLength) {
        errors.push("The maximum of 4’000 characters are allowed");
      }
    }
    return errors;
  }

  public get contactErrors() {
    const errors: string[] = [];
    if (this.$v.contact) {
      if (!this.$v.contact.$dirty) return errors;
      if (!this.$v.contact.maxLength) {
        errors.push("The maximum of 4’000 characters are allowed");
      }
    }
    return errors;
  }
  public get reportingPeriodErrors() {
    const errors: string[] = [];
    if (this.$v.reportingPeriod) {
      if (!this.$v.reportingPeriod.$dirty) return errors;
      if (!this.$v.reportingPeriod.required) {
        errors.push("The Reporting Period is required");
      }
    }
    return errors;
  }

  public onEdit() {
    this.readonly = false;
  }
  public onCancel() {
    this.readonly = true;
  }
  public onSave() {
    if (this.$v.$invalid) {
      this.$v.$touch();
      return;
    } else {
      this.$emit("messageSpec", {
        sendingEntityIN: this.$v.sendingEntityIN.$model,
        transmittingCountry: this.$v.transmittingCountry.$model,
        receivingCountry: this.$v.receivingCountry.$model,
        messageType: this.$v.messageType.$model,
        language: this.$v.language.$model,
        warning: this.$v.warning.$model,
        contact: this.$v.contact.$model,
        messageRefId: this.$v.messageRefId.$model,
        messageTypeIndic: this.$v.messageTypeIndic.$model,
        corrMessageRefId: this.$v.corrMessageRefId.$model,
        reportingPeriod: this.$v.reportingPeriod.$model,
        timestamp: this.$v.timestamp.$model
      });
    }
  }

  public data() {
    return {
      sendingEntityIN: "" as string,
      transmittingCountry: {} as Country,
      receivingCountry: [] as Country[],
      messageType: MessageType_EnumType[MessageType_EnumType.CBC],
      language: {} as Language,
      warning: "" as string,
      contact: "" as string,
      messageRefId: "" as string,
      messageTypeIndic:
        CbcMessageTypeIndic_EnumType[CbcMessageTypeIndic_EnumType.CBC401],
      corrMessageRefId: "" as string,
      reportingPeriodMenu: false,
      reportingPeriod: moment().format("YYYY-MM-DD"),
      timestampMenu: false,
      timestamp: moment()
    };
  }
  get computedTimestamp() {
    return moment(this.$v.timestamp.$model).toISOString();
  }
}
</script>
<style lang="scss" scoped>
.message-spec {
	width: 100%;
	margin-bottom: 10px;
}
</style>
