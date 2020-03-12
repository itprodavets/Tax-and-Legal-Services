import {PropOptions} from "vue";
import {createDecorator} from "vue-class-component";
import {Constructor} from "vue/types/options";
import {Vue} from "vue/types/vue";

export function PropTestSync(
	propName: string,
	options: PropOptions | Constructor[] | Constructor = {},
): PropertyDecorator {
	// @ts-ignore
	return (target: Vue, key: string) => {
		createDecorator((componentOptions, k) => {
			(componentOptions.props || (componentOptions.props = {} as any))[propName] = options;
			(componentOptions.computed || (componentOptions.computed = {}))[k] = {
				get() {
					return (this as any)[propName]
				},
				set(value) {
					// @ts-ignore
					this.$emit(`update:${propName}`, value)
				},
			};
		})(target, key)
	}
}