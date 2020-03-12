<template>
	<v-container fluid class="pa-0">
		<v-row v-if="country">
			<v-col class="pa-0">
				<span class="flag-icon" :class="getIcon(country)"></span>
				<span class="pa-2">{{country.name}}</span>
			</v-col>
		</v-row>
		<v-row v-else-if="countries && countries.length > 0" v-for="country in countries">
			<v-col class="pa-0">
				<span class="flag-icon" :class="getIcon(country)"></span>
				<span class="pa-2">{{country.name}}</span>
			</v-col>
		</v-row>
	</v-container>
</template>
<script lang="ts">
	import {Country} from "src/modules/country/models/dto.model";
	import {Component, Prop, Vue} from "vue-property-decorator";

	@Component
	export default class CompanyDisplayComponent extends Vue {
		@Prop({default: false})
		public readonly squared!: boolean;

		@Prop()
		public readonly country!: Country;

		@Prop()
		public readonly countries!: Country[];

		public getIcon(country: Country) : string {
			return (this.squared ? 'flag-icon-squared ' : '') + `flag-icon-${country.alpha2Code.toLowerCase()}`;
		}
	}
</script>