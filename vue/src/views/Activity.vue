<template>
<div id="main-content">
    <section>
        <div class="bin">
            <h1>Activity Log</h1>
            <div>
                <b-table responsive hover                 
                :items="convertedActivityLog"
                :fields="fields"
                :head-variant="headVariant"
                :borderless="borderless"
                :striped="striped" outlined
                class="table"></b-table>
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
            convertedActivityLog: [],
            fields: ['activity_name', 'time'],
            headVariant: 'light',
            borderless: true
        }
    },
    methods: {
        formatTime(stringTime) {
            let string = stringTime;
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

            return `${timeObj.hours}:${timeObj.minutes} - ${timeObj.month}/${timeObj.day}/${timeObj.year}`;
        },
        getActivityLog() {
            ActivityService.getActivityLog()
            .then((response) => {
                if (response.status === 200) {
                    this.activityLog = response.data;
                    this.convertActivityLog();
                }
            })
            .catch((error) => {
                console.error("Couldn't find activities to log", error);
            });
        },
        convertActivityLog() {
            for (let i = 0; i < this.activityLog.length; i++) {
                //console.log(this.activityLog[i].activityName);
                //console.log(this.formatTime(this.activityLog[i].time));

                // Push each item as object with formatted time
                this.convertedActivityLog.push(
                    {
                        activity_name: this.activityLog[i].activityName, 
                        time: this.formatTime(this.activityLog[i].time)
                    }
                );
            }            
        }
    },
    created() {
        this.getActivityLog();
    }
}
</script>

<style scoped>

section {
    height: auto;
    width: 40rem;
}

.bin {
    width: 90%;
}

#main-content{
    height: 100vh;
    width: 100%;
    padding-bottom: 1rem;
}

section {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 1rem;
    background-color: white;
    border-radius: 10px;
    border: thin solid rgb(201, 201, 201);
    box-shadow: 5px 5px 5px rgba(0, 0, 0, .15);
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

.table-responsive {
    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.2);
}

</style>