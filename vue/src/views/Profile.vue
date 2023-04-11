<template>
  <div>
    <h1>Your Profiles</h1>
    <div v-for="(profile, index) in profiles" v-bind:key="profile.profileId">
      <button v-on:click="viewProfile(profile.profileId)"> {{index + 1}} </button>
    </div>
    <router-link :to="{name: 'AddProfile'}">Add Profile</router-link>
  </div>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService.js'

export default {
  name: "profile",
  data(){
    return {
      profiles: []
    }
  },
  methods: {
    viewProfile(profileId) {
      this.$router.push({name: 'ProfileDetails', params: {
        profileId: profileId,
      }});
    },
    getUserProfiles() {
      ProfileInfoService.getUserProfiles()
      .then((response) => {
        if (response.status === 200) {
          this.profiles = response.data;
        }
      })
      .catch((error) => {
        console.error("Couldn't update profileeeee", error);
      });
    },
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
    this.getUserProfiles();
  }
}
</script>

<style scoped>

div{
  text-align: center;
  margin-top: 1rem;
}

button{
  border: thin solid rgb(117, 0, 0);
}

router-link{
  padding-top: 1rem;
}

</style>