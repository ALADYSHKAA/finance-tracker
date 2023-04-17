import Vue from 'vue';
import UiBtn from './ui/UiBtn.vue';

const components = {
    UiBtn,
};

Object.keys(components).forEach(name => {
    Vue.component(name, components[name]);
});
