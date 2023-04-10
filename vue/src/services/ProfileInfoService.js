import axios from 'axios';

export default {
    addProfileInfo(profile){
        return axios.post('profiles', profile);
    },
    updateProfileInfo(){
        return axios.put('/profiles');
    },
    getProfile(profileId) {
        return axios.get('/profiles/' + profileId);
    },
    getUserProfiles() {
        return axios.get('/profiles');
    }
}