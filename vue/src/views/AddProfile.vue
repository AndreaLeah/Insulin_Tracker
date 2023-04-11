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
            console.log(response);
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
  max-width: 100%;
  max-height: 100%;
  height: 100vh;
  width: 100vw;
  margin: 0;
  padding: 0;
}


.container {
    display: flex;
    flex-direction: column;
    align-items: center;
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
}

form > div, .router-link{
  display: flex;
  justify-content: center;
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
  font-weight: bold;
}

button {
  background-image: linear-gradient(to left, rgb(255, 142, 142), rgb(233, 64, 64));
  width: 200px;
  border-style: none;
  border-radius: 15px;
}

input {
  border: 1px solid rgb(219, 219, 219);
  border-radius: 15px;
  width: 200px;
}

.router-link {
  margin-top: 30px;
}

</style>