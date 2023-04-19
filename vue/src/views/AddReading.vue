<template>
    <div class="addFormContainer">
        <h2>Add new reading</h2>
        <form>
            <profile-select/>
            <label for="bloodSugar">Blood Sugar</label>
            <input type="text" id='bloodSugar' v-model="newReading.bloodSugar"/>
            <button @click.prevent="addReading">Add</button>
        </form>
    </div>
</template>

<script>
import ReadingService from '../services/ReadingsService.js';
import ProfileSelect from '../components/ProfileSelect.vue';
import ActivityService from '../services/ActivityService.js';

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
            activity: {
                logId: 0,
                userId: 0,
                activityName: '',
                time: '2000-01-01T00:00:00.000'
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
                this.activity.activityName = 'blood sugar alert: very low'
                ActivityService.addActivityToLog(this.activity)
                alert ("DANGEROUSLY LOW BLOOD SUGAR")
            }
            else if (this.newReading.bloodSugar < profile.minBloodSugar) {
                this.activity.activityName = 'blood sugar alert: low'
                ActivityService.addActivityToLog(this.activity)
                alert("Low blood sugar");
            }

            if (this.newReading.bloodSugar > profile.maxWarningSugar) {
                this.activity.activityName = 'blood sugar alert: very high'
                ActivityService.addActivityToLog(this.activity)
                alert ("DANGEROUSLY HIGH BLOOD SUGAR")
            }
            else if (this.newReading.bloodSugar > profile.maxBloodSugar) {
                this.activity.activityName = 'blood sugar alert: high'
                ActivityService.addActivityToLog(this.activity)
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
                        this.activity.activityName = 'blood sugar reading added'
                        ActivityService.addActivityToLog(this.activity);
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

<style scoped src="../styles/addForm.css">
</style>