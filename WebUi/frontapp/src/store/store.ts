import Vue from 'vue';
import Vuex, {Module} from 'vuex';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {},
});

export const registerStoreModule = (storeName: string, storeModule: Module<any, any>) => {
    if (!store.hasModule(storeName)) {
        store.registerModule(storeName, storeModule);
    }
};

export default store;
