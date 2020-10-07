import Vue from 'vue';
import Vuex from 'vuex';
import drinksList from '../views/administration/drinks/storeModules/index';
import drinkAdd from '../views/administration/drinks/storeModules/add';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
        drinksList, drinkAdd
  }
});
