<template>
    <v-app>
        <v-main>
            <transition name="fade">
                <LayoutPreload v-if="loading"/>
                <router-view v-else/>
            </transition>
        </v-main>
        <LayoutSnackbars/>
    </v-app>
</template>

<script lang="ts">
import LayoutPreload      from '@/components/layout/LayoutPreload.vue';
import LayoutSnackbars    from '@/components/layout/LayoutSnackbars.vue';

export default {
    name: 'App',
    components: {
        LayoutPreload,
        LayoutSnackbars,
    },
    data() {
        return {
            loading: true,
        };
    },
    computed: {},

    async created() {
        this.loading = true;
        try {
            setTimeout(() => true, 1000);
        } finally {
            this.loading = false;
        }
    },
    methods: {
    },
};
</script>

<style lang="scss">
.v-application {
  overflow: hidden;
}

.v-dialog {
  background: #fff;
}

.theme--light.v-label {
  color: map-get($grey, 'lighten-1');
}

.v-list-item__mask {
  background: rgba($primary, .2) !important;
}

.LayoutFeedbackBtn {
  position: fixed;
  right: 16px;
  bottom: 16px;
}
</style>
