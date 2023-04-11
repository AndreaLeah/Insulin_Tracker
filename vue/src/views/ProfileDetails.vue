<template>
  <section>
    <h1>Profile {{ profile.profileId }}</h1>
    <p>Basal Rate: {{ profile.basalRate }}</p>
    <p>Blood Sugar Range: {{profile.minBloodSugar}} - {{profile.maxBloodSugar}}</p>
    <p>Carb Ratio: 1:{{profile.carbRatio}}</p>
    <p>Correction Ratio: 1:{{profile.correctionRatio}}</p>
    <p>Insulin Type: {{ profile.insulinType }}</p>
    <p>Insulin Strength: {{ profile.insulinStrength }}</p>
    <router-link v-bind:to="{name: 'Profile'}">&#60; Back</router-link>
  </section>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService'
export default {
    data() {
        return {
            profile: []
        }
    },
    created() {
        ProfileInfoService.getProfile(+this.$route.params.profileId)
        .then((response) => {
            if (response.status === 200) {
                this.profile = response.data;
            }
            else {
                this.$router.push({name: 'home'});
            }
        })
        .catch((error) => {
            error;
            this.$router.push({name: 'home'});
        });
    }
}
</script>

<style scoped>

section{
    display: flex;
    flex-direction: column;
    padding: 1rem;
    border: thin solid black;
    border-radius: 10px;
    box-shadow: 10px 5px 5px rgb(49, 49, 49, 0.5);
}

p{
    display: flex;
    flex-grow: 1fr;
}

</style>