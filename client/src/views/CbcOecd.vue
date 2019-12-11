<template>
	<div class="cbc-oecd">
		<MessageSpecComponent />
		<CbcBodyComponent />
	</div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import CbcBodyComponent from "../components/cbc/CbcBody.vue";
import MessageSpecComponent from "../components/cbc/MessageSpec.vue";

@Component({
  components: {
    CbcBodyComponent,
    MessageSpecComponent
  }
})
export default class CbcOecd extends Vue {
  public created() {
    this.$store.dispatch("language/list");
  }
}
</script>
<style lang="scss" scoped>
.cbc-oecd {
	width: 100%;
}
</style>
