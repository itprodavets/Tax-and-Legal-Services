<template>
	<v-app class="app">
		<v-navigation-drawer app v-model="drawer" absolute temporary>
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
		<v-app-bar app class="elevation-1">
			<v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
			<v-toolbar-title>Tax and Legal Services</v-toolbar-title>
		</v-app-bar>
		<v-content class="cbc-content">
			<v-container fluid>
				<router-view></router-view>
			</v-container>
		</v-content>
		<v-footer app class="cbc-footer">
			<v-container fluid class="pa-0">
				<v-row>
					<v-col cols="12" style="text-align: center">
					<span>
						Copyright © {{ this.$moment().year() }}
                        <a href="mailto: itprodavets@gmail.com">Denis Kharchenko</a>
					</span>
					</v-col>
				</v-row>
			</v-container>
		</v-footer>
	</v-app>
</template>

<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";

	@Component({
		components: {},
		mounted() {
			this.$store.dispatch("country/list");
			this.$store.dispatch("language/list");
			this.$store.dispatch("currency/list");
		}
	})
	export default class App extends Vue {
		public drawer: boolean = false;
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
			background-color: #dedede;
		}

		.cbc-footer {
			background-color: #fff;
		}
	}

	table {
		thead {
			background-color: #f9f9fc;
			text-transform: uppercase;
		}

		tbody {
			tr {
				cursor: pointer;
			}

			tr:nth-child(2n) {
				background: #f9f9fc;
			}

			.v-data-table__mobile-table-row {
				.v-data-table__mobile-row {
					.v-data-table__mobile-row__header {
						font-size: 12px;
						text-transform: uppercase;
					}

					.v-data-table__mobile-row__cell {
						font-size: 11px;
					}
				}
			}
		}
	}
</style>
