import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import vuelidate from 'vuelidate'
import i18n from './i18n'
import moment from 'moment';
import lodash from 'lodash';

import 'flag-icon-css/css/flag-icon.min.css';

Vue.use(vuelidate);
Vue.config.productionTip = false;
Vue.prototype.$moment = moment;
Vue.prototype.$_ = lodash;

new Vue({
  router,
  store,
  vuetify,
  i18n,
  render: h => h(App)
}).$mount('#app');
