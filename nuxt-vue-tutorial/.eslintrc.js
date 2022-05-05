const prettierrc = require('./.prettierrc')

module.exports = {
  root: true,
  env: {
    browser: true,
    node: true,
  },
  extends: ['@nuxtjs/eslint-config-typescript', 'plugin:nuxt/recommended', 'prettier'],
  plugins: [],
  rules: {
    'prettier/prettier': [...prettierrc, 'error'],
  },
}
