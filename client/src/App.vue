<template>
	<v-app class="app">
		<v-navigation-drawer app v-model="drawer" absolute bottom temporary>
			<v-list-item link to="/">
				<v-list-item-title>Tax and Legal Services</v-list-item-title>
			</v-list-item>
			<v-divider></v-divider>
			<v-list>
				<v-list-item link to="/cbc-report">
					<v-list-item-icon>
						<v-icon>mdi-vanity-light</v-icon>
					</v-list-item-icon>
					<v-list-item-content>
						<v-list-item-title>CBC Reporting (OECD)</v-list-item-title>
					</v-list-item-content>
				</v-list-item>
			</v-list>
		</v-navigation-drawer>
		<v-app-bar app>
			<v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
			<v-toolbar-title>Tax and Legal Services</v-toolbar-title>
		</v-app-bar>
		<v-content class="cbc-content">
			<v-container fluid class="cbc-container">
				<router-view></router-view>
			</v-container>
		</v-content>
		<v-footer app align="center">
			<v-row align="center">
				<v-col cols="12">
					<span>
						Copyright © {{ this.$moment().year() }}
						<a
							href="mailto: itprodavets@gmail.com"
						>Denis Kharchenko</a>
					</span>
				</v-col>
			</v-row>
		</v-footer>
	</v-app>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
@Component({
  components: {}
})
export default class App extends Vue {
  public data() {
    return {
      drawer: false,
      group: null
    };
  }
}
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
