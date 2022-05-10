<template>
  <div class="tw-min-h-full tw-flex tw-items-center tw-justify-center tw-py-12 tw-px-4 sm:tw-px-6 lg:tw-px-8">
    <div class="tw-max-w-md tw-w-full tw-space-y-8">
      <div>
        <img :src="lexLogo" class="tw-mx-auto tw-h-12 tw-w-auto" alt="" />
        <h2 class="tw-mt-6 tw-text-center tw-text-3xl tw-font-extrabold tw-text-gray-900">
          Sign {{ isLoginForm ? 'in' : 'up' }} to your account
        </h2>
        <p class="tw-mt-2 tw-text-center tw-text-sm tw-text-gray-600">
          Or
          <a
            @click="isLoginForm = false"
            v-if="isLoginForm === true"
            class="tw-font-medium tw-text-indigo-600 hover:tw-text-indigo-500"
            >Sign up
          </a>
          <a
            @click="isLoginForm = true"
            v-if="isLoginForm === false"
            class="tw-font-medium tw-text-indigo-600 hover:tw-text-indigo-500"
            >Sign in
          </a>
        </p>
      </div>

      <div class="tw-mt-8 tw-space-y-6">
        <template v-if="isLoginForm === true">
          <div class="tw-rounded-md tw-shadow-sm tw--space-y-px">
            <div>
              <input
                v-model="loginPayload.email"
                @keypress.enter="signButtonClick"
                type="email"
                autocomplete="email"
                required
                class="tw-appearance-none tw-rounded-none tw-relative tw-block tw-w-full tw-px-3 tw-py-2 tw-border tw-border-gray-300 tw-placeholder-gray-500 tw-text-gray-900 tw-rounded-t-md focus:tw-outline-none focus:tw-ring-indigo-500 focus:tw-border-indigo-500 focus:tw-z-10 sm:tw-text-sm"
                placeholder="Email address"
              />
            </div>

            <div>
              <input
                v-model="loginPayload.password"
                @keypress.enter="signButtonClick"
                type="password"
                required
                class="tw-appearance-none tw-rounded-none tw-relative tw-block tw-w-full tw-px-3 tw-py-2 tw-border tw-border-gray-300 tw-placeholder-gray-500 tw-text-gray-900 tw-rounded-b-md focus:tw-outline-none focus:tw-ring-indigo-500 focus:tw-border-indigo-500 focus:tw-z-10 sm:tw-text-sm"
                placeholder="Password"
              />
            </div>
          </div>

          <div class="tw-flex tw-items-center tw-justify-between">
            <div class="tw-flex tw-items-center">
              <input
                type="checkbox"
                class="tw-h-4 tw-w-4 tw-text-indigo-600 focus:tw-ring-indigo-500 tw-border-gray-300 tw-rounded"
              />
              <label class="tw-ml-2 tw-block tw-text-sm tw-text-gray-900">Remember me</label>
            </div>

            <div class="tw-text-sm">
              <a href="#" class="tw-font-medium tw-text-indigo-600 hover:tw-text-indigo-500">Forgot your password?</a>
            </div>
          </div>
        </template>

        <template v-else>
          <div class="tw-rounded-md tw-shadow-sm tw--space-y-px">
            <div>
              <input
                v-model="registerPayload.email"
                @keypress.enter="signButtonClick"
                type="email"
                autocomplete="email"
                required
                class="tw-appearance-none tw-rounded-none tw-relative tw-block tw-w-full tw-px-3 tw-py-2 tw-border tw-border-gray-300 tw-placeholder-gray-500 tw-text-gray-900 tw-rounded-t-md focus:tw-outline-none focus:tw-ring-indigo-500 focus:tw-border-indigo-500 focus:tw-z-10 sm:tw-text-sm"
                placeholder="Email address"
              />
            </div>

            <div>
              <input
                v-model="registerPayload.password"
                @keypress.enter="signButtonClick"
                type="password"
                required
                class="tw-appearance-none tw-rounded-none tw-relative tw-block tw-w-full tw-px-3 tw-py-2 tw-border tw-border-gray-300 tw-placeholder-gray-500 tw-text-gray-900 tw-rounded-b-md focus:tw-outline-none focus:tw-ring-indigo-500 focus:tw-border-indigo-500 focus:tw-z-10 sm:tw-text-sm"
                placeholder="Password"
              />
            </div>

            <div>
              <input
                v-model="registerPayload.name"
                @keypress.enter="signButtonClick"
                type="text"
                required
                class="tw-appearance-none tw-rounded-none tw-relative tw-block tw-w-full tw-px-3 tw-py-2 tw-border tw-border-gray-300 tw-placeholder-gray-500 tw-text-gray-900 tw-rounded-b-md focus:tw-outline-none focus:tw-ring-indigo-500 focus:tw-border-indigo-500 focus:tw-z-10 sm:tw-text-sm"
                placeholder="Your name"
              />
            </div>
          </div>
        </template>

        <div>
          <button
            @click="signButtonClick"
            class="group tw-relative tw-w-full tw-flex tw-justify-center tw-py-2 tw-px-4 tw-border tw-border-transparent tw-text-sm tw-font-medium tw-rounded-md tw-text-white tw-bg-indigo-600 hover:tw-bg-indigo-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-indigo-500"
          >
            <span class="tw-absolute tw-left-0 tw-inset-y-0 tw-flex tw-items-center tw-pl-3">
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
          location.replace('/')
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
