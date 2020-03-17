module.exports = {
	publicPath: process.env.NODE_ENV === 'production'
		? '/tax-legal/'
		: '/',
	transpileDependencies: [
		"vuetify"
	],
	pluginOptions: {
		i18n: {
			locale: 'en',
			fallbackLocale: 'en',
			localeDir: 'locales',
			enableInSFC: true
		}
	},
	css: {
		requireModuleExtension: true
	}
};
