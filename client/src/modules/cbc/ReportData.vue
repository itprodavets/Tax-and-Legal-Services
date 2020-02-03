<template>
	<v-container fluid class="pa-0">
        <router-view/>
    </v-container>
</template>
<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class ReportDataView extends Vue {}
</script>
