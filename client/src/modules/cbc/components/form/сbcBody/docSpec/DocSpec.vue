<template>
    <v-container class="pa-0">
        <v-row>
            <v-col cols="12" lg="2" md="2">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.doc.type.$model"
                        :items="docTypes"
                        label="Doc Type"
                        item-text="name"
                        :disabled="readonly"
                        :error-messages="docTypeIndicErrors"
                        required
                        @input="$v.doc.type.$touch()"
                        @blur="$v.doc.type.$touch()"
                ></v-autocomplete>
            </v-col>
            <v-col cols="12" lg="4" md="4">
                <v-text-field
                        dense
                        filled
                        v-model="$v.doc.refId.$model"
                        label="Doc Ref Id"
                        :disabled="true"
                        :error-messages="docRefIdErrors"
                        required
                        @input="$v.doc.refId.$touch()"
                        @blur="$v.doc.refId.$touch()"
                ></v-text-field>
            </v-col>
            <v-col cols="12" lg="3" md="3">
                <v-text-field
                        dense
                        filled
                        v-model="$v.doc.corrMessageRefId.$model"
                        label="Corr Message Ref Id"
                        :disabled="true"
                        :error-messages="corrMessageRefIdErrors"
                        @input="$v.doc.corrMessageRefId.$touch()"
                        @blur="$v.doc.corrMessageRefId.$touch()"
                ></v-text-field>
            </v-col>
            <v-col cols="12" lg="3" md="3">
                <v-text-field
                        dense
                        filled
                        v-model="$v.doc.corrDocRefId.$model"
                        label="Corr Doc Ref Id"
                        :disabled="true"
                        :error-messages="corrDocRefIdErrors"
                        @input="$v.doc.corrDocRefId.$touch()"
                        @blur="$v.doc.corrDocRefId.$touch()"
                ></v-text-field>
            </v-col>
        </v-row>
    </v-container>
</template>
<script lang="ts">
	import {Component, Mixins, Prop, Watch} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Doc, DocTypeEnum} from "@/modules/cbc/models";

	@Component({
		components: {},
		mixins: [validationMixin],
		validations: {
			doc: {
				type: {
					required
				},
				refId: {
					required,
					maxLength: maxLength(200)
				},
				corrMessageRefId: {
					maxLength: maxLength(200)
				},
				corrDocRefId: {
					maxLength: maxLength(170)
				}
			}
		}
	})
	export default class DocSpecComponent extends Mixins(CbcMixin) {
		@Prop()
		public readonly!: boolean;

		@Prop()
		public value!: Doc;

		@Watch("doc", {deep: true})
		onDocChanged(value: Doc, oldValue: Doc) {
			this.onSave();
		}

		@Watch("value", {deep: true})
		onValueChanged(value: Doc, oldValue: Doc) {
			(this.$data.doc as Doc).refId = value.refId;
		}

		public data() {
			return {
				doc: {
					type: {} as DocTypeEnum,
					refId: "" as string,
					corrMessageRefId: "" as string,
					corrDocRefId: "" as string
				} as Doc
			};
		}

		public onSave() {
			if (this.$v.doc.$invalid) {
				this.$v.$touch();
				return;
			} else {
				const data = {
					type: this.$data.doc.type.id,
					refId: this.$data.doc.refId,
					corrMessageRefId: this.$data.doc.corrMessageRefId,
					corrDocRefId: this.$data.doc.corrDocRefId
				} as Doc;
				this.$emit("input", data);
			}
		}

		public get docTypeIndicErrors() {
			const errors: string[] = [];
			if (this.$v.doc.type) {
				if (!this.$v.doc.type.$dirty) return errors;
				if (!this.$v.doc.type.required) {
					errors.push("The Doc Type Indic is required");
				}
			}
			return errors;
		}

		public get docRefIdErrors() {
			const errors: string[] = [];
			if (this.$v.doc.refId) {
				if (!this.$v.doc.refId.$dirty) return errors;
				if (!this.$v.doc.refId.required) {
					errors.push("The Doc Ref Id is required");
				}
				if (!this.$v.doc.refId.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}

		public get corrMessageRefIdErrors() {
			const errors: string[] = [];
			if (this.$v.doc.corrMessageRefId) {
				if (!this.$v.doc.corrMessageRefId.$dirty) return errors;
				if (!this.$v.doc.corrMessageRefId.maxLength) {
					errors.push("The maximum of 200 characters are allowed");
				}
			}
			return errors;
		}

		public get corrDocRefIdErrors() {
			const errors: string[] = [];
			if (this.$v.doc.corrDocRefId) {
				if (!this.$v.doc.corrDocRefId.$dirty) return errors;
				if (!this.$v.doc.corrDocRefId.maxLength) {
					errors.push("The maximum of 170 characters are allowed");
				}
			}
			return errors;
		}
	}
</script>
