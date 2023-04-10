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
            profile: undefined
        }
    },
    created() {
        ProfileInfoService.getProfile(+this.$route.params.profileId)
        .then((response) => {
            this.profile = response.data;
        })
        .catch((error) => {
            console.error("Couldn't update profileeeee", error);
        });
    }
}
</script>

<style>

</style>