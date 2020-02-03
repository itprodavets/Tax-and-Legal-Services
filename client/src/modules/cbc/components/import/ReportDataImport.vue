<template>
    <v-dialog v-model="dialog" width="500">
        <template v-slot:activator="{ on }">
            <v-btn v-on="on" dense icon>
                <v-icon>mdi-file-import</v-icon>
            </v-btn>
        </template>
        <v-card>
            <v-card-title class="headline justify-center align-center">Import "Country by Country Reporting"
            </v-card-title>

            <v-card-text class="mt-8">
                <v-file-input
                        v-model="files"
                        accept="text/xml"
                        placeholder="Load your xml documents"
                        label="File input"
                        multiple
                        prepend-icon="mdi-paperclip"
                        show-size
                >
                    <template v-slot:selection="{ text }">
                        <v-chip
                                small
                                label
                                color="primary"
                        >
                            {{ text }}
                        </v-chip>
                    </template>
                </v-file-input>
            </v-card-text>

            <v-card-actions class="justify-center">
                <v-btn :disabled="files.length === 0" class="ma-2" color="success" outlined tile @click="onParse()">
                    <v-icon left>mdi-file-import</v-icon>
                    Parse
                </v-btn>
                <v-btn class="ma-2" color="warning" outlined tile @click="dialog = false">
                    <v-icon left>mdi-cancel</v-icon>
                    Cancel
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";

	@Component
	export default class ReportDataImportComponent extends Vue {

		public data() {
			return {
				files: [] as File[],
				dialog: false as boolean
			}
		}

		public onParse() {
			const files = (this.$data.files as File[]);
			files.forEach(file => this.$emit("parse-file", file));
			this.$data.dialog = false;
		}
	}

</script>