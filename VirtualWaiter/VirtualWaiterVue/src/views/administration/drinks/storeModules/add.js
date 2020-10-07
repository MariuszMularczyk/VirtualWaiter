import axios from 'axios';
import { getField, updateField } from 'vuex-map-fields';
const state = {
    Drink: {
        Name: '',
        Description: '',
        Price: 0,
    }
};

const getters = {
    getField,
    getDrink(state) {
        return state.Drink;
    },
};

const mutations = {
    updateField,
    updateUserField(state, drink) {
        state.Drink.Name = drink.Name;
        state.Drink.Description = drink.Description;
        state.Drink.Price = drink.Price;
    },
};

const actions = {
    addDrink({ state }, ) {
        console.log(state.Drink)
        axios.post('/drinks/add', state.Drink).then(() => this.$router.push({ name: 'administration.drinksList' }));
    }

};

export default { state, getters, mutations, actions };




