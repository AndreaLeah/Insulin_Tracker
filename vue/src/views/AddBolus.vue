<template>
    <div id="bolusCard">
        <h2>Calculate Bolus</h2>
        <form id="bolusForm">
            <profile-select id="profileSelect" />
            <label for="carbAmount"></label><input type="text" id='carbAmount' placeholder="Carb Amount" v-model="carbAmount"/>
            <label for="bloodSugar"></label><input type="text" id='bloodSugar' placeholder="Blood Sugar" v-model="newReading.bloodSugar"/>
            <button id="calcBtn" @click.prevent="calculateBolus">Calculate</button>
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
                alert ("DANGEROUSLY LOW BLOOD SUGAR");
                this.sugarToLow = true;
            }
            else if (this.newReading.bloodSugar < profile.minBloodSugar) {
                alert("Low blood sugar");
                this.sugarToLow = true;
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

<style scoped>

#bolusCard {
    display: flex;
    justify-self: center;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
    background-color: white;
    width: 300px;
    height: 250px;
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

#bolusCard > form > button {
  background-image: linear-gradient(to left, rgb(255, 142, 142), rgb(233, 64, 64));
  width: 200px;
  border-style: none;
  border-radius: 15px;
  margin: .5rem;
}
</style>