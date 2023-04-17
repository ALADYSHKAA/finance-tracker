<template functional>
    <v-container class="text-center layout-preload fluid pa-0">
        <div class="layout-preload__logo">
            <img :src="require('../../assets/img/FiT.svg')" alt="Загрузка">
        </div>
        <div class="layout-preload__text body-2 mt-7">
            <v-progress-circular class="mr-2" color="grey darken-4" indeterminate size="18" width="2"/>
            Подождите немного...
        </div>
    </v-container>
</template>

<script>

export default {
    name: 'LayoutPreload',
};
</script>

<style lang="scss">
.layout-preload {
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;

  &__logo {
    min-height: 50%;
    background: #f00;
    width: 122px;
    display: flex;
    align-items: flex-end;
    background: linear-gradient(67.64deg, #2954DF 28.69%, #733BD1 82.18%);

    img {
      display: block;
      width: 100%;
    }
  }

  &__text {
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    text-align: center;
  }
}
</style>
