<template>
    <v-container fluid class="cbc-reports">
        <DocSpecComponent :readonly="readonly"/>
        <v-row>
            <v-col cols="12" lg="3" md="3">
                <v-autocomplete
                        dense
                        filled
                        v-model="$v.resCountryCode.$model"
                        :items="countries"
                        item-text="name"
                        label="Res Country Code"
                        :disabled="readonly"
                        return-object
                ></v-autocomplete>
            </v-col>
        </v-row>
        <SummaryComponent :readonly="readonly"/>
    </v-container>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";
	import DocSpecComponent from "../../DocSpec.vue";
	import SummaryComponent from "../../cbcBody/cbcReports/Summary.vue";
	import {Country} from "@/modules/country/models/dto.model";
	import {validationMixin} from "vuelidate";
	import {required} from "vuelidate/lib/validators";

	@Component({
		components: {
			DocSpecComponent,
			SummaryComponent
		},
		mixins: [validationMixin],
		validations: {
			resCountryCode: {
				required
			}
		}
	})
	export default class CbcReportsComponent extends Vue {
		@Prop()
		public readonly countries!: Country[];

		@Prop()
		public readonly readonly!: boolean;

		public data() {
			return {
				resCountryCode: ""
			};
		}
	}
</script>
<style lang="scss" scoped>
    .cbc-reports {
        width: 100%;
    }
</style>
