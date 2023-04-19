<template>
  <div id="main-content">
      <team-member/>
  </div>
</template>

<script>
import TeamMember from "@/components/TeamMember.vue"

export default {
    name: "about",
    components: {
        TeamMember
    }
}
</script>

<style scoped>

#main-content{
    height: 100vh;
    width: 100%;
}

</style>