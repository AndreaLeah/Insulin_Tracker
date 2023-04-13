<template>
    <div>
        <h2>Add new reading</h2>
        <form>
            <label for="bloodSugar"></label><input type="text" id='bloodSugar' placeholder="Blood Sugar" v-model="newReading.bloodSugar"/>
            <label for="profileId"></label><input type="text" id='profileId' placeholder="Profile Id" v-model="newReading.profileId"/>
            <button @click.prevent="addReading">Add</button>
        </form>
    </div>
</template>

<script>
import ReadingService from '../services/ReadingsService.js';
export default {
    data() {
        return {
            newReading: {
                readingId: 0,
                userId: 0,
                profileId: '',
                bloodSugar: '',
                time: ''
            }
        }
    },
    methods: {
        addReading() {
            this.newReading.bloodSugar = +this.newReading.bloodSugar;
            this.newReading.profileId = +this.newReading.profileId;

            let d = new Date();

            let dateTime = d.getFullYear() + '-' + d.getMonth() + '-' + d.getDate() + 
                            'T' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + '.' + d.getMilliseconds();

            this.newReading.time = dateTime;

            ReadingService.addReading(this.newReading)
                .then(response => {
                    console.log(response);
                    this.$router.push({name: 'Profile'});
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }
}
</script>

<style>

</style>