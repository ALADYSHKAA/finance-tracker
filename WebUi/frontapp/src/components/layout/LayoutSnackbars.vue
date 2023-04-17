<template>
    <Snackbar>
        <template #default="options">
            <component v-if="options.messageComponent" :is="options.messageComponent" v-bind="options"/>
            <div v-else class="d-flex align-center">
                <v-icon v-if="options.icon" class="mr-4">{{ options.icon }}</v-icon>
                <span class="caption" v-html="options.message"></span>
            </div>
        </template>
        <template #action="options">
            <component v-if="options.sideComponent" :is="options.sideComponent" v-bind="options"/>
            <UiBtn v-else class="mr-2" text @click="close">{{ options.closeButton }}</UiBtn>
        </template>
    </Snackbar>
</template>

<script lang="ts">
import Snackbar from '@/plugins/snackbar/snackbar.vue';

export default {
    name: 'LayoutSnackbars',
    components: {
        Snackbar,
    },
};
</script>
