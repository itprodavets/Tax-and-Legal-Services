<template></template>
<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";
import { validationMixin } from "vuelidate";
import { Country } from "@/modules/country/models/dto.model";

@Component({
  components: {},
  mixins: [validationMixin],
  validations: {}
})
export default class AdditionalInfoListComponent extends Vue {
//   @Prop()
//   public additionalInfoList: AdditionalInfo[];
  @Prop()
  public countries!: Country[];

  public readonly: boolean = true;
  public onEdit() {
    this.readonly = !this.readonly;
  }
  public data() {
    return {};
  }
}
</script>
<style lang="scss" scoped>
</style>