import {getField, updateField} from 'vuex-map-fields';
import clients                 from '@/api/clients';
import {EditUserCmd, UserVm}   from '@financeTracker';

import {Module}                from 'vuex';
import {registerStoreModule}   from '@/store/store';

export const welcomeStorePath = 'welcomeStore';

interface IWelcomeStore {
    name: string
    helloCount: number
    user: UserVm
}

export const welcomeStore: Module<IWelcomeStore, any> = {
    namespaced: true,

    state: {
        name: 'Noone',
        helloCount: 0,
        user: new UserVm({
            id: undefined,
            nickname: undefined,
            disabled: true,
            fullName: '',
        }),

    },
    mutations: {
        updateField,

        RESET(state) {
            state.name       = 'Noone';
            state.helloCount = 0;
            state.user       = new UserVm({
                id: undefined,
                nickname: undefined,
                disabled: true,
                fullName: '',
            });
        },

    },

    actions: {
        async fetchUser({commit}) {
            const value = await clients.UsersClient.getCurrent();

            commit('updateField', {
                path: 'user',
                value,
            });
        },

        async saveUserNickname({state, commit, dispatch}) {
            //const reg = await clients.UsersClient.createRegistrationRecord(new CreateRegistrationRecordCmd(state.user));
            commit('updateField', {
                path: 'user.nickname',
                value: state.name,
            });
            const cmd   = new EditUserCmd({...state.user});
            const value = await clients.UsersClient.editUserCmd(cmd);

            await dispatch('fetchUser');
        },

    },

    getters: {
        getField,

    },
};

registerStoreModule(welcomeStorePath, welcomeStore);
