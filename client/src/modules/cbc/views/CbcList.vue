<template>
	<div class="cbc-oecd-list">
        test
	</div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component({
  components: {}
})
export default class CbcList extends Vue {
  public created() {
    this.$store.dispatch("cbc/list");
  }
}
</script>
<style lang="scss" scoped>
.cbc-oecd-list {
	width: 100%;
}
</style>
