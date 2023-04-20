<template>
<div id="main-content">
  <div class="addFormContainer">
    <h2>Add New Profile</h2>
    <form autocomplete="off" id="form-body">
      <label for="profileName">Profile Name</label>
      <input type="text" id="profileName" v-model="profile.profileName" required>
      <label for="baselRate">Basal Rate</label>
      <input type="text" id="basalRate" v-model="profile.basalRate" required>
      <label for="minBloodSugar">Min Blood Sugar Range</label>
      <input type="text" id="minBloodSugar" v-model="profile.minBloodSugar">
      <label for="maxBloodSugar">Max Blood Sugar Range</label>
      <input type="text" id="maxBloodSugar" v-model="profile.maxBloodSugar">
      <label for="minWarningSugar">Min warning range</label>
      <input type="text" id="minWarningSugar" v-model="profile.minWarningSugar">
      <label for="maxWarningSugar">Max warning range</label>
      <input type="text" id="maxWarningSugar" v-model="profile.maxWarningSugar">
      <label for="carbRatio">Carb ratio</label>
      <input type="text" id="carbRatio" v-model="profile.carbRatio">
      <label for="correctionRatio">Correction ratio</label>
      <input type="text" id="correctionRatio" v-model="profile.correctionRatio">
      <label for="insulinType">Insulin Type</label>
      <input type="text" id="insulinType" v-model="profile.insulinType" required>
      <label for="insulinStrength">Insulin strength</label>
      <input type="text" id="insulinStrength" v-model="profile.insulinStrength" required>
      <button @click.prevent="addProfile">Save</button>
    </form>
  </div>
</div>
</template>

<script>
import ProfileInfoService from "../services/ProfileInfoService.js"
import ActivityService from "../services/ActivityService.js"

export default {
  name: "addProfile",
  data(){
    return {
      profile: {
        profileId: '',
        userId: '',
        profileName: '',
        basalRate: '',
        minBloodSugar: '',
        maxBloodSugar: '',
        minWarningSugar: '',
        maxWarningSugar: '',
        carbRatio: '',
        correctionRatio: '',
        insulinType: '',
        insulinStrength: ''
      },
      activity: {
        logId: 0,
        userId: 0,
        activityName: '',
        time: '2000-01-01T00:00:00.000'
      }
    }
  },
  methods: {
    addProfile(){
      this.profile.profileId = +this.profile.profileId;
      this.profile.userId = +this.profile.userId;
      this.profile.basalRate = +this.profile.basalRate;
      this.profile.minBloodSugar = +this.profile.minBloodSugar;
      this.profile.maxBloodSugar = +this.profile.maxBloodSugar;
      this.profile.minWarningSugar = +this.profile.minWarningSugar;
      this.profile.maxWarningSugar = +this.profile.maxWarningSugar;
      this.profile.carbRatio = +this.profile.carbRatio;
      this.profile.correctionRatio = +this.profile.correctionRatio;
      this.profile.insulinStrength = +this.profile.insulinStrength;

      ProfileInfoService.addProfileInfo(this.profile)
        .then(response => {
          this.profile = response.data;
          this.activity.activityName = 'profile added'
          ActivityService.addActivityToLog(this.activity);
          this.$router.push({name: 'Profile'});
        })
        .catch(error => console.error('Could not add profile', error));
    },
  }
}
</script>

<style scoped src="../styles/addForm.css">
</style>