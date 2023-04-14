<template>
    <div id="container">
    <h2>Add New Profile</h2>
    <form>
      <div>
        <label for="baselRate"></label>
        <input type="text" id="basalRate" placeholder="Basal rate" v-model="profile.basalRate" required>
      </div>
      <div class="blood-sugar-range">
        <label for="targetBloodRange"></label>
        <input type="text" id="targetBloodRange" placeholder="Min blood sugar range" v-model="profile.minBloodSugar">
      </div>
      <div>
        <input type="text" id="targetBloodRange" placeholder="Max blood sugar range" v-model="profile.maxBloodSugar">
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

export default {
  name: "addProfile",
  //beforeCreate() {
  //},
  //beforeDestroy() {
  //    document.body.className = 'testClass';
  //},
  data(){
    return {
      profile: {
        profileId: '',
        userId: '',
        basalRate: '',
        minBloodSugar: '',
        maxBloodSugar: '',
        carbRatio: '',
        correctionRatio: '',
        insulinType: '',
        insulinStrength: ''
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
          this.$router.push({name: 'Profile'});
        })
        .catch(error => console.error('Could not add profile', error));
    }
  }
}
</script>

<style>

/* This needs to unscoped. It is applied to the App.vue div*/
.AddProfileBackground{
  background-image: linear-gradient(rgb(255, 195, 195), rgb(233, 64, 64));

}

#container {
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

#container > form > button {
  background-image: linear-gradient(to left, rgb(255, 142, 142), rgb(233, 64, 64));
  width: 200px;
  border-style: none;
  border-radius: 15px;
  margin: .5rem;
}

</style>