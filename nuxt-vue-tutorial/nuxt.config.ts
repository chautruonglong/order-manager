import { NuxtConfig } from '@nuxt/types'
import { resolve } from 'path'

const config: NuxtConfig = {
  head: {
    title: 'Lex Shopee',
    htmlAttrs: {
      lang: 'en',
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' },
    ],
    link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }],
    script: [
      { hid: 'ionic-no-module', src: 'https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js', defer: true },
      { hid: 'ionic-module', src: 'https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js', type: 'module', defer: true },
    ],
  },

  css: [],

  plugins: ['@plugins/api.plugin.ts'],

  components: true,

  buildModules: ['@nuxt/typescript-build', '@nuxtjs/tailwindcss', '@nuxtjs/vuetify'],

  modules: ['@nuxtjs/axios'],

  axios: {
    baseURL: process.env.API_BASE_URL || 'https://localhost:5001',
  },

  ssr: false,

  server: {
    host: process.env.HOST || '0.0.0.0',
    port: process.env.PORT || 3000,
  },

  build: {},

  alias: {
    '@': resolve(__dirname, './'),
    '@utils': resolve(__dirname, './utils'),
    '@store': resolve(__dirname, './store'),
    '@pages': resolve(__dirname, './pages'),
    '@assets': resolve(__dirname, './assets'),
    '@models': resolve(__dirname, './models'),
    '@layouts': resolve(__dirname, './layouts'),
    '@plugins': resolve(__dirname, './plugins'),
    '@services': resolve(__dirname, './services'),
    '@middleware': resolve(__dirname, './middleware'),
    '@components': resolve(__dirname, './components'),
  },

  vuetify: {
    treeShake: true,
    customVariables: ['@assets/variables.scss'],
  },
}

export default config
