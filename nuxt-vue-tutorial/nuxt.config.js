export default {
  server: {
    host: process.env.HOST || '0.0.0.0',
    port: process.env.PORT || '3000',
  },
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
      { hid: 'ion-icon', src: 'https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js', defer: true, type: 'module' },
      { hid: 'ion-icon2', src: 'https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js', defer: true },
    ],
  },

  css: [],

  plugins: ['@/plugins/api.plugin.ts'],

  components: true,

  buildModules: ['@nuxt/typescript-build', '@nuxtjs/tailwindcss'],

  modules: ['@nuxtjs/axios'],

  axios: {
    baseURL: process.env.API_BASE_URL || 'https://localhost:5001/api',
  },

  build: {
    extend(config) {
      config.module.rules.push({
        test: /\.mjs$/,
        include: /node_modules/,
        type: 'javascript/auto',
      })
    },
  },
}
