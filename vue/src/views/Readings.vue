<template>
    <div>
        <h1>Your Readings</h1>
        <table>
            <thead>
                <th>User Id</th>
                <th>ProfileId</th>
                <th>ReadingId</th>
                <th>Blood Sugar</th>
                <th>Time</th>
            </thead>
            <tr v-for="reading in readings" v-bind:key="reading.readingId">
                <td>{{reading.userId}}</td>
                <td>{{reading.profileId}}</td>
                <td>{{reading.readingId}}</td> 
                <td>{{reading.bloodSugar}}</td> 
                <td>{{reading.time}}</td>
            </tr>
        </table>
    </div>
</template>

<script>
import ReadingsService from '../services/ReadingsService.js';
export default {
    data() {
        return {
            readings: []
        }
    },
    methods: {
        getUserReadings() {
            ReadingsService.getUserReadings()
            .then((response) => {
                if (response.status === 200) {
                    this.readings = response.data;
                    console.log(this.readings);
                }
            })
            .catch((error) => {
                console.error("Couldn't update profileeeee", error);
            });
        }
    },
    created() {
        this.getUserReadings();
    }
}
</script>

<style>

</style>