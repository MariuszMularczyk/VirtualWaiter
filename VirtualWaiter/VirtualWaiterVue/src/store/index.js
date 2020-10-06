import Vue from 'vue';
import Vuex from 'vuex';
import drinksList from '../views/administration/drinks/index';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
        drinksList
  }
});
