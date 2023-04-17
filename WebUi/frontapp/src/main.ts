import '@fontsource/open-sans/400.css';
import '@fontsource/open-sans/400-italic.css';
import '@fontsource/open-sans/600.css';
import '@fontsource/open-sans/700.css';

import vuetify from './plugins/vuetify/vuetify';

import './styles/main.scss';
import '@/plugins/install';
import '@/components/install';

import Vue from 'vue';
import App from './App.vue';
import router from '@/router';
import store from './store/store';

Vue.config.productionTip = false;

if ('ontouchstart' in document.documentElement) {
    document.body.classList.add('is-touchable');
}

if (import.meta.env.VITE_APP_ENV === 'testing') {
    Vue.config.devtools = true;
}

Vue.mixin({
    computed: {
        $isMobile() {
            return !this.$vuetify.breakpoint.mdAndUp;
        },
    },
});

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App),
}).$mount('#app');
