<template>
  <div class="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <div>
        <img :src="lexLogo" class="mx-auto h-12 w-auto" alt="" />
        <h2 class="mt-6 text-center text-3xl font-extrabold text-gray-900">
          Sign {{ isLoginForm ? 'in' : 'up' }} to your account
        </h2>
        <p class="mt-2 text-center text-sm text-gray-600">
          Or
          <a @click="isLoginForm = false" v-if="isLoginForm === true" class="font-medium text-indigo-600 hover:text-indigo-500"
            >Sign up
          </a>
          <a @click="isLoginForm = true" v-if="isLoginForm === false" class="font-medium text-indigo-600 hover:text-indigo-500"
            >Sign in
          </a>
        </p>
      </div>

      <div class="mt-8 space-y-6">
        <template v-if="isLoginForm === true">
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <input
                v-model="loginPayload.email"
                type="email"
                autocomplete="email"
                required
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Email address"
              />
            </div>

            <div>
              <input
                v-model="loginPayload.password"
                type="password"
                required
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Password"
              />
            </div>
          </div>

          <div class="flex items-center justify-between">
            <div class="flex items-center">
              <input type="checkbox" class="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded" />
              <label class="ml-2 block text-sm text-gray-900">Remember me</label>
            </div>

            <div class="text-sm">
              <a href="#" class="font-medium text-indigo-600 hover:text-indigo-500">Forgot your password?</a>
            </div>
          </div>
        </template>

        <template v-else>
          <div class="rounded-md shadow-sm -space-y-px">
            <div>
              <input
                v-model="registerPayload.email"
                type="email"
                autocomplete="email"
                required
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Email address"
              />
            </div>

            <div>
              <input
                v-model="registerPayload.password"
                type="password"
                required
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Password"
              />
            </div>

            <div>
              <input
                v-model="registerPayload.name"
                type="text"
                required
                class="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                placeholder="Your name"
              />
            </div>
          </div>
        </template>

        <div>
          <button
            @click="signButtonClick"
            class="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          >
            <span class="absolute left-0 inset-y-0 flex items-center pl-3">
              <ion-icon name="lock-closed"></ion-icon>
            </span>
            Sign {{ isLoginForm ? 'in' : 'up' }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations, mapActions } from 'vuex'
import { COMMON_MUTATIONS } from '@store/common'
import { AUTH_ACTIONS } from '@store/auth'
import { Login, UserRegister } from '@models'

const lexLogo = '/lex-logo.svg'

const loginPayload: Login = {
  email: '',
  password: '',
}

const registerPayload: UserRegister = {
  email: '',
  password: '',
  name: '',
}

export default Vue.extend({
  data() {
    return {
      lexLogo,
      isLoginForm: true,
      loginPayload,
      registerPayload,
    }
  },
  methods: {
    ...mapMutations({
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
    }),

    ...mapActions({
      login: AUTH_ACTIONS.LOGIN,
      register: AUTH_ACTIONS.REGISTER,
    }),

    async signButtonClick() {
      try {
        this.mutateIsLoading(true)
        if (this.isLoginForm) {
          await this.login(this.loginPayload)
          this.$router.push('/')
          this.mutateSnackbar('Login successfully')
        } else {
          await this.register(this.registerPayload)
          this.isLoginForm = true

          this.mutateSnackbar('Register successfully')
        }
      } catch (e: any) {
        this.mutateSnackbar(e.message)
      } finally {
        this.mutateIsLoading(false)
      }
    },
  },
  mounted() {
    this.mutateIsLoading(false)
  },
})
</script>
