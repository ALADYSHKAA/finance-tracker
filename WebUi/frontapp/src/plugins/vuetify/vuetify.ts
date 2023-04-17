import '@mdi/font/scss/materialdesignicons.scss';

import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import * as components from 'vuetify/lib/components';
import * as directives from 'vuetify/lib/directives';
import usedColors from '@/helpers/usedColors';
import {GlobalVuetifyPreset} from 'vuetify/types/services/presets';
import ru from 'vuetify/src/locale/ru';

Vue.use(Vuetify, {components, directives});

export default new Vuetify({
    icons: {
        iconfont: 'mdi',
        values: {},
    },
    lang: {
        locales: {ru},
        current: 'ru',
    },
    theme: {
        dark: false,
        options: {
            customProperties: true,
        },
        themes: {
            light: {
                ...usedColors,
            },
        },
    },
} as GlobalVuetifyPreset);
