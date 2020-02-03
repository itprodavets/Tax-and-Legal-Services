<template>
    <div class="constituent-entity">TEST</div>
</template>
<script lang="ts">
	import {Component, Prop, Vue} from "vue-property-decorator";

	@Component({
		components: {}
	})
	export default class ConstituentEntityComponent extends Vue {
		@Prop()
		public readonly!: boolean;
	}
</script>
<style lang="scss" scoped>
    .constituent-entity {
        width: 100%;
    }
</style>