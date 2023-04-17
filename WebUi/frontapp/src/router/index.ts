import store from '@/store/store';
import Vue from 'vue';
import Router from 'vue-router';
import routes from '@/router/routes';

Vue.use(Router);

const router = new Router({
    mode: 'history',
    base: import.meta.env.BASE_URL,
    routes,
});

router.beforeEach(async (toRote, from, next) => {
    if (toRote.meta.hasOwnProperty('preLoadStateActions')) {
        const actions = toRote.meta['preLoadStateActions'];
        for (const actionDescription of actions) {
            const action = actionDescription.join('/');
            await store.dispatch(action);
        }
        return next();
    } else {
        return next();
    }
});

router.afterEach((to, from) => {
    document.title = to.meta?.title || 'Finance Tracker';
});

const originalPush = Router.prototype.push;
Router.prototype.push = function push(location) {
    return originalPush.call(this, location).catch(err => err);
};

export default router;
