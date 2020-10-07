import Vue from 'vue';
import Vuex from 'vuex';
import drinksList from '../views/administration/drinks/storeModules/index';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
        drinksList
  }
});
