export const isValueCheck = (value: any): boolean => {
    if (value === undefined || value === null) return false;
    if (typeof value === 'number') return true;
    if (Array.isArray(value)) {
        return value.length > 0;
    }
    if (typeof value === 'string') {
        return value.trim().length > 0;
    }
    if (typeof value === 'object') {
        return Object.keys(value).length > 0;
    }
    return !!value;
};

export const highlightString = (text: string, query = '') => {
    if (!text) return '';
    if (!query) return text;
    const innerString = text.toString().toLowerCase().replace(/ё/g, 'е');
    const queryString = query.toString().toLowerCase().replace(/ё/g, 'е');
    const idx = innerString.indexOf(queryString);
    if (idx < 0) return text;
    const begin = text.toString().substr(0, idx);
    const highlight = text.toString().substr(idx, query.length);
    const end = text.toString().substr(idx + query.length);
    return `${begin}<span class="highlight" style="background: rgba(87, 68, 214, 0.2)">${highlight}</span>${end}`;
};

export const foundQueryString = (query: string, string: string) => {
    const Q = query.toString().trim().toLowerCase().replace(/ё/g, 'е');
    const S = string.toString().trim().toLowerCase().replace(/ё/g, 'е');
    return S.indexOf(Q) >= 0;
};

export const isObject = (value: any) => {
    return typeof value === 'object' && !Array.isArray(value) && value !== null;
};

export const isValueClean = (value: any) => {
    if (value === null) return true;
    if (Array.isArray(value)) {
        if (value.length === 0) return true;
        return !value.find(i => !isValueClean(i));
    }
    if (typeof value === 'boolean') return false;
    if (typeof value === 'number') return false;
    if (typeof value === 'string') return value.trim() === '';
    if (typeof value === 'object') {
        if (Object.keys(value).length === 0) return true;
        return !Object.values(value).find(i => !isValueClean(i));
    }
    return !value;
};

export const clearObject = (value: any) => {
    if (isObject(value)) {
        const clearedObject = {};
        Object.keys(value).forEach(key => {
            if (!isValueClean(value[key])) {
                clearedObject[key] = clearObject(value[key]);
            }
        });
        return clearedObject;
    } else if (Array.isArray(value)) {
        return value.map(v => clearObject(v));
    }
    return value;
};

export const equalValues = (v1: any, v2: any, ignoreStringNumberType = true, debug = false) => {
    if (typeof v1 === 'string' && typeof v2 === 'string') {
        return v1.trim().toLowerCase() === v2.trim().toLowerCase();
    }
    let value1 = JSON.stringify(clearObject(v1));
    let value2 = JSON.stringify(clearObject(v2));
    if (ignoreStringNumberType && value1 !== undefined && value2 !== undefined) {
        value1 = value1.replace(/\"/ig, '');
        value2 = value2.replace(/\"/ig, '');
    }
    if (debug) {
        console.info(value1);
        console.info(value2);
    }
    return value1 === value2;
};

export const copyValue = value => {
    if (value === undefined) return value;
    return JSON.parse(JSON.stringify(value));
};

export const removeArrayItem = (arr: any[], idx: number, newItem?: any) => {
    if (Array.isArray(arr) && idx >= 0) {
        const item = newItem !== undefined ? [newItem] : [];
        return [...arr.slice(0, idx), ...item, ...arr.slice(idx + 1)];
    }
    return arr;
};

export const responseErrorParser = error => {
    if (!error) return null;
    const response = error?.data || error?.response;
    const message = typeof response === 'string' ? response : response?.error || response?.title || response?.message || 'Ошибка запроса';

    return {
        message: message.replaceAll('\r', '<br>'),
        status: error.status,
        instance: error,
    };
};
