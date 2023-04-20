<template>
    <div id="bannerParentDiv">
        <div class="banner" v-if="this.$store.state.isUserLowBloodSugar">
            <img class="img1" src='../assets/exclamation.png'/>
            <div>LOW BLOOD SUGAR</div>
            <img class="img2" src='../assets/exclamation.png'/>
            <button @click.prevent="closeBanner">x</button>
        </div>
        <div class="banner" v-if="this.$store.state.isUserHighBloodSugar">
            <img class="img1" src='../assets/exclamation.png'/>
            <div>HIGH BLOOD SUGAR</div>
            <img class="img2" src='../assets/exclamation.png'/>
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

#bannerParentDiv {
    width: 100%
}

.banner {
    display: flex;
    justify-content: center;
    align-items: center;

    z-index: 10;
    height: 90px;
    width: 100%;

    border: rgb(114, 13, 13) solid 2px;
    background-color: rgb(231, 114, 114);
}

.banner > .img1 {
    position: relative;
    right: 230px;
    width: 40px;
    height: 40px;
}

.banner > .img2 {
    position: relative;
    left: 230px;
    width: 40px;
    height: 40px;
}

.banner > div {
    position: absolute;
    top: auto;
    right: auto;
    color: rgb(185, 22, 22);
    font-weight: bold;
    font-size: 40px;
}

.banner > button {
    position: absolute;
    top: 30px;
    right: 10px;
    font-size: 40px;
    border: solid black 0px;
    background-color: rgba(0, 0, 0, 0);
}

</style>