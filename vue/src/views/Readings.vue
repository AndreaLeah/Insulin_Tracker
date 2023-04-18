<template>
    <div id="div-content">
        <h1>Your Readings</h1>
        <div>
            <section>
                <label for="days">Days</label>
                <select name="days" id="days" v-model="selectedDays" @change="onDaysChange">
                    <option value="1">1 day</option>
                    <option value="3">3 day</option>
                    <option value="30">30 day</option>
                    <option value="90">90 day</option>
                    <option value="180">180 day</option>
                </select>
            </section>

            <profile-select @hook:mounted="profileSelectMounted" @hook:updated="onProfileChange"/>
        
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

        <table>
            <thead>
                <th>Blood Sugar</th>
                <th>Time</th>
            </thead>
            <tr v-for="reading in readings" v-bind:key="reading.readingId">
                <td>{{reading.bloodSugar}}</td> 
                <td>{{reading.time}}</td>
            </tr>
        </table>

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
        readingsCall() {
            ReadingsService.getBSByTimeframe(this.timeframe)
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
            this.readingsCall();
        }
    },
}
</script>

<style>

body{
    background-color: rgb(255, 245, 245);
}

h1{
    padding-bottom: 1rem;
}

#div-content{
    padding: 0 3rem;
    display: flex;
    flex-direction: column;
    width: 70vw;
}

section{
    display: inline;
    padding-right: 1rem;
}

section > label{
    padding-right: .4rem;
}

div{
    justify-items: center;
}

th, td{
    padding: 10px;
    border: thin solid black;
    background-color: white;
}

</style>