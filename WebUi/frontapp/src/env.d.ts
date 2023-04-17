/// <reference types="vite/client" />

/*interface ImportMetaEnv extends Readonly<Record<string, string>> {
    readonly VITE_APP_TITLE: string
    readonly BASE_URL: string
    // more env variables...
}*/

interface ImportMeta {
    readonly env: ImportMetaEnv
}