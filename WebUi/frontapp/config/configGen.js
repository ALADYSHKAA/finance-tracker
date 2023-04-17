// Автоматическое добавление алиасов в файлы jsconfig.json и tsconfig.json.
// Скрипт сначала считывает текущие конфиги,добавляет в них алиасы, потом записывает в них,
// данные конфигов при этом не теряются

const path = require('path');
const fs = require('fs');
// Алиасы добавляем сюда
const paths = [
    {alias: '@', path: 'src'},
    {alias: '@enumDicts', path: '@enumDicts.ts'},
    {alias: '@financeTracker', path: '@/../financeTracker.ts'},
    {alias: 'vue', path: 'node_modules/vue/dist/vue.esm.js'}
];

const generate_vue_paths = () => {
    return paths.reduce((acc, val) => {
        acc[val.alias] = path.resolve(__dirname + '../../', val.path);
        return acc;
    }, {});
};

const generate_vite_paths = () => {
    return paths.map((val) => {
        return {
            find: val.alias,
            replacement: path.resolve(__dirname + '../../', val.path),
        };
    });
};

const generate_config_paths = () => {
    return paths.reduce((acc, val) => {
        if (val.path.endsWith('.js') || val.path.endsWith('.ts')) {
            acc[val.alias] = [val.path];
        } else {
            acc[val.alias + '/*'] = [val.path + '/*'];
        }
        return acc;
    }, {});
};
const read_config = (configPath) => {
    const config = fs.readFileSync(path.resolve(__dirname, configPath), 'utf8');
    return JSON.parse(config);
};
const add_paths_to_config = (from, target) => {
    const config = read_config(from);
    const paths = generate_config_paths();
    config.compilerOptions.paths = paths;
    fs.writeFileSync(path.resolve(__dirname, target), JSON.stringify(config, null, 4), 'utf8');

};

const generate_config_files = () => {
    add_paths_to_config('./_tsconfig.json', '../tsconfig.json');
    add_paths_to_config('./_jsconfig.json', '../jsconfig.json');
}


module.exports = {generate_vue_paths, generate_config_files, generate_vite_paths};
