<template>
<div id="main-content">
    <div id="div-content">
        <h1>Your Readings</h1>
        <div>
            <article>
                <label for="days">Days</label>
                <select name="days" id="days" v-model="selectedDays" @change="onDaysChange">
                    <option value="1">1 day</option>
                    <option value="3">3 day</option>
                    <option value="30">30 day</option>
                    <option value="90">90 day</option>
                    <option value="180">180 day</option>
                </select>
            </article>

            <profile-select :propSelectedIndex="+this.$route.params.index" @hook:mounted="profileSelectMounted" @hook:updated="onProfileChange"/>
        
        </div>
            
        <div>Average: {{readingsAverage}}</div>
        <div>Range: {{getMinRange}}-{{getMaxRange}}</div>
        <div id="chart">
            <apexcharts 
                width="100%"
                height="350"
                type="rangeArea"
                :options="chartOptions"
                :series="series">
            </apexcharts>
        </div>

        <div>
            <b-table responsive striped hover :items="readingsForTable" id="table"></b-table>
        </div>

    </div>
</div>
</template>

<script>
import ReadingsService from '../services/ReadingsService.js';
import ProfileSelect from '../components/ProfileSelect.vue';
import VueApexCharts from "vue-apexcharts";

export default {
    data() {
        return {
            timeframe: 30,
            selectedDays: 30,
            readings: [],
            readingsForTable: [],
            yAxis: [],
            areaChartXYValues: [],

            series: [
                {
                    type: 'line',
                    name: "Blood Sugar",
                    data: []
                },
                {
                    type: 'rangeArea',
                    name: "Profile Range",
                    data: [],
                }
            ],
            chartOptions: {
                chart: {
                    height: 350,
                    type: 'rangeArea',
                    animations: {
                        speed: 200
                    }
                },
                colors: ['#4ae1ff', '#7dff7d'],

                dataLabels: {
                    enabled: false
                },

                fill: {
                    opacity: [1, 0.25]},
                forecastDataPoints: {
                    count: 2
                },
                plotOptions: {
                    area: {
                        fillTo: origin
                    }
                },
                markers: {
                    size: [4, 0],
                },
                stroke: {
                    curve: 'smooth',
                    width: [2, 0]
                },
                xaxis: {
                    type: 'datetime'
                },
            },
        }
    },
    components: {
        apexcharts: VueApexCharts,
        ProfileSelect,
    },
    computed: {
        readingsAverage() {
            let sum = 0;
            for (let i = 0; i < this.readings.length; i++) {
                sum += this.readings[i].bloodSugar;
            }

            let result = sum / this.readings.length;
            return result.toFixed(2);
        },
        getMinRange() {
            if (this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1] != null) {
                return this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1].minBloodSugar;
            } else {
                return 0;
            }
        },
        getMaxRange() {
            if (this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1] != null) {
                return this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1].maxBloodSugar;
            } else {
                return 0;
            }
        }
    },
    methods: {
        convertActivityLog() {
            for (let i = 0; i < this.readings.length; i++) {
                //console.log(this.readings[i].bloodSugar);
                //console.log(this.timeReformat(this.readings[i]));

            // Push each item as object with formatted time
            this.readingsForTable.push(
                {
                    blood_sugar: this.readings[i].bloodSugar, 
                    time: this.timeReformat(this.readings[i])
                }
            );            
            }            
        },  
        timeReformat(reading){
            let dateArray = reading.time.split("T");
            
            let dateSplit = dateArray[0].split("-");
            let date = dateSplit[1]+"/"+dateSplit[2]+"/"+dateSplit[0];

            let timeSplit = dateArray[1].split(":");
            let time = timeSplit[0]+":"+timeSplit[1];

            date += " - " + time;
            return date;
        },
        readingsCall() {
            ReadingsService.getBSByTimeframe(this.timeframe)
            .then(response => {
                if (response.status === 200) {
                    this.readings = response.data;
                    // Reset readingsForTable
                    this.readingsForTable = [];
                    this.convertActivityLog();
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
                    type: 'line',
                    name: "Blood Sugar",
                    data: []
                },
                {
                    type: 'rangeArea',
                    name: "Profile Range",
                    data: [],
                }];

            // Get & Set Min&Max BS variables for rangeArea chart
            if (this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1] != null) {
                this.minBS = this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1].minBloodSugar;
                this.maxBS = this.$store.state.userProfiles[this.$store.state.selectedProfileIndex - 1].maxBloodSugar;
            } else {
                this.minBS = 0;
                this.maxBS = 0;
            }

            for (let i = 0; i < this.readings.length; i ++) {
                let time = Date.parse(this.readings[i].time)
                let bs = this.readings[i].bloodSugar;
                // Line chart
                this.series[0].data[i] = {x: time, y: bs};
                // Range area chart
                this.series[1].data[i] = {x: time, y: [this.minBS, this.maxBS]};
            }
        },
        onDaysChange() {
            this.timeframe = +this.selectedDays;
            this.readingsCall();
        },
        onProfileChange() {
            this.updateGraph();
        },
        profileSelectMounted() {
            this.$store.commit('SET_PROFILE_INDEX', +this.$route.params.index);
            this.readingsCall();
        }
    },
}
</script>

<style>

#main-content{
    height: 100%;
    width: 100%;
    padding-bottom: 1rem;
}

body{
    background-color: rgb(255, 245, 245);
}

h1{
    padding-bottom: 1rem;
}

#div-content{
    padding: 2rem;
    display: flex;
    flex-direction: column;
    width: 70vw;
    margin-bottom: 3rem;
    background-color: white;
    border: thin solid black;
    box-shadow: 5px 0px 5px rgba(0, 0, 0, 0.1);
    border-radius: 10px;
}

article{
    display: flex;
    display: inline;
    padding-right: 1rem;
}

article > label{
    padding-right: .4rem;
}

div{
    justify-items: center;
}

#th {
 border: none;
}

</style>