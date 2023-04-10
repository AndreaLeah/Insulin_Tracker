<template>
  <div>
    <h1>Profile</h1>
    <h3>Your profile information:</h3>
    <form>
      <div>
        <label for="baselRate">Basel rate: </label>
        <input type="text" placeholder="0.000" required>
      </div>
      <div>
        <label for="baselRate">Target blood range: </label>
        <input type="text" placeholder="70" required style="width: 40px;">
        <p> - </p>
        <input type="text" placeholder="130" required style="width: 40px;">
      </div>
      <div>
        <label for="baselRate">Carb ratio: </label>
        <input type="text" placeholder="1" required style="width: 40px;">
        <p> : </p>
        <input type="text" placeholder="10" required style="width: 40px;">
      </div>
      <div>
        <label for="baselRate">Correction ratio: </label>
        <input type="text" placeholder="1" required style="width: 40px;">
        <p> : </p>
        <input type="text" placeholder="35" required style="width: 40px;">
      </div>
      <div>
        <label for="baselRate">Insulin type: </label>
        <input type="text" placeholder="Humolog" required>
      </div>
      <div>
        <label for="baselRate">Insulin strength: </label>
        <input type="text" placeholder="u100" required>
      </div>
      <button type="submit">Save</button>
    </form>
      <button type="submit" v-on:click="getProfile">Test</button>
  </div>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService.js'

export default {
  name: "profile",
  data(){
    return {
      profile: undefined
    }
  },
  methods: {
    getProfile() {

      ProfileInfoService.getProfile(1) 

      .then((response) => {

        if (response.status === 200) {
          console.log(response.data);
        }
      })
      .catch((error) => {
        
        console.error("Couldn't update profileeeee", error);
        
        });

    }
  },
  created(){

  }
}
</script>

<style scoped>

form > div{
  display: block;
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