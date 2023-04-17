<template>
    <div :class="{'layout-container--side' : $slots.side}" class="layout-container">
        <div class="layout-container-main">
            <div class="layout-container-main__inner">
                <div class="layout-container-head">
                    <router-link class="layout-container-head__logo" to="/">
                        <img width="40px" :src="require('../../assets/img/FiT.svg')" alt="">
                    </router-link>
                    <div class="layout-container-head__content">
                        <slot name="head"></slot>
                    </div>
                </div>
                <div class="layout-container-body">
                    <slot></slot>
                </div>
            </div>
        </div>
        <div v-if="$slots.side" class="layout-container-side">
            <div class="layout-container-side__inner">
                <slot name="side"></slot>
            </div>
        </div>
    </div>
</template>

<script lang="ts">

export default {
    name: 'LayoutWelcome',
};
</script>

<style lang="scss">
.layout-container {
  height: 100%;
  width: 100%;
  box-sizing: border-box;

  @include breakPoint(0, $breakpointMedium) {
    padding-top: 62px;
  }

  &--side {
    display: flex;
    flex-direction: column;

    @include breakPoint($breakpointMedium) {
      display: flex;
      flex-direction: row;
    }
  }

  &-head {
    height: 62px;
    display: flex;
    background: #fff;
    align-items: center;

    @include breakPoint(0, $breakpointMedium) {
      position: fixed;
      top: 0;
      left: 0;
      right: 0;
      z-index: 3;
    }

    &__logo {
      min-width: 51px;
      width: 51px;
      background: $gradient;
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
    }

    &__content {
      position: relative;
      padding: 4px 16px;
      flex: 1 1 0;
      min-width: 0;
    }
  }

  &-body {
    flex: 1 1 0;
    min-height: 0;
    min-width: 0;
    overflow: auto;

    &__inner {
    }
  }

  &-main {
    min-height: 100%;
    display: flex;
    flex: 1 1 auto;

    .layout-container--side & {
      min-height: 0;
      display: block;
    }

    @include breakPoint($breakpointMedium) {

    }

    &__inner {
      flex: 1 1 auto;
      display: flex;
      flex-direction: column;

      .layout-container--side & {
        display: block;
      }

      @include breakPoint($breakpointMedium) {

        .layout-container--side & {
          min-width: (100% * 7 / 12);
          max-width: (100% * 7 / 12);
        }
      }
    }
  }

  &-side {
    width: 100%;
    background: rgba($primary, 0.1);
    flex: 1 1 0;

    @include breakPoint($breakpointMedium) {
      min-width: (100% * 5 / 12);
      max-width: (100% * 5 / 12);
    }

    @include breakPoint($breakpointLarge) {

    }

    &__inner {
      height: 100%;

      @include breakPoint($breakpointMedium) {
        margin-right: -12px;
        margin-left: 12px;
      }
    }
  }
}
</style>
