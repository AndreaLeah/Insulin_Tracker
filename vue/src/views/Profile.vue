<template>
  <section>
    <h1>Your Profiles</h1>
    <div v-for="(profile, index) in profiles" v-bind:key="profile.profileId">
      <button class="fancyButton" v-on:click="viewProfile(profile.profileId)"> {{index + 1}} </button>
    </div>
    <router-link :to="{name: 'AddProfile'}">Add Profile</router-link>
  </section>
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

section{
  display: flex;
  flex-direction: column;
  padding: 1rem;
  border: thin solid black;
  border-radius: 10px;
  box-shadow: 5px 0px 5px rgba(0, 0, 0, 0.1);
  width: 20rem;
  align-items: center;
  margin: 4rem auto;
}

a{
  text-align: center;
  padding-top: 1rem;
}

@keyframes buttonAnimation {
  from {background-color: rgb(255, 215, 215);}
  to {background-color: #ea5151;}
}

button{
  border: thin solid rgb(117, 0, 0);
  width: 200px;
  border-radius: 15px;
  background-color: white;
}

.fancyButton:hover, .fancyButton:focus{
  animation-name: buttonAnimation;
  animation-duration: .2s;
  animation-fill-mode: forwards;
}

router-link{
  text-align: center;
  padding-top: 1rem;
}

</style>