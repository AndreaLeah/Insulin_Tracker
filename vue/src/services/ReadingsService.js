import axios from 'axios';

export default {
    getUserReadings() {
        return axios.get('/readings');
    }
}