<template>
    <form role="form" @submit.prevent="addDrink">


        <div class="form-group">
            <label for="drinkName">Nazwa</label>
            <input type="text"  v-model="Name" class="form-control" id="drinkName">
        </div>

        <div class="form-group">
            <label for="description" class="control-label">Opis</label>
            <textarea v-model="Description" id="description" class="form-control"></textarea>
        </div>
        <div class="form-group">
            <label for="price" class="control-label">cena</label>
            <input type="number" v-model="Price" id="price" class="form-control">
        </div>

        <button type="submit" class="btn btn-primary">Zapisz</button>
        <router-link :to="{name: 'administration.drinksList'}"><button type="default" class="btn btn-primary" style="margin-left: 20px;">Wróć</button></router-link>
    </form>
</template>

<script>
    import { mapActions } from "vuex";
    import { mapFields } from 'vuex-map-fields';
    export default {
        name: "DrinkAdd",
        data() {
            return {
            }
        },
        computed: {
            ...mapFields(['Drink.Name','Drink.Description', 'Drink.Price']),
        },
        methods: {
            ...mapActions(['addDrink']),
            
        }
    }
</script>

<style scoped>
</style>