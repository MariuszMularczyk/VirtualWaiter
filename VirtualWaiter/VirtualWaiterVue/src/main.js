import Vue from 'vue'
import App from './App.vue'
import router from './router/index';
import store from './store/index';
import 'bootstrap/dist/css/bootstrap.min.css';
import './assets/sass/styles.sass';
import axios from 'axios';
import Notifications from 'vue-notification'

axios.defaults.baseURL = "https://localhost:44303/api";

Vue.config.productionTip = false;
Vue.use(Notifications)

new Vue({
  render: h => h(App),
  router,
  store
}).$mount('#app');
