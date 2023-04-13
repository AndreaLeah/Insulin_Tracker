<template>
    <div>
        <h1>Your Readings</h1>
        <section>LINE GRAPH</section>
        <table>
            <thead>
                <th>User Id</th>
                <th>ProfileId</th>
                <th>ReadingId</th>
                <th>Blood Sugar</th>
                <th>Carbs</th>
                <th>Time</th>
            </thead>
            <tr v-for="reading in readings" v-bind:key="reading.readingId">
                <td>{{reading.userId}}</td>
                <td>{{reading.profileId}}</td>
                <td>{{reading.readingId}}</td> 
                <td>{{reading.bloodSugar}}</td> 
                <td>{{reading.carbs}}</td>
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
                }
            })
            .catch((error) => {
                console.error("Couldn't update profile", error);
            });
        }
    },
    created() {
        this.getUserReadings();
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