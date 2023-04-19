<template>
    <div class="addFormContainer">
        <h2>Calculate Bolus</h2>
        <form>
            <profile-select id="profileSelect" />
            <label for="carbAmount">Carb Amount</label>
            <input type="text" id='carbAmount' v-model="carbAmount"/>
            <label for="bloodSugar">Blood Sugar</label>
            <input type="text" id='bloodSugar' v-model="newReading.bloodSugar"/>
            <button id="calcBtn" @click.prevent="calculateBolus">Calculate</button>
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
            },
            carbAmount: '',
            sugarToLow: false,
        }
    },
    methods: {
        calculateBolus() {
           this.addReading();
        },
        checkReading() {
            this.sugarToLow = false;
            let profile = this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1];
            if (this.newReading.bloodSugar < profile.minWarningSugar) {
                this.activity.activityName = 'blood sugar alert: very low';
                ActivityService.addActivityToLog(this.activity);
                alert ("DANGEROUSLY LOW BLOOD SUGAR");
                this.sugarToLow = true;
            }
            else if (this.newReading.bloodSugar < profile.minBloodSugar) {
                this.activity.activityName = 'blood sugar alert: low';
                ActivityService.addActivityToLog(this.activity);
                alert("Low blood sugar");
                this.sugarToLow = true;
            }

            if (this.newReading.bloodSugar > profile.maxWarningSugar) {
                this.activity.activityName = 'blood sugar alert: very high';
                ActivityService.addActivityToLog(this.activity);
                alert ("DANGEROUSLY HIGH BLOOD SUGAR");
            }
            else if (this.newReading.bloodSugar > profile.maxBloodSugar) {
                 this.activity.activityName = 'blood sugar alert: high';
                ActivityService.addActivityToLog(this.activity);
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
                        console.log("Reading created");
                        this.showBolus();
                    }
                    else {
                        console.log("Error");
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        },
        showBolus() {
            let profile = this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1]; 
            if (profile != null)
            {
                if (this.sugarToLow) {
                    alert("Sugar to low for a bolus");
                } else {
                    let carbBolus = +this.carbAmount / profile.carbRatio;
                    let correctionBolus = 0; 

                    let bsMaxDifference = this.newReading.bloodSugar - profile.maxBloodSugar;
                    if (bsMaxDifference > 0) {
                        correctionBolus = bsMaxDifference / profile.correctionRatio; 
                    }

                    let totalBolus = carbBolus + correctionBolus;
                    alert("Suggested Bolus: " + totalBolus);
                    this.$router.push({name: 'Profile'});
                }
            }
        }
    },
    components: {
        ProfileSelect
    }
}
</script>

<style scoped src="../styles/addForm.css">
</style>