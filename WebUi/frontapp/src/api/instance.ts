import axios, {AxiosError} from 'axios';

import {responseErrorParser} from '@/helpers/utils';

import Snackbar from '@/plugins/snackbar/snackbar.vue';

const snackbar = Object.create(Snackbar.prototype);

const statusParser = (error: AxiosError) => {
    console.color('response error', 'red', error?.response);
    const errorData = responseErrorParser(error?.response);
    if (errorData.message) {
        snackbar.$snackbar.error(errorData.message);
    }
};

axios.interceptors.response.use(
    response => {
        //statusParser(response?.status, response?.data);
        return response;
    },
    error => {
        statusParser(error);
        return Promise.reject(error);
    }
);

export default axios;
