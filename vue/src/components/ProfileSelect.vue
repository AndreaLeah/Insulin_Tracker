<template>
    <select name="profile" id="profile" v-model="selectProfileIndex">
        <option v-for="(p, index) in userProfiles" v-bind:key="p.profileId" :value="p.profileId">{{index + 1}}</option>
    </select>
</template>

<script>
import ProfileInfoService from '../services/ProfileInfoService.js'

export default {
    data() {
        return {
            userProfiles: [],
            selectProfileIndex: 0
        }
    },
    methods: {
        setSelectedProfileIndex() {
            this.$store.commit("SET_PROFILE_INDEX", this.selectProfileIndex);
        },
        setLoadedUserProfiles() {
            this.$store.commit("SET_USER_PROFILES", this.userProfiles);
        }
    },
    created() {
        console.log("Loading user profiles");
        ProfileInfoService.getUserProfiles()
            .then((response) => {
                if (response.status === 200) {
                    this.userProfiles = response.data;
                    this.selectProfileIndex = 1;
                    this.setSelectedProfileIndex();
                    this.setLoadedUserProfiles();
                }
                else {
                    console.log("Could not get user profiles");
                }
            })
            .catch((error) => {
                console.error("Couldn't find profiles", error);
            });
    }
}
</script>

<style>

</style>