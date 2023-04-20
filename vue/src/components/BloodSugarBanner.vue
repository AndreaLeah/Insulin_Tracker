<template>
    <div>
        <div class="banner" v-if="this.$store.state.isUserLowBloodSugar">
            <div>LOW BLOOD SUGAR</div>
            <button @click.prevent="closeBanner">x</button>
        </div>
        <div class="banner" v-if="this.$store.state.isUserHighBloodSugar">
            <div>HIGH BLOOD SUGAR</div>
            <button @click.prevent="closeBanner">x</button>
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
    height: 90px;
    width: 100vw;

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
    top: 30px;
    right: 10px;
    font-size: 30px;
    border: solid black 0px;
    background-color: rgba(0, 0, 0, 0);
}

</style>