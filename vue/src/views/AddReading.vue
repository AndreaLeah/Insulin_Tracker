<template>
    <div>
        <h2>Add new reading</h2>
        <form>
            <label for="bloodSugar"></label><input type="text" id='bloodSugar' placeholder="Blood Sugar" v-model="newReading.bloodSugar"/>
            <label for="profileId"></label><input type="text" id='profileId' placeholder="Profile Id" v-model="newReading.profileId"/>
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
        }
    },
    methods: {
        checkReading() {
             ProfileInfoService.getProfile(this.newReading.profileId)
            .then((response) => {
                if (response.status === 200) {
                     if (this.newReading.bloodSugar < response.data.minBloodSugar) {
                        alert("LOW blood sugar");
                    }
                    if (this.newReading.bloodSugar > response.data.maxBloodSugar) {
                        alert("HIGH blood sugar");
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
        },
        addReading() {
            ProfileInfoService.getUserProfiles()
                .then((response) => {
                    if (response.status === 200) {
                    this.userProfiles = response.data;

                    this.newReading.bloodSugar = +this.newReading.bloodSugar;
                    this.newReading.profileId = +this.userProfiles[+this.newReading.profileId-1].profileId;
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
                })
                .catch((error) => {
                    console.error("Couldn't find profiles", error);
                });
        }
    },
    created() {
        console.log("hello");
    }
}
</script>

<style>

</style>