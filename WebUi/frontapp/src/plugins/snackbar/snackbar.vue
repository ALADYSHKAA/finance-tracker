<template>
  <div class="layout-snackbars">
    <v-snackbar
        v-for="(bar, idx) in bars"
        :key="bar._id"
        :ref="`v-snackbar-${bar._id}`"
        v-model="bar._model"
        :style="calcPosition(idx, bar)"
        absolute
        class="ul-snackbar"
        min-width="0"
        v-bind="bar"
    >
      <template #default>
        <slot v-bind="bar"/>
      </template>
      <template v-if="bar.closeButton || bar.sideComponent" #action>
        <slot
            :close="() => close(bar._id)"
            :closeButton="bar.closeButton"
            name="action"
            v-bind="bar"
        >
          <UiBtn text @click="close(bar._id)">Закрыть</UiBtn>
        </slot>
      </template>
    </v-snackbar>
  </div>
</template>

<script lang="ts">

import usedColors from '@/helpers/usedColors';
import Vue, {Component} from 'vue';

export interface SnackbarOptions {
  message: string
  top?: boolean
  bottom?: boolean
  left?: boolean
  right?: boolean
  color?: string
  timeout?: null | number
  transition?: string,
  closeButton?: string,
  messageComponent?: Component,
  sideComponent?: Component,
  _id?: string
  _model?: boolean,
  _timer?: null | ReturnType<typeof setTimeout>,
  _height?: number,
}

const defaultOptions: SnackbarOptions = {
  message: '',
  color: usedColors.info,
  timeout: 6000,
  bottom: true,
  _id: undefined,
  _model: false,
  _timer: null,
  _height: 0,
};

type TSnackbarStatus = 'error' | 'success' | 'warning' | 'info'

declare module 'vue/types/vue' {
  interface Vue {
    $snackbar: Record<TSnackbarStatus | 'show', (text: string | SnackbarOptions, options?: SnackbarOptions) => void>
  }
}

export default Vue.extend({
  name: 'PluginSnackbars',
  props: {
    top: {
      type: Boolean,
      default: undefined,
    },
    gap: {
      type: Number,
      default: 8,
    },
  },
  data: () => ({
    bars: [],
  }),
  mounted() {
    Vue.use({
      install: V => {
        V.prototype.$snackbar = {
          error: (message, opts) => this.openAlias(message, opts, {color: usedColors.error}),
          success: (message, opts) => this.openAlias(message, opts, {color: '#12A54D'}),
          warning: (message, opts) => this.openAlias(message, opts, {color: usedColors.warning}),
          info: (message, opts) => this.openAlias(message, opts, {color: usedColors.accent}),
          show: this.open,
        };
      },
    });
  },
  methods: {
    openAlias(message, opts = {}, add = {}) {
      const options = typeof message === 'string' ? {message, ...opts, ...add} : {...message, ...opts, ...add};
      return this.open(options);
    },

    open(option = {}) {
      const object = {
        ...defaultOptions,
        _id: this.getRandomId(),
        bottom: this.bottom,
        ...option,
      };
      this.bars.push(object);
      this.$nextTick(() => {
        object._model = true;
        this.$nextTick(() => {
          object._timer = setTimeout(() => {
            this.remove(object._id);
          }, object.timeout);
          const $bar = this.$refs[`v-snackbar-${object._id}`][0].$el;
          object._height = $bar.querySelector('.v-snack__wrapper').clientHeight;
          $bar.addEventListener('transitionend', () => {
            object._height = $bar.querySelector('.v-snack__wrapper').clientHeight;
          }, {once: true});
        });
      });
      return {
        close: () => this.close(object._id),
      };
    },

    getRandomId() {
      return `${Math.floor((1 + Math.random()) * 0x10000).toString(16) + Date.now()}`;
    },

    close(id) {
      const idx = this.findIdxById(id);
      this.bars[idx]._model = false;
      this.$refs[`v-snackbar-${id}`][0].$el.addEventListener('transitionend', () => {
        this.remove(id);
      }, {once: true});
    },

    remove(id) {
      const idx = this.findIdxById(id);
      clearTimeout(this.bars[idx]._timer);
      this.bars = [...this.bars.slice(0, idx), ...this.bars.slice(idx + 1)];
    },

    findIdxById(id, arr = this.bars) {
      return arr.findIndex(i => i._id === id);
    },

    calcPosition(idx, bar) {
      let stack: string[] = [];
      stack = this.bars.filter((_, index) => index <= idx - 1).map(i => i._height + this.gap);
      const position = stack?.length ? stack.reduce((a, b) => a + b) : 0;
      return bar.top ? {top: `${position}px`} : {bottom: `${position}px`};
    },

  },
});
</script>

<style lang="scss">
.layout-snackbars {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  overflow: hidden;
  pointer-events: none;
  z-index: 9999;
}

.ul-snackbar {
  transition: all 0.3s ease;
  height: auto !important;
  pointer-events: none;

  .v-snack__wrapper {
    pointer-events: auto;
  }
}
</style>
