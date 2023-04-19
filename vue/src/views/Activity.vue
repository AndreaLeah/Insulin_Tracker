<template>
    <div id="main-content">
        <section>
            <h1>Activity Log</h1>
            <div class="rendered-info">
                <div class="label-container">
                    <div class="activity-name">Activity Name: </div>
                    <div>Date Created: </div>
                </div>
                <div class="the-mother">        
                <div v-for="activity in activityLog" v-bind:key="activity.logId" class="containerr">
                    <!-- <div class="containerr"> -->
                    <div class="activity">                        
                        <div class="activity-name">                            
                            <p>{{ activity.activityName }}</p>
                        </div>
                        <div class="activity-date">
                            <p>{{ formatTime(activity.time) }}</p>
                        </div>
                    <!-- </div> -->
                    </div>
                </div>
                </div>
            </div>
        </section>
    </div>
</template>

<script>
import ActivityService from '../services/ActivityService'

export default {
    name: "activity",
    data() {
        return {
            activityLog: [],
        }
    },
    methods: {
        formatTime(stringTime) {
            let string = stringTime;
            //console.log(string);
            let separatedArray = string.split("T");

            // Get array of [YYYY, MM, DD]
            let dateArray = separatedArray[0].split("-");

            // Get year, month & day & set them to time object
            let timeObj = {
                year: 0,
                month: 0,
                day: 0,
                hours: 0,
                minutes: 0
            };

            timeObj.year = dateArray[0];
            timeObj.month = dateArray[1];
            timeObj.day = dateArray[2];

            // Get array of [hour, minute]
            let timeArray = separatedArray[1].split(":");

            // Get hours & minutes & set them to time object
            timeObj.hours = timeArray[0];
            timeObj.minutes = timeArray[1];

            return `${timeObj.hours}:${timeObj.minutes} on ${timeObj.month}/${timeObj.day}/${timeObj.year}`;
        },
        getActivityLog() {
            ActivityService.getActivityLog()
            .then((response) => {
                if (response.status === 200) {
                    this.activityLog = response.data;
                }
            })
            .catch((error) => {
                console.error("Couldn't find activities to log", error);
            });
        }
    },
    created() {
        this.getActivityLog();
    }
}
</script>

<style scoped>

#main-content{
    height: 100vh;
    width: 100%;
}

section {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 1rem;
    background-color: white;
    border: thin solid black;
    box-shadow: 5px 0px 5px rgba(0, 0, 0, 0.1);
    border-radius: 10px;
}

.rendered-info {
    display: flex;
    flex-direction: column;
}

.the-mother {
    display: flex;
    flex-direction: column;
}

.activity {
    display: flex;
    column-gap: 20px;
}
.containerr {
    display: flex;
    flex-direction: column;
    padding: 0;
}


.activity-name {    
    width: 200px;
}

.label-container {
    display: flex;
    justify-content: flex-start;
    font-weight: bold;
    column-gap: 20px;
}

p {
    margin: 0;
}
</style>