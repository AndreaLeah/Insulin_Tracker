<template>
  <div class="home">
    <h1>Insulin Tracker</h1>
    <p>This application simulates that action of an insulin pump which delivers a continuous base level of insulin
       and then additional amounts based on food intake.</p>
      <p>
        Tracks blood sugar and insulin intake.
      </p>
      <router-link v-bind:to="{name: 'Profile'}">View Profile</router-link>
  </div>
</template>

<script>
export default {
  name: "home"
};
</script>
