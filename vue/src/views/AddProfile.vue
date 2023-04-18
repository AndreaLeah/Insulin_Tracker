<template>
    <div id="profileContainer">
      <h2>Add New Profile</h2>
      <form>
        <div>
          <label for="baselRate"></label>
          <input type="text" id="basalRate" placeholder="Basal rate" v-model="profile.basalRate" required>
        </div>
        <div>
          <label for="minBloodSugar"></label>
          <input type="text" id="minBloodSugar" placeholder="Min blood sugar range" v-model="profile.minBloodSugar">
        </div>
        <div>
          <label for="maxBloodSugar"></label>
          <input type="text" id="maxBloodSugar" placeholder="Max blood sugar range" v-model="profile.maxBloodSugar">
        </div>
        <div>
          <label for="minWarningSugar"></label>
          <input type="text" id="minWarningSugar" placeholder="Min warning sugar" v-model="profile.minWarningSugar">
        </div>
        <div>
          <label for="maxWarningSugar"></label>
          <input type="text" id="maxWarningSugar" placeholder="Max warning sugar" v-model="profile.maxWarningSugar">
        </div>
        <div>
          <label for="carbRatio"></label>
          <input type="text" id="carbRatio" placeholder="Carb ratio" v-model="profile.carbRatio">
        </div>
        <div>
          <label for="correctionRatio"></label>
          <input type="text" id="correctionRatio" placeholder="Correction ratio" v-model="profile.correctionRatio">
        </div>
        <div>
          <label for="insulinType"></label>
          <input type="text" id="insulinType" placeholder="Insulin type" v-model="profile.insulinType" required>
        </div>
        <div>
          <label for="insulinStrength"></label>
          <input type="text" id="insulinStrength" placeholder="Insulin strength" v-model="profile.insulinStrength" required>
        </div>
          <button @click.prevent="addProfile">Save</button>
      </form>
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

<style scoped>
#profileContainer {
    display: flex;
    justify-self: center;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
    background-color: white;
    width: 300px;
    height: 500px;
    border-radius: 10px;
    align-content: center;
    text-align: center;
    margin: 4rem auto;
}

form > div, .router-link{
  display: flex;
  justify-content: center;
}

div > p{
  display: inline;
}

input{
  border: 1px solid rgb(219, 219, 219);
  border-radius: 15px;
  width: 200px;
  margin: 0.5rem;
  align-content: right;
  padding-left: .5rem;
}

label{
  align-content: left;
  font-weight: bold;
}

#profileContainer > form > button {
  background-image: linear-gradient(to left, rgb(255, 142, 142), rgb(233, 64, 64));
  width: 200px;
  border-style: none;
  border-radius: 15px;
  margin: .5rem;
}

</style>