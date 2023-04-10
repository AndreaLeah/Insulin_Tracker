<template>
  <div>
    <div v-for="(profile, index) in profiles" v-bind:key="profile.profileId">
      <button v-on:click="viewProfile(profile.profileId)"> {{index + 1}} </button>
    </div>
  </div>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService.js'

export default {
  name: "profile",
  data(){
    return {
      profile: undefined,
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
          console.log(this.profiles);
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