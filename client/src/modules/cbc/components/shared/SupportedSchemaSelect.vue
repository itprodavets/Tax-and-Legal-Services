<template>
    <div class="text-center">
        <v-menu offset-y>
            <template v-slot:activator="{ on }">
                <v-btn text small v-on="on">version: {{supportedSchema.name}}</v-btn>
            </template>
            <v-list>
                <v-list-item
                        v-for="schema in supportedSchemas"
                        :key="schema.id"
                        @click="onSelectedSchema(schema)"
                >
                    <v-list-item-title>{{schema.name}}</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>
    </div>
</template>
<script lang="ts">
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {SupportedSchema} from "../../models";
	import {CbcMixin} from "../../mixins";
	import {ReferenceBook} from "@/core/models";

	@Component({
		components: {}
	})
	export default class SupportedSchemaSelectComponent extends Mixins(CbcMixin) {
		@Prop() value!: SupportedSchema;

		public data() {
			return {
				supportedSchema: ""
			};
		}

		public created() {
			this.$data.supportedSchema = this.supportedSchemas.find(
				x => x.id === this.value
			);
		}

		public onSelectedSchema(supportedSchema: ReferenceBook) {
			this.onSave(supportedSchema.id);
			this.$data.supportedSchema = supportedSchema;
		}

		public onSave(supportedSchema: SupportedSchema) {
			this.$emit("input", supportedSchema);
		}
	}
</script>