import axios from 'axios';

export default {
    getActivityLog() {
        return axios.get('/activity');
    },
    addActivityToLog() {
        return axios.post('/activity');
    }
}