<template>
    <div>
        <div class="banner" v-if="this.$store.state.isUserLowBloodSugar">
            <div>Low Blood Sugar</div>
            <button @click.prevent="closeBanner">X</button>
        </div>
        <div class="banner" v-if="this.$store.state.isUserHighBloodSugar">
            <div>High Blood Sugar</div>
            <button @click.prevent="closeBanner">X</button>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        closeBanner() {
            this.$store.state.isUserLowBloodSugar = false;
            this.$store.state.isUserHighBloodSugar = false;
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
    width: 150px;
    height: 60px;

    border-radius: 10px;
    background-color: rgb(221, 58, 58);
}

.banner > div {
    position: absolute;
    top: auto;
    right: auto;
    color: black;
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