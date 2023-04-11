<template>
  <section>
    <h1>Profile {{ profile.profileId }}</h1>
    <p>Basal Rate: {{ profile.basalRate }}</p>
    <p>Blood Sugar Range: {{profile.minBloodSugar}} - {{profile.maxBloodSugar}}</p>
    <p>Carb Ratio: 1:{{profile.carbRatio}}</p>
    <p>Correction Ratio: 1:{{profile.correctionRatio}}</p>
    <p>Insulin Type: {{ profile.insulinType }}</p>
    <p>Insulin Strength: {{ profile.insulinStrength }}</p>
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
            console.error("Couldn't get profile", error);
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
}

p{
    display: flex;
    flex-grow: 1fr;
}

</style>