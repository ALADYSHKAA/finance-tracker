import {defineConfig} from 'vite'
import {createVuePlugin} from 'vite-plugin-vue2'

const {generate_vite_paths, generate_config_files} = require('./config/configGen');

generate_config_files();

export default defineConfig(({mode}) => {
    return {
        plugins: [
            createVuePlugin(),
        ],
        base: '/frontapp/',
        server: {
            port: 20011,
            proxy: {
                '/api': 'http://localhost:7000',
            },
            fs: {
                allow: ['../corelib', './']
            }
        },
        css: {
            preprocessorOptions: {
                sass: {
                    additionalData: `
@import "@/styles/_variables.scss"
`,
                },
                scss: {
                    additionalData: `
						@import "@/styles/_mixins.scss";
						@import "@/styles/_variables.scss";
					`,
                },
            },
        },
        resolve: {
            extensions: ['.js', '.vue', '.json', '.ts'],
            alias: [...generate_vite_paths()],
        },
        module: {
            test: /\.vue$/,
            loader: 'vue-loader'
        },
        build: {
            outDir: '../wwwroot/frontapp',
        }
    }

})
