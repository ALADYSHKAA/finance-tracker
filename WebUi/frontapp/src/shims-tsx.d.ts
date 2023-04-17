/* tslint:disable */
/* eslint-disable */

import Vue, {VNode} from 'vue';

declare global {
    namespace JSX {
        // tslint:disable no-empty-interface
        interface Element extends VNode {
        }

        // tslint:disable no-empty-interface
        interface ElementClass extends Vue {
        }

        interface IntrinsicElements {
            [elem: string]: any
        }
    }

    type mutation = (state: any, args?: any) => any;
    type getter = (state: any, getters: any, rootState: any, rootGetters: any) => any;
    type action = (context: action_context, args) => any;

    interface mutations {
        [x: string]: mutation;
    }

    interface getters {
        [x: string]: getter;
    }

    interface is_root {
        root: true;
    }

    interface Vuex {
        [x: string]: any
    }

    interface action_context {
        state: any;
        getters: any;
        dispatch: any;
        rootState: any;
        rootGetters: any;

        commit(mutation_name: String, payload?: any, is_root?: is_root);
    }

    interface actions {
        [x: string]: action;
    }

    interface _t_store {
        namespaced?: Boolean;
        modules?: any;
        state?: any;
        mutations?: mutations;
        getters?: getters;
        actions?: actions;
    }
}
