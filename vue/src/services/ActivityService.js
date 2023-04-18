import axios from 'axios';

export default {
    getActivityLog() {
        return axios.get('/activity');
    },
    addActivityToLog(activity) {
        return axios.post('/activity', activity);
    }
}