<template>
    <div>
        <div class="banner" v-if="this.$store.state.isUserLowBloodSugar">
            <div>LOW BLOOD SUGAR</div>
            <button @click.prevent="closeBanner">X</button>
        </div>
        <div class="banner" v-if="this.$store.state.isUserHighBloodSugar">
            <div>HIGH BLOOD SUGAR</div>
            <button @click.prevent="closeBanner">X</button>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        closeBanner() {
            this.$store.commit('SET_LOW', false);
            this.$store.commit('SET_HIGH', false);
        }
    }
}
</script>

<style scoped>

.banner {
    display: flex;
    justify-content: center;
    align-items: center;

    z-index: 10;
    position: fixed;
    top: 10%;
    right: 1%;
    width: 200px;
    height: 90px;

    border-radius: 10px;
    background-color: rgb(221, 58, 58);
}

.banner > div {
    position: absolute;
    top: auto;
    right: auto;
    color: white;
    font-weight: bold;
}

.banner > button {
    position: absolute;
    top: 0;
    right: 0;
    font-size: 16px;
    border: solid black 0px;
    background-color: rgba(0, 0, 0, 0);
}

</style>