<template>
    <div>
        <h2>Add new reading</h2>
        <form>
            <label for="bloodSugar"></label><input type="text" id='bloodSugar' placeholder="Blood Sugar" v-model="newReading.bloodSugar"/>
            <label for="profile">Profile</label>
            <select name="profile" id="profile" v-model="selectedProfileIndex">
                <option v-for="(p, index) in userProfiles" v-bind:key="p.profileId" :value="p.profileId">{{index + 1}}</option>
            </select>
            <button @click.prevent="addReading">Add</button>
        </form>
    </div>
</template>

<script>
import ReadingService from '../services/ReadingsService.js';
import ProfileInfoService from '../services/ProfileInfoService.js'

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
            },
            userProfiles: [],
            selectedProfileIndex: 0
        }
    },
    methods: {
        checkReading() {
            let profile = this.userProfiles[this.selectedProfileIndex - 1];
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
            this.newReading.profileId = +this.userProfiles[this.selectedProfileIndex - 1].profileId;
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
                    response;
                    this.checkReading();
                    this.$router.push({name: 'Profile'});
                })
                .catch(error => {
                    console.log(error);
                });
        }
    },
    created() {
        ProfileInfoService.getUserProfiles()
            .then((response) => {
                if (response.status === 200) {
                    this.userProfiles = response.data;
                }
                else {
                    console.log("Could not get user profiles");
                }
            })
            .catch((error) => {
                console.error("Couldn't find profiles", error);
            });
    }
}
</script>

<style>

</style>