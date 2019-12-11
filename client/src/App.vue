<template>
	<v-app class="app">
		<v-app-bar app>
			<!-- -->
		</v-app-bar>
		<!-- Sizes your content based upon application components -->
		<v-content class="cbc-content">
			<!-- Provides the application the proper gutter -->
			<v-container fluid class="cbc-container">
				<!-- If using vue-router -->
				<router-view></router-view>
			</v-container>
		</v-content>

		<v-footer app>
			<!-- -->
		</v-footer>
	</v-app>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
@Component({
  components: {}
})
export default class App extends Vue {}
</script>
<style lang="scss">
html {
	height: 100%;
	width: 100%;
	margin: 0;
	display: flex;
}
body {
	height: 100%;
	width: 100%;
	margin: 0;
	display: flex;
	flex-direction: column;
}
.app {
	flex-grow: 100;
	display: flex;
	flex-direction: column;
	.cbc-content {
		display: flex;
		flex-direction: column;
		width: 100%;
		height: 100%;
		.cbc-container {
			display: flex;
			flex-direction: column;
			width: 100%;
			height: 100%;
		}
	}
}
</style>
