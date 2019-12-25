<template>
	<!-- <div class="cbc-oecd">
		<MessageSpecComponent
			:languages="this.$store.state.language.entities"
			:countries="this.$store.state.country.entities"
			v-on:messageSpec="onSave"
		/>
		<CbcBodyComponent
			:languages="this.$store.state.language.entities"
			:countries="this.$store.state.country.entities"
		/>
	</div>-->
	<div class="cbc-oecd">
		<router-view />
	</div>
</template>
<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
// import MessageSpecComponent from "@/modules/cbc/components/messageSpec/MessageSpec.vue";
// import CbcBodyComponent from "@/modules/cbc/components/cbcBody/CbcBody.vue";

@Component({
  components: {
    // CbcBodyComponent,
    // MessageSpecComponent
  }
})
export default class CbcOecd extends Vue {
//   public created() {
//     //   this.$store.dispatch("country/list");
//     //   this.$store.dispatch("language/list");
//   }
  //   public onSave(messageSpec: any) {
  //     console.log(messageSpec);
  //   }
}
</script>
<style lang="scss" scoped>
.cbc-oecd {
	width: 100%;
}
</style>
