import axios from 'axios';

export default {
    getUserReadings() {
        return axios.get('/readings');
    },
    addReading(reading) {
        return axios.post('/readings', reading);
    },
    // Timeframe object to include profileId & BS timeframe
    getBSByTimeframe(timeframe) {
        return axios.get('readings/history/' + timeframe);
    }
}