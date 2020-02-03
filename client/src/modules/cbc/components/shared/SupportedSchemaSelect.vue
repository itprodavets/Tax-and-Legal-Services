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
	import {ReferenceBook} from "@/core/models";
	import {Component, Mixins, Prop} from "vue-property-decorator";
	import {CbcMixin} from "../../mixins";
	import {SupportedSchema} from "../../models";

	@Component({
		components: {}
	})
	export default class SupportedSchemaSelectComponent extends Mixins(CbcMixin) {
		@Prop() value!: SupportedSchema | string;

		public data() {
			return {
				supportedSchema: ""
			};
		}

		public created() {
			this.$data.supportedSchema = typeof this.value === "string" ? this.supportedSchemas.find(
				x => SupportedSchema[x.id] === this.value
			) : this.supportedSchemas.find(
				x => x.id === this.value
			);
		}

		public onSelectedSchema(supportedSchema: ReferenceBook<SupportedSchema>) {
			this.onSave(supportedSchema.id);
			this.$data.supportedSchema = supportedSchema;
		}

		public onSave(supportedSchema: SupportedSchema) {
			this.$emit("input", supportedSchema);
		}
	}
</script>