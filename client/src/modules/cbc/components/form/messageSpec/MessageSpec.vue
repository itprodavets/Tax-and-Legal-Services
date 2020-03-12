<template>
	<v-container fluid class="pa-0">
		<v-row align="center" justify="space-between">
			<v-col cols="12" lg="2" md="2">
				<v-autocomplete
						dense
						filled
						v-model="type"
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
						v-model="message.sendingEntityIn"
						label="Sending Entity IN"
						:disabled="readonly"
						:error-messages="sendingEntityInErrors"
						@input="$v.message.sendingEntityIn.$touch()"
						@blur="$v.message.sendingEntityIn.$touch()"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="2" md="2">
				<v-autocomplete
						dense
						filled
						v-model="language"
						:items="languages"
						item-text="name"
						label="Language"
						:disabled="readonly"
						:error-messages="languageErrors"
						return-object
						@input="$v.message.language.$touch()"
						@blur="$v.message.language.$touch()"
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="3" md="3">
				<v-autocomplete
						dense
						filled
						v-model="jurisdiction"
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
						v-model="jurisdictions"
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
						v-model="typeIndic"
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
						v-model="refId"
						label="Message Ref Id"
						:disabled="readonly"
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
								v-model="reportingPeriodText"
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
							v-model="reportingPeriod"
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
								:value="timestampText"
								label="Timestamp"
								:disabled="readonly"
								v-on="on"
						></v-text-field>
					</template>
					<v-date-picker v-model="timestamp"
					               @input="timestampMenu = false"></v-date-picker>
				</v-menu>
			</v-col>
			<v-col cols="12" lg="6" md="6">
				<v-textarea
						dense
						filled
						v-model="message.warning"
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
						v-model="message.contact"
						label="Contact"
						rows="10"
						:disabled="readonly"
						:error-messages="contactErrors"
						@input="$v.message.contact.$touch()"
						@blur="$v.message.contact.$touch()"
				></v-textarea>
			</v-col>
			<v-col cols="12">
				<v-combobox
						dense
						filled
						v-model="message.corrMessageRefId"
						:items="$v.message.corrMessageRefId.$model"
						label="Corr Message Ref Id"
						multiple
						:disabled="readonly"
				></v-combobox>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {ReferenceBook} from "@/core/models";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Message, MessageTypeEnum, MessageTypeIndicEnum} from "@/modules/cbc/models";
	import {CountryEnumMixin} from "@/modules/country/mixins/country-enum";
	import {CountryEnum} from "@/modules/country/models";
	import {Country} from "@/modules/country/models/dto.model";
	import {LanguageEnumMixin} from "@/modules/language/mixins";
	import {LanguageEnum} from "@/modules/language/models";
	import {Language} from "@/modules/language/models/dto.model";
	import _ from "lodash";
	import moment from "moment";
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, minLength, required} from "vuelidate/lib/validators";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			message: {
				sendingEntityIn: {
					minLength: minLength(1),
					maxLength: maxLength(200)
				},
				jurisdiction: {
					required
				},
				jurisdictions: {
					required
				},
				type: {},
				language: {},
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
	export default class MessageSpecComponent extends Mixins(CbcMixin, CountryEnumMixin, LanguageEnumMixin) {
		@Prop()
		public readonly readonly!: boolean;

		@Prop()
		public languages!: Language[];

		@Prop()
		public readonly countries!: Country[];

		@Prop({
			type: Object, default: () => {
				return {
					sendingEntityIn: "",
					jurisdiction: {} as CountryEnum,
					jurisdictions: [] as CountryEnum[],
					type: MessageTypeEnum.CBC,
					language: undefined,
					warning: "",
					contact: "",
					refId: "",
					typeIndic: undefined,
					corrMessageRefId: [] as string[],
					reportingPeriod: "",
					timestamp: ""
				} as Message;
			}
		})
		public readonly message!: Message;

		public get value(): Message {
			return this.message;
		}

		public set value(message: Message) {
			if (this.$v!.message!.$invalid) {
				this.$v!.message!.$touch();
			} else
				this.$emit("update:message", message);
		}

		@Watch("message", {deep: true})
		public onChanged(value: Message, oldValue: Message) {
			this.value = value;
		}

		@Watch("jurisdiction", {deep: true})
		public onJurisdictionChanged(value: Country, oldValue: Country) {
			if (value !== oldValue) this.refId = this.generateRefId();
		}

		@Watch("jurisdictions", {deep: true})
		public onJurisdictionsChanged(values: Country[], oldValues: Country[]) {
			if (values.some(x => !oldValues.some(y => y.alpha2Code === x.alpha2Code)) || oldValues.some(x => !values.some(y => y.alpha2Code === x.alpha2Code)))
				this.refId = this.generateRefId();
		}

		@Watch("reportingPeriod", {deep: true})
		public onReportingPeriodChanged(value: string, oldValue: string) {
			if (value !== oldValue) this.refId = this.generateRefId();
		}

		public reportingPeriodMenu: boolean = false;
		public timestampMenu: boolean = false;

		public get jurisdiction(): Country | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdiction)) return this.countries.find(x => x.alpha2Code === CountryEnum[this.value.jurisdiction])!;
		}

		public set jurisdiction(country: Country | undefined) {
			if (this.value && country) this.save({
				jurisdiction: this.getCountryEnum(country)
			});
		}

		public get jurisdictions(): Country[] | undefined {
			if (this.value && !_.isUndefined(this.value.jurisdictions)) return this.countries.filter(x => this.value.jurisdictions.find(y => CountryEnum[y] === x.alpha2Code))!;
		}

		public set jurisdictions(countries: Country[] | undefined) {
			if (this.value && countries && countries.length > 0) this.save({
				jurisdictions: this.getCountryEnums(countries)
			});
		}

		public get language(): Language | undefined {
			if (this.value && !_.isUndefined(this.value.language)) {
				const languageEnum = LanguageEnum[this.value.language];
				return this.languages.find(x => x.code === languageEnum);
			}
		}

		public set language(language: Language | undefined) {
			if (this.value && language) this.save({language: this.getLanguageEnum(language)});
		}

		public get type(): ReferenceBook<MessageTypeEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.type))
				return this.messageTypes.find(x => x.id === this.value.type)!;
		}

		public set type(type: ReferenceBook<MessageTypeEnum> | undefined) {
			if (this.value && type) this.save({type: type.id});
		}

		public get typeIndic(): ReferenceBook<MessageTypeIndicEnum> | undefined {
			if (this.value && !_.isUndefined(this.value.typeIndic)) return this.messageTypeIndics.find(x => x.id === this.value.typeIndic)!;
		}

		public set typeIndic(typeIndic: ReferenceBook<MessageTypeIndicEnum> | undefined) {
			if (this.value && typeIndic) this.save({typeIndic: typeIndic.id});
		}

		public get refId(): string | undefined {
			if (this.value) return this.value.refId;
		}

		public set refId(refId: string | undefined) {
			if (this.value && refId) this.save({refId: refId});
		}

		public get reportingPeriod(): string | undefined {
			if (this.value && this.value.reportingPeriod) {
				this.reportingPeriodText = moment(this.value.reportingPeriod).format("YYYY-MM-DD");
				return this.reportingPeriodText;
			}
		}

		public reportingPeriodText: string = "";

		public set reportingPeriod(reportingPeriod: string | undefined) {
			if (this.value && reportingPeriod) {
				this.save({reportingPeriod: reportingPeriod});
				this.reportingPeriodText = reportingPeriod;
			}
		}

		public get timestamp(): string | undefined {
			if (this.value && this.value.timestamp) {
				this.timestampText = moment(this.value.timestamp).toISOString();
				return this.timestampText;
			}
		}

		public timestampText: string = "";

		public set timestamp(timestamp: string | undefined) {
			if (this.value && timestamp) {
				this.save({timestamp: timestamp});
				this.timestampText = timestamp;
			}
		}

		public get sendingEntityInErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.sendingEntityIn!.$dirty) return errors;
			!this.$v!.message!.sendingEntityIn!.minLength && errors.push("The minimum of 1 character are allowed");
			!this.$v!.message!.sendingEntityIn!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		public get jurisdictionErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.jurisdiction!.$dirty) return errors;
			!this.$v!.message!.jurisdiction!.required && errors.push("The Transmitting Country is required");
			return errors;
		}

		public get jurisdictionsErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.jurisdictions!.$dirty) return errors;
			!this.$v!.message!.jurisdictions!.required && errors.push("The Receiving Country is required");
			return errors;
		}

		public get languageErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.language!.$dirty) return errors;
			return errors;
		}

		public get warningErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.warning!.$dirty) return errors;
			!this.$v!.message!.warning!.maxLength && errors.push("The maximum of 4’000 characters are allowed");
			return errors;
		}

		public get contactErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.contact!.$dirty) return errors;
			!this.$v!.message!.contact!.maxLength && errors.push("The maximum of 4’000 characters are allowed");
			return errors;
		}

		public get reportingPeriodErrors() {
			const errors: string[] = [];
			if (!this.$v!.message!.reportingPeriod!.$dirty) return errors;
			!this.$v!.message!.reportingPeriod!.required && errors.push("The Reporting Period is required");
			return errors;
		}

		private save(message: Message | any) {
			this.value = Object.assign(this.value, message);
		}

		private generateRefId(): string {
			return this.onGenerateRefId(
				this.jurisdiction ? this.jurisdiction.alpha2Code : "",
				new Date(this.reportingPeriod!).getFullYear(),
				this.jurisdictions ? this.jurisdictions.map(x => x.alpha2Code).join("") : "",
				new Date());
		}
	}
</script>
