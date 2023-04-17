<!--suppress JSVoidFunctionReturnValueUsed -->
<template>
    <LayoutContainer>
        <template #head>
            <div class="text-h5 text-ellipsis">Добро пожаловать</div>
            <p> test welcome</p>
        </template>

        <v-container class="container--main pb-10 py-4 px-6 height-100" fluid>
            <v-row>
                <v-col cols="12" sm="6">
                    <span v-if="!user.id">Привет, неизвестный!</span>
                    <span v-else="!user.id">Я вас не узнал, {{ user.fullName}}!</span><br>
                    <span v-if="user.nickname">Или мне стоит называть вас "{{user.nickname}}"?</span>
                    <v-btn small v-if="!user.id" @click="fetchUser">Кто я?</v-btn>

                </v-col>
                <v-col xs="6">
                    <v-text-field label="Никнейм" v-model="name"></v-text-field>
                    <v-btn small @click="saveUserNickname">Set nickname</v-btn>
                </v-col>
                <v-col cols="12">
                    {{user}}
                </v-col>
            </v-row>
        </v-container>
    </LayoutContainer>
</template>

<script lang="ts">
import LayoutContainer                      from '@/components/layout/LayoutContainer.vue';
import {welcomeStorePath}                   from '@/views/welcome/welcomeStore';
import {mapFields}                          from 'vuex-map-fields';
import {mapActions, mapMutations, mapState} from 'vuex';
import Vue                                  from 'vue';

export default Vue.extend({
    name: 'WelcomePage',

    components: {
        LayoutContainer,

    },
    props: {
        recordIdProp: String || null,
    },
    data: () => ({}),

    computed: {
        ...mapFields(welcomeStorePath, ['name', 'helloCount']),
        ...mapState(welcomeStorePath, ['user']),
    },
    async created() {
    },
    methods: {
        ...mapActions(welcomeStorePath, ['fetchUser', 'saveUserNickname']),
        ...mapMutations(welcomeStorePath, ['RESET']),
    },
});
</script>

<style lang="scss">
.container--main {
  position: relative;
}
</style>
