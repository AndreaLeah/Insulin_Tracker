<template>
 <div></div>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService.js'

export default {
  name: "profile",
  data(){
    return {
      profile: {
        strength: 'u100'
      }
    }
  },
  created(){
    ProfileInfoService.updateProfileInfo(this.profile)
      .then(response => {
        this.profile = response.data;
      })
      .catch(error => console.error('Could not update profile', error))
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