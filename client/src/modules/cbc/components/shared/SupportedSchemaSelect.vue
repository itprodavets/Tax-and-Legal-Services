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
	import {CbcMixin} from "@/modules/cbc/mixins";
	import {SupportedSchema} from "@/modules/cbc/models";
	import {Component, Emit, Mixins, Prop} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class SupportedSchemaSelectComponent extends Mixins(CbcMixin) {
		@Prop() value!: SupportedSchema | string;
		public supportedSchema!: ReferenceBook<SupportedSchema>;

		public created() {
			this.supportedSchema = this.supportedSchemas.find(x => x.id === this.value)!;
		}

		public onSelectedSchema(supportedSchema: ReferenceBook<SupportedSchema>) {
			this.onSave(supportedSchema.id);
			this.supportedSchema = supportedSchema;
		}

		@Emit("input")
		public onSave(supportedSchema: SupportedSchema) {
			return supportedSchema;
		}
	}
</script>