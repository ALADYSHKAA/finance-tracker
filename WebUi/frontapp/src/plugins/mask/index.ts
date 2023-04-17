import Vue from 'vue';
import Inputmask from 'inputmask';
import {equalValues} from '@/helpers/utils';

Inputmask.extendAliases({
    numeric: {
        rightAlign: false,
        placeholder: '',
    },

    percent: {
        alias: 'numeric',
        allowMinus: false,
        digits: 2,
    },

    phoneMobile: {
        autoUnmask: true,
        mask: '+7 999 999 99 99',
        onUnMask(maskedValue, unmaskedValue) {
            return unmaskedValue.length === 10 ? `+7${unmaskedValue}` : unmaskedValue;
        },
    },

    price: {
        alias: 'numeric',
        allowMinus: false,
        autoUnmask: true,
        groupSeparator: ' ',
        suffix: ' ₽',
        digits: 2,
    },

    count: {
        alias: 'numeric',
        allowMinus: false,
        autoUnmask: true,
        groupSeparator: ' ',
        digits: 0,
    },
});

const bindInputmask = (el, binding, vnode) => {
    const $input = el.getElementsByTagName('input');
    try {
        Inputmask.remove($input as HTMLElement);
    } catch (e) { /**/
    }

    const onFocus = e => {
        const len = e.target.value.length;
        setTimeout(() => {
            if (e.target.setSelectionRange && len) {
                e.target.focus();
                e.target.setSelectionRange(len, len);
            }
        }, 10);
    };

    if ($input.length && binding.value !== undefined) {
        let options = {
            mask: undefined,
            showMaskOnHover: false,
            onBeforePaste: pastedValue => {
                vnode.componentInstance.lazyValue = pastedValue;
            },
            onincomplete: () => {
                vnode.componentInstance.lazyValue = '';
            },
        };
        if (typeof binding.value === 'string') {
            options.mask = binding.value;
        } else {
            options = {...options, ...binding.value};
        }
        Inputmask(options).mask($input as HTMLElement);

        //$input[0].addEventListener('focus', onFocus);
    }
};

const VueInputmask = {
    install: (V, options) => {
        V.directive('mask', {
            bind: bindInputmask,
            update: (el, binding, vnode, oldVnode) => {
                if (!equalValues(vnode.data.attrs.mask, oldVnode.data.attrs.mask)) {
                    bindInputmask(el, binding, vnode);
                }
            },
        });
    },
};

Vue.use(VueInputmask);
