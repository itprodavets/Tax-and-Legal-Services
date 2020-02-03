<template>
    <div class="additional-info-list">
        <v-toolbar dense class="additional-info-list-toolbar">
            <template>
                <v-btn dense icon @click.stop="isOpenDialog = true">
                    <v-icon>mdi-plus-circle</v-icon>
                </v-btn>
            </template>
            <v-toolbar-title>Additional Info</v-toolbar-title>
        </v-toolbar>
        <v-data-table dense :headers="headers" :items="items"></v-data-table>
        <v-dialog v-model="isOpenDialog" max-width="95vw">
            <v-card align="center">
                <v-card-title class="headline">Create Additional Info</v-card-title>
                <v-card-text>
                    <AdditionalInfoComponent :readonly="false" :countries="countries" :languages="languages"/>
                </v-card-text>

                <v-card-actions class="justify-center">
                    <v-btn class="ma-2" tile outlined color="success" @click="isOpenDialog = false">
                        <v-icon left>mdi-content-save</v-icon>
                        Save
                    </v-btn>
                    <v-btn class="ma-2" tile outlined color="success" @click="isOpenDialog = false">
                        <v-icon left>mdi-content-save-move</v-icon>
                        Save & Next
                    </v-btn>
                    <v-btn class="ma-2" tile outlined color="warning" @click="isOpenDialog = false">
                        <v-icon left>mdi-cancel</v-icon>
                        Cancel
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";
	import {validationMixin} from "vuelidate";

	import AdditionalInfoComponent from "./AdditionalInfo.vue";
	import {Country} from "@/modules/country/models/dto.model";
	import {Language} from "@/modules/language/models/dto.model";

	@Component({
		components: {
			AdditionalInfoComponent
		},
		mixins: [validationMixin],
		validations: {}
	})
	export default class AdditionalInfoDataTableComponent extends Vue {
		@Prop()
		public languages!: Language[];

		@Prop()
		public readonly countries!: Country[];

		public readonly: boolean = true;

		public onEdit() {
			this.readonly = !this.readonly;
		}

		public data() {
			return {
				isOpenDialog: false as boolean,
				headers: [
					{
						text: "Other Info",
						value: "otherInfo"
					},
					{
						text: "Language",
						value: "language"
					},
					{
						text: "Res Country Code",
						value: "resCountryCode"
					},
					{
						text: "Summary Ref",
						value: "summaryRef"
					}
				],
				items: []
			};
		}
	}
</script>
<style lang="scss" scoped>
    .additional-info-list {
        width: 100%;

        &-toolbar {
            box-shadow: none;
        }
    }
</style>