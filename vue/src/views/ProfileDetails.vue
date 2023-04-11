<template>
  <div>
      <div>
          <p>Profile Information:</p>
          <p>Basal Rate: {{ profile.basalRate }}</p>
          <p>Blood Sugar Range: {{profile.minBloodSugar}} - {{profile.maxBloodSugar}}</p>
          <p>Carb Ratio: 1:{{profile.carbRatio}}</p>
          <p>Correction Ratio: 1:{{profile.correctionRatio}}</p>
          <p>Insulin Type: {{ profile.insulinType }}</p>
          <p>Insulin Strength: {{ profile.insulinStrength }}</p>
      </div>
  </div>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService'
export default {
    data() {
        return {
            profile: []
        }
    },
    beforeCreate() {
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

<style>

</style>