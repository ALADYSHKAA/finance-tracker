import './moment';
import './mask';

window.console['color'] = function (message: string, color = 'green', ...args) {
    const text = typeof message === 'string' ? message : JSON.stringify(message);
    return this.log(`%c ${text} `, `font:12px/18px monospace; color:white;background-color:${color};border-radius:3px;white-space: pre-line;word-break: break-all;`, ...args);
};

declare global {
    interface Console {
        color(message: any, color?: string, arguments?: any): undefined
    }
}
