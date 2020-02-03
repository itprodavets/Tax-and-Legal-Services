<template>
    <v-container fluid class="pa-0">
        <v-row align="center" justify="space-between">
            <v-col cols="12" lg="2" md="2">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.message.type.$model"
                        item-text="name"
                        :items="messageTypes"
                        label="Message Type"
                        :disabled="readonly"
                        return-object
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="5" md="5">
                <v-text-field
                        dense
                        filled
                        v-model="$v.message.sendingEntityIn.$model"
                        label="Sending Entity IN"
                        :disabled="readonly"
                        :error-messages="sendingEntityInErrors"
                        required
                        @input="$v.message.sendingEntityIn.$touch()"
                        @blur="$v.message.sendingEntityIn.$touch()"
                ></v-text-field>
            </v-col>
            <v-col cols="12" lg="2" md="2">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.message.language.$model"
                        :items="languages"
                        item-text="name"
                        label="Language"
                        :disabled="readonly"
                        :error-messages="languageErrors"
                        return-object
                        required
                        @input="$v.message.language.$touch()"
                        @blur="$v.message.language.$touch()"
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="3" md="3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.message.jurisdiction.$model"
                        :items="countries"
                        item-text="name"
                        label="Jurisdiction"
                        :disabled="readonly"
                        :error-messages="jurisdictionErrors"
                        return-object
                        required
                        @input="$v.message.jurisdiction.$touch()"
                        @blur="$v.message.jurisdiction.$touch()"
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="12" md="12">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.message.jurisdictions.$model"
                        :items="countries"
                        item-text="name"
                        label="Jurisdictions"
                        multiple
                        :disabled="readonly"
                        :error-messages="jurisdictionsErrors"
                        return-object
                        required
                        @input="$v.message.jurisdictions.$touch()"
                        @blur="$v.message.jurisdictions.$touch()"
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="2" md="2">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.message.typeIndic.$model"
                        item-text="name"
                        :items="messageTypeIndics"
                        label="Message Type Indic"
                        :disabled="readonly"
                        return-object
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="5" md="5">
                <v-text-field
                        dense
                        filled
                        v-model="$v.message.refId.$model"
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
                                v-model="$v.message.reportingPeriod.$model"
                                label="Reporting Period"
                                :disabled="readonly"
                                :error-messages="reportingPeriodErrors"
                                v-on="on"
                                required
                                @input="$v.message.reportingPeriod.$touch()"
                                @blur="$v.message.reportingPeriod.$touch()"
                        ></v-text-field>
                    </template>
                    <v-date-picker
                            v-model="$v.message.reportingPeriod.$model"
                            @input="reportingPeriodMenu = false"
                    ></v-date-picker>
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
                                :disabled="readonly"
                                v-on="on"
                        ></v-text-field>
                    </template>
                    <v-date-picker v-model="$v.message.timestamp.$model" @input="timestampMenu = false"></v-date-picker>
                </v-menu>
            </v-col>
            <v-col cols="12" lg="6" md="6">
                <v-textarea
                        dense
                        filled
                        v-model="$v.message.warning.$model"
                        label="Warning"
                        rows="10"
                        :disabled="readonly"
                        :error-messages="warningErrors"
                        @input="$v.message.warning.$touch()"
                        @blur="$v.message.warning.$touch()"
                ></v-textarea>
            </v-col>
            <v-col cols="12" lg="6" md="6">
                <v-textarea
                        dense
                        filled
                        v-model="$v.message.contact.$model"
                        label="Contact"
                        rows="10"
                        :disabled="readonly"
                        :error-messages="contactErrors"
                        @input="$v.message.contact.$touch()"
                        @blur="$v.message.contact.$touch()"
                ></v-textarea>
            </v-col>
            <v-col cols="12" lg="5" md="5">
                <v-text-field
                        dense
                        filled
                        v-model="$v.message.corrMessageRefId.$model"
                        label="Corr Message Ref Id"
                        :disabled="readonly"
                ></v-text-field>
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
    import {CountryEnum} from "@/modules/country/models";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import moment from "moment";
	import {maxLength, required} from "vuelidate/lib/validators";
	import {Language} from "@/modules/language/models/dto.model";
	import {Country} from "@/modules/country/models/dto.model";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {LanguageEnum, Message, MessageTypeEnum, MessageTypeIndicEnum} from "@/modules/cbc/models";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			message: {
				sendingEntityIn: {
					required
				},
				jurisdiction: {
					required
				},
				jurisdictions: {
					required
				},
				type: {},
				language: {
					required
				},
				warning: {
					maxLength: maxLength(4000)
				},
				contact: {
					maxLength: maxLength(4000)
				},
				refId: {},
				typeIndic: {},
				corrMessageRefId: {},
				reportingPeriod: {
					required
				},
				timestamp: {}
			}
		}
	})
	export default class MessageSpecComponent extends Mixins(CbcMixin, CountryEnumMixin) {
		@Prop()
		public languages!: Language[];

		@Prop()
		public readonly countries!: Country[];

		@Prop()
		public readonly readonly!: boolean;

		@Watch("message", {deep: true})
		onMessageChanged(value: Message, oldValue: Message) {
			this.$data.message.refId = this.onGenerateMessageRefId(
				this.getCountryEnums(this.$data.message.jurisdiction),
				this.onGetYear(value.reportingPeriod),
				this.getCountryEnums(this.$data.message.jurisdictions)
			);
		}

		public onSave() {
			if (this.$v.$invalid) {
				this.$v.$touch();
				return;
			} else {
				// this.$emit("input", {
				//   sendingEntityIn: this.$data.message.sendingEntityIN,
				//   jurisdiction: this.onGetCountryEnum(this.$data.message.jurisdiction),
				//   jurisdictions: this.onGetCountryEnum(this.$data.message.jurisdictions),
				//   type: this.$data.message.messageType,
				//   language: this.$data.message.language,
				//   warning: this.$data.message.warning,
				//   contact: this.$data.message.contact,
				//   refId: this.$data.message.messageRefId,
				//   typeIndic: this.$data.message.messageTypeIndic,
				//   corrMessageRefId: this.$data.message.corrMessageRefId,
				//   reportingPeriod: this.$data.message.reportingPeriod,
				//   timestamp: this.$data.message.timestamp
				// } as Message);
			}
		}

		public data() {
			return {
				reportingPeriodMenu: false,
				timestampMenu: false,
				message: {
					sendingEntityIn: "",
					jurisdiction: {} as CountryEnum,
					jurisdictions: [] as CountryEnum[],
					type: {} as MessageTypeEnum,
					language: {} as LanguageEnum,
					warning: "",
					contact: "",
					refId: "",
					typeIndic: {} as MessageTypeIndicEnum,
					corrMessageRefId: [] as string[],
					reportingPeriod: moment()
						.format("YYYY-MM-DD")
						.toString(),
					timestamp: moment().toString()
				} as Message
			};
		}

		get computedTimestamp() {
			return moment(this.$data.timestamp).toISOString();
		}

		public get sendingEntityInErrors() {
			const errors: string[] = [];
			if (this.$v.message.sendingEntityIn) {
				if (!this.$v.message.sendingEntityIn.$dirty) return errors;
				if (!this.$v.message.sendingEntityIn.required) {
					errors.push("The Sending Entity In is required");
				}
			}
			return errors;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (this.$v.message.jurisdiction) {
				if (!this.$v.message.jurisdiction.$dirty) return errors;
				if (!this.$v.message.jurisdiction.required) {
					errors.push("The Transmitting Country is required");
				}
			}
			return errors;
		}

		public get jurisdictionsErrors() {
			const errors: string[] = [];
			if (this.$v.message.jurisdictions) {
				if (!this.$v.message.jurisdictions.$dirty) return errors;
				if (!this.$v.message.jurisdictions.required) {
					errors.push("The Receiving Country is required");
				}
			}
			return errors;
		}

		public get languageErrors() {
			const errors: string[] = [];
			if (this.$v.message.language) {
				if (!this.$v.message.language.$dirty) return errors;
				if (!this.$v.message.language.required) {
					errors.push("The Language is required");
				}
			}
			return errors;
		}

		public get warningErrors() {
			const errors: string[] = [];
			if (this.$v.message.warning) {
				if (!this.$v.message.warning.$dirty) return errors;
				if (!this.$v.message.warning.maxLength) {
					errors.push("The maximum of 4’000 characters are allowed");
				}
			}
			return errors;
		}

		public get contactErrors() {
			const errors: string[] = [];
			if (this.$v.message.contact) {
				if (!this.$v.message.contact.$dirty) return errors;
				if (!this.$v.message.contact.maxLength) {
					errors.push("The maximum of 4’000 characters are allowed");
				}
			}
			return errors;
		}

		public get reportingPeriodErrors() {
			const errors: string[] = [];
			if (this.$v.message.reportingPeriod) {
				if (!this.$v.message.reportingPeriod.$dirty) return errors;
				if (!this.$v.message.reportingPeriod.required) {
					errors.push("The Reporting Period is required");
				}
			}
			return errors;
		}
	}
</script>
