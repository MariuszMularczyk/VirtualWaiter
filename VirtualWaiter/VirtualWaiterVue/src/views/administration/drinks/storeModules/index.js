import axios from 'axios';

const state = {
    drinksList: null
};

const getters = {
  getDrinksList(state) {
        return state.drinksList;
  },
};

const mutations = {
  setDrinksList(state, payload) {
        state.drinksList = payload;
  },

};

const actions = {
    setDrinksList({commit}) {
        axios.get('/drinks/getDrinks')
            .then(({ data }) => commit('setDrinksList', data));
    },

};

export default {state, getters, mutations, actions };




