import axios from 'axios';

export default {
    getActivityLog() {
        return axios.get('/activity');
    }
}