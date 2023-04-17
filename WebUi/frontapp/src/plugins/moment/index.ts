import Vue from 'vue';
import moment from 'moment';
import ru from './locale';

moment.locale('ru', ru);

Vue.use({
    install: V => {
        V.prototype.$moment = moment;
    },
});

declare module 'vue/types/vue' {
    interface Vue {
        $moment: ReturnType<typeof moment>
    }
}

window.moment = moment;

export default moment;
