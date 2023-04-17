<template>
  <v-btn
      :class="{
            'v-btn--gradient' : gradient,
            'v-btn--link': link,
            'v-btn--white': white,
        }"
      :color="link ? 'primary' : color"
      :ripple="link ? false : ripple"
      :rounded="rounded || gradient"
      :small="small"
      :text="text || link"
      class="ui-btn"
      depressed
      v-bind="{ ...$props, ...$attrs }"
      v-on="$listeners"

  >
    <template #loader>
      <slot name="loader"/>
    </template>
    <slot/>
  </v-btn>

</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'UiBtn',

  props: {
    color: {
      type: String,
      default: undefined,
    },
    ripple: {
      type: Boolean,
      default: undefined,
    },
    gradient: {
      type: Boolean,
      default: false,
    },
    text: {
      type: Boolean,
      default: false,
    },
    plain: {
      type: Boolean,
      default: false,
    },
    link: {
      type: Boolean,
      default: false,
    },
    rounded: {
      type: Boolean,
      default: false,
    },
    small: {
      type: Boolean,
      default: true,
    },
    white: {
      type: Boolean,
      default: false,
    },
  },

});
</script>

<style lang="scss">
.ui-btn {
  white-space: normal;

  &:not(.v-btn--icon) {
    height: auto !important;
    min-height: 32px;
  }

  &.v-btn--gradient {
    padding: 0 16px !important;

    &:not(.v-btn--disabled) {
      background: $gradient;
      color: #fff;
    }

    &:not(.v-btn--outlined) {
      .v-btn-toggle & {
        border: none !important;
        opacity: 1;
      }
    }

    &.v-btn--outlined {
      border-width: 2px;
      color: #733BD1;
      box-shadow: inset 0 0 0 100px #fff;

      &.v-btn--white {
        box-shadow: none;
        color: white;
        background: transparent;
      }
    }
  }

  &.v-btn--link {
    padding: 0 !important;
    height: unset !important;

    &:before {
      display: none;
    }

    &:hover {
      opacity: 0.72;
    }

    .v-icon--left {
      margin-left: 0;
    }
  }

  .v-btn__content {
    max-width: 100%;
  }

  &.v-btn--outlined.v-btn--rounded {
    border-width: 2px !important;
  }
}
</style>

