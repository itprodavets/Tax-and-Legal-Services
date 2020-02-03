<template>
	<v-container fluid class="pa-0">
		<v-row>
			<v-col cols="12" lg="2" md="2">
				<v-autocomplete
						dense
						filled
						v-model="type"
						:items="docTypes"
						label="Doc Type"
						item-text="name"
						:disabled="readonly"
						:error-messages="docTypeIndicErrors"
						required
						@input="$v.doc.type.$touch()"
						@blur="$v.doc.type.$touch()"
						return-object
				></v-autocomplete>
			</v-col>
			<v-col cols="12" lg="4" md="4">
				<v-text-field
						dense
						filled
						v-model="doc.refId"
						label="Doc Ref Id"
						:disabled="readonly"
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
						v-model="doc.corrMessageRefId"
						label="Corr Message Ref Id"
						:disabled="readonly"
						:error-messages="corrMessageRefIdErrors"
						@input="$v.doc.corrMessageRefId.$touch()"
						@blur="$v.doc.corrMessageRefId.$touch()"
				></v-text-field>
			</v-col>
			<v-col cols="12" lg="3" md="3">
				<v-text-field
						dense
						filled
						v-model="doc.corrDocRefId"
						label="Corr Doc Ref Id"
						:disabled="readonly"
						:error-messages="corrDocRefIdErrors"
						@input="$v.doc.corrDocRefId.$touch()"
						@blur="$v.doc.corrDocRefId.$touch()"
				></v-text-field>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {ReferenceBook} from "@/core/models";
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {Doc, DocTypeEnum} from "@/modules/cbc/models";
	import {Component, Mixins, Prop, PropSync} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";
	import {maxLength, required} from "vuelidate/lib/validators";

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
		@Prop({default: false})
		public readonly readonly!: boolean;

		@PropSync('doc', {
			type: Object, default: () => {
				return {
					type: {},
					refId: "",
					corrMessageRefId: "",
					corrDocRefId: ""
				} as Doc;
			}
		})
		public value!: Doc;

		public get type(): ReferenceBook<DocTypeEnum> | undefined {
			if (this.value && this.value.type)
				return this.docTypes.find(x => x.id === this.value.type)!;
		}

		public set type(type: ReferenceBook<DocTypeEnum> | undefined) {
			if (type)
				this.value = {
					type: type.id,
					refId: this.value.refId,
					corrDocRefId: this.value.corrDocRefId,
					corrMessageRefId: this.value.corrMessageRefId,
				} as Doc;
		}

		public get docTypeIndicErrors() {
			const errors: string[] = [];
			if (!this.$v!.doc!.type!.$dirty) return errors;
			!this.$v!.doc!.type!.required && errors.push("The Doc Type Indic is required");
			return errors;
		}

		public get docRefIdErrors() {
			const errors: string[] = [];
			if (!this.$v!.doc!.refId!.$dirty) return errors;
			!this.$v!.doc!.refId!.required && errors.push("The Doc Ref Id is required");
			!this.$v!.doc!.refId!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		public get corrMessageRefIdErrors() {
			const errors: string[] = [];
			if (!this.$v!.doc!.corrMessageRefId!.$dirty) return errors;
			!this.$v!.doc!.corrMessageRefId!.maxLength && errors.push("The maximum of 200 characters are allowed");
			return errors;
		}

		public get corrDocRefIdErrors() {
			const errors: string[] = [];
			if (!this.$v!.doc!.corrDocRefId!.$dirty) return errors;
			!this.$v!.doc!.corrDocRefId!.maxLength && errors.push("The maximum of 170 characters are allowed");
			return errors;
		}
	}
</script>
