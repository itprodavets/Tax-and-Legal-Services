<template>
    <p>{{ $t('hello') }}</p>
</template>

<script lang="ts">
	import {Component, Vue} from "vue-property-decorator";

	@Component
	export default class HelloI18nComponent extends Vue {}
</script>

<i18n>
    {
    "en": {
    "hello": "Hello i18n in SFC!"
    }
    }
</i18n>
