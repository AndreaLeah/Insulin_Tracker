import axios from 'axios';

export default {
    updateProfileInfo(){
        return axios.put('profiles');
    }
}