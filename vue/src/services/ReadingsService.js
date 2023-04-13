import axios from 'axios';

export default {
    getUserReadings() {
        return axios.get('/readings');
    },
    addReading(reading) {
        return axios.post('/readings', reading);
    }
}