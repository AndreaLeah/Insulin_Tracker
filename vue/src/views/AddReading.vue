<template>
    <div id="readingContainer">
        <h2>Add new reading</h2>
        <form>
            <profile-select/>
            <label for="bloodSugar"></label><input type="text" id='bloodSugar' placeholder="Blood Sugar" v-model="newReading.bloodSugar"/>
            <button @click.prevent="addReading">Add</button>
        </form>
    </div>
</template>

<script>
import ReadingService from '../services/ReadingsService.js';
import ProfileSelect from '../components/ProfileSelect.vue';

export default {
    data() {
        return {
            newReading: {
                readingId: 0,
                userId: 0,
                profileId: '',
                bloodSugar: '',
                carbs: '',
                time: ''
            }
        }
    },
    components: {
        ProfileSelect,
    },
    methods: {
        checkReading() {
            let profile = this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1];
            if (this.newReading.bloodSugar < profile.minWarningSugar) {
                alert ("DANGEROUSLY LOW BLOOD SUGAR")
            }
            else if (this.newReading.bloodSugar < profile.minBloodSugar) {
                alert("Low blood sugar");
            }

            if (this.newReading.bloodSugar > profile.maxWarningSugar) {
                alert ("DANGEROUSLY HIGH BLOOD SUGAR")
            }
            else if (this.newReading.bloodSugar > profile.maxBloodSugar) {
                alert("HIGH blood sugar");
            }
        },
        addReading() {
            this.newReading.bloodSugar = +this.newReading.bloodSugar;
            this.newReading.profileId = +this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1].profileId;
            this.newReading.carbs = +0;

            let d = new Date();

            let dateTime = 
                d.getFullYear() + '-' + 
                (d.getMonth()+1).toLocaleString('en-US', {minimumIntegerDigits: 2, useGrouping:false}) + '-' + 
                d.getDate().toLocaleString('en-US', {minimumIntegerDigits: 2, useGrouping:false}) + 'T' + 
                d.getHours() + ':' + 
                d.getMinutes().toLocaleString('en-US', {minimumIntegerDigits: 2, useGrouping:false}) + ':' + 
                d.getSeconds().toLocaleString('en-US', {minimumIntegerDigits: 2, useGrouping:false}) + '.' + 
                d.getMilliseconds();

            this.newReading.time = dateTime;

            ReadingService.addReading(this.newReading)
                .then(response => {
                    if (response.status === 200) {
                        response;
                        this.checkReading();
                        this.$router.push({name: 'Profile'});
                    }
                    else {
                        console.log("Error");
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }
}
</script>

<style scoped>

#readingContainer {
    display: flex;
    justify-self: center;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
    background-color: white;
    width: 300px;
    height: 200px;
    border-radius: 10px;
    align-content: center;
    text-align: center;
    margin: 4rem auto;
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

#readingContainer > form > button {
  background-image: linear-gradient(to left, rgb(255, 142, 142), rgb(233, 64, 64));
  width: 200px;
  border-style: none;
  border-radius: 15px;
  margin: .5rem;
}
</style>