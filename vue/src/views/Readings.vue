<template>
    <div>
        <h1>Your Readings</h1>
        <div>
            <label for="days">Days</label>
            <select name="days" id="days" v-model="selectedDays" @change="onDaysChange">
                <option value="1">1 day</option>
                <option value="3">3 day</option>
                <option value="30">30 day</option>
                <option value="90">90 day</option>
                <option value="180">180 day</option>
            </select>
            
            <label for="profile">Profile</label>
            <select name="profile" id="profile" v-model="selectedProfile" @change="onProfileChange">
                <option v-for="(p, index) in userProfiles" v-bind:key="p.profileId" :value="p.profileId">{{index + 1}}</option>
            </select>
        </div>
        <div>Average: {{readingsAverage()}}</div>
        <section>LINE GRAPH</section>
        <div id="chart">
            <apexcharts 
                width="100%"
                height="350"
                type="line"
                :options="chartOptions"
                :series="series">
            </apexcharts>
        </div>
        <table>
            <thead>
                <th>Blood Sugar</th>
                <th>Carbs</th>
                <th>Time</th>
            </thead>
            <tr v-for="reading in readings" v-bind:key="reading.readingId">
                <td>{{reading.bloodSugar}}</td> 
                <td>{{reading.carbs}}</td>
                <td>{{reading.time}}</td>
            </tr>
        </table>
    </div>
</template>

<script>
import ReadingsService from '../services/ReadingsService.js';
import ProfileInfoService from '../services/ProfileInfoService.js'

import VueApexCharts from "vue-apexcharts";


export default {
    data() {
        return {
            readings: [],
            userProfiles: [],
            selectedProfile: 0,
            selectedDays: 0,
            timeFrameObj: {
                timeFrame: 0,
                profileId: 0
            },

            chartOptions: {
                chart: {
                    id: "basic-line",
                    //stacked: true,
                    animations: {
                        speed: 200
                    }
                },
                dataLabels: {
                    enabled: false
                },
                plotOptions: {
                    area: {
                        fillTo: origin
                    }
                },
                markers: {
                    size: 4,
                },
                stroke: {
                    curve: 'smooth'
                },
                xaxis: {
                    categories: [],
                    convertedCatToNumeric: false
                }
            },
            series: [
                {
                    name: "Blood Sugar",
                    data: []
                }
            ],
        }
    },
    components: {
        apexcharts: VueApexCharts,
    },
    methods: {
        readingsAverage() {
            let sum = 0;
            for (let i = 0; i < this.readings.length; i++) {
                sum += this.readings[i].bloodSugar;
            }
            return sum / this.readings.length;
        },
        getUserReadings() {
            ReadingsService.getUserReadings()
            .then((response) => {
                if (response.status === 200) {
                    this.readings = response.data;
                }
            })
            .catch((error) => {
                console.error("Couldn't find readings", error);
            });
        },
        getUserProfiles() {
            ProfileInfoService.getUserProfiles()
            .then((response) => {
                if (response.status === 200) {
                    this.userProfiles = response.data;
                }
            })
            .catch((error) => {
                console.error("Couldn't find profiles", error);
            });
        },
        readingsCall() {
            if(this.timeFrameObj.profileId == 0 || this.timeFrameObj.timeFrame == 0) {
                return;
            } 

            ReadingsService.getBSByTimeframe(this.timeFrameObj)
            .then(response => {
                if (response.status === 200) {
                    this.readings = response.data;
                    this.updateGraph();
                }
            })
            .catch((error) => {
                console.error("Couldn't find readings", error);
            });
        },
        updateGraph() {
            this.chartOptions.xaxis.convertedCatToNumeric = false;
            this.chartOptions.xaxis.categories = [];
            this.series = [{
                name: "Blood Sugar",
                data: []
            }];

            for (let i = 0; i < this.readings.length; i ++) {
                let time = this.readings[i].time.split('T')[1];
                this.chartOptions.xaxis.categories[i] = time;
                this.series[0].data[i] = this.readings[i].bloodSugar;
            }
        },
        onProfileChange() {
            this.timeFrameObj.profileId = this.selectedProfile;
            this.readingsCall();
        },
        onDaysChange() {
            this.timeFrameObj.timeFrame = +this.selectedDays;
            this.readingsCall();
        }
    },
    created() {
        this.getUserReadings();
        this.getUserProfiles();
        this.updateGraph();
    }
}
</script>

<style>

div{
    justify-items: center;
}

section{
    text-align: center;
    padding: 5rem;
    margin: 2rem 0;
    border: thin solid black;
    border-radius: 10px;
}

th, td{
    padding: 10px;
    border: thin solid black;
}

</style>