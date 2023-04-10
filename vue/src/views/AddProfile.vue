<template>
    <div>
    <h1>Add Profile</h1>
    <h3>Your profile information:</h3>
    <form>
      <div>
        <label for="baselRate">Basel rate: </label>
        <input type="text" id="basalRate" placeholder="0.000" v-model="profile.basalRate" required>
      </div>
      <div>
        <label for="targetBloodRange">Target blood range: </label>
        <input type="text" id="targetBloodRange" placeholder="70" v-model="profile.minBloodSugar" required style="width: 40px;">
        <p> - </p>
        <input type="text" id="targetBloodRange" placeholder="130" v-model="profile.maxBloodSugar" required style="width: 40px;">
      </div>
      <div>
        <label for="carbRatio">Carb ratio: </label>
        <p id="ratio"> 1 : </p>
        <input type="text" id="carbRatio" placeholder="10" v-model="profile.carbRatio" required style="width: 40px;">
      </div>
      <div>
        <label for="correctionRatio">Correction ratio: </label>
        <p id="ratio"> 1 : </p>
        <input type="text" id="correctionRatio" placeholder="35" v-model="profile.correctionRatio" required style="width: 40px;">
      </div>
      <div>
        <label for="insulinType">Insulin type: </label>
        <input type="text" id="insulinType" placeholder="Humolog" v-model="profile.insulinType" required>
      </div>
      <div>
        <label for="insulinStrength">Insulin strength: </label>
        <input type="text" id="insulinStrength" placeholder="u100" v-model="profile.insulinStrength" required>
      </div>
        <button @click.prevent="addProfile">Save</button>
    </form>
  </div>
</template>

<script>
import ProfileInfoService from "../services/ProfileInfoService.js"

export default {
  name: "addProfile",
  data(){
    return {
      profile: {
        profileId: 0,
        userId: 0,
        basalRate: 0.00,
        minBloodSugar: 0,
        maxBloodSugar: 0,
        carbRatio: 0,
        correctionRatio: 0,
        insulinType: '',
        insulinStrength: 0
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
        this.profile.carbRatio = +this.profile.carbRatio;
        this.profile.correctionRatio = +this.profile.correctionRatio;
        this.profile.insulinStrength = +this.profile.insulinStrength;
        ProfileInfoService.addProfileInfo(this.profile)
          .then(response => {
            this.profile = response.data;
            console.log(response);
            this.$router.push({name: 'Profile'});
          })
          .catch(error => console.error('Could not add profile', error));
    }
  }
}
</script>

<style scoped>

form > div{
  display: block;
}

#ratio{
  padding-left: .5rem;
}

div > p{
  display: inline;
}

input{
  margin: 0.5rem;
  align-content: right;
}

label{
  align-content: left;
}

</style>