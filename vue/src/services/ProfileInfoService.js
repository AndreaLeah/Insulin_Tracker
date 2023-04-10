import axios from 'axios';

export default {
    addProfileInfo(profile){
        return axios.post('profiles', profile);
    }
}