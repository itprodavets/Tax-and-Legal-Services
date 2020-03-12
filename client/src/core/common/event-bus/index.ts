import Vue, {ComponentOptions} from "vue";

export declare abstract class Bus {
  $on(action: string, fn: any): any;
  
  $emit(action: string, params?: any): any;
  
  $off(action: string, id: number): void;
  
  $subscribed(action: string): boolean;
}

export declare function install(vue: typeof Vue): void;

declare const _default: {
  Bus: typeof Bus;
  install: typeof install;
};

export default _default;

declare module "vue/types/vue" {
  interface Vue {
    $bus: Bus;
  }
}

declare module "vue/types/options" {
  interface ComponentOptions<V extends Vue> {
    bus?: Bus;
  }
}
