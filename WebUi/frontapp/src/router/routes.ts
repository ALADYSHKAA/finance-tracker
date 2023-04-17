import {RouteConfig} from 'vue-router';
import {financeTrackerRouteNames} from '@/router/financeTrackerRouteNames';

export default [
    {
        path: '/:id?',
        //props    : route => ({recordIdProp : route.params['id']}),
        name: financeTrackerRouteNames.Welcome,
        component: () => import('@/views/welcome/WelcomePage.vue').then(m => m.default),
    },
] as RouteConfig[];
