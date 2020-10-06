<template>
    <div>
        <div class="list-buttons">
            <router-link :to="{name: 'categories.add'}" class="btn btn-primary">
                Dodaj kategorie
            </router-link>
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th scope="col" class="name-column">Nazwa</th>
                    <th scope="col">Opis</th>
                    <th scope="col">Cena</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="drink of getDrinksList()" :key="`category-${drink.id}`">
                    <td class="name-column">{{ drink.name }}</td>
                    <td>{{ drink.description }}</td>
                    <td>{{ drink.price }}</td>
                    <td class="buttons-column">
                        <router-link :to="{name: 'categories.edit', params: {id: drink.id}}" class="btn btn-warning">
                            Edytuj
                        </router-link>
                        <button class="btn btn-danger" @click.prevent="deleteCategory(drink)">
                            Usuń
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
        <router-link to="{name: 'administrationDashboard'}"><button type="default" class="btn btn-primary">Wróć</button></router-link>
    </div>
</template>

<script>

    import { mapActions, mapGetters } from 'vuex'
    export default {
        name: "DrinksList",
        data() {
            return {
            }
        },
        computed: {
        },
        methods: {
            ...mapActions(['setDrinksList']),
            ...mapGetters(['getDrinksList'])
            /*deleteCategory(category) {
                axios.delete(`categories/${category.id}`, {
                    headers:
                    {
                        'Authorization': 'Bearer ' + this.currentUser.accessToken
                    }
                })
                    .then(() => {
                        this.categories = this.categories.filter(categoryElement => categoryElement !== category);
                    })
            }*/
        },
        mounted() {
            this.setDrinksList();
        },
    }
</script>

<style scoped lang="sass">
    .list-buttons
        margin-bottom: 20px

        .name-column
            width: 100px

        .buttons-column
            display: flex
            flex-direction: row
            button
                margin-left: 10px
</style>
