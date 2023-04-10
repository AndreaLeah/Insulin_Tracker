import axios from 'axios';

export default {
    updateProfileInfo(){
        return axios.put('/profiles');
    },
    getProfile(profileId) {
        return axios.get('/profiles/', {params: { profileId: profileId }});
    }
}