<template>
  <section>
      <h1>Activity Log</h1>
    <div v-for="(activity, index) in activityLog" v-bind:key="index">
        <div class="rendered-info">
            <div class="activity">
                <p>Activity Name: {{ activity.activityName }}</p>
            </div>
            <div class="date">
                <p> Date Created: {{ activity.time }}</p>
            </div>
        </div>
    </div>
  </section>
</template>

<script>
import ActivityService from '../services/ActivityService'

export default {
    name: "activity",
    data() {
        return {
            activityLog: []
        }
    },
    methods: {
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

<style>

section {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.rendered-info {
    display: flex;
}

</style>