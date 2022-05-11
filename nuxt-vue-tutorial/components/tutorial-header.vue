<template>
  <nav class="tw-bg-gray-800 tw-w-full">
    <div class="tw-max-w-7xl tw-mx-auto tw-px-2 sm:tw-px-6 lg:tw-px-8">
      <div class="tw-relative tw-flex tw-items-center tw-justify-between tw-h-16">
        <div class="tw-absolute tw-inset-y-0 tw-left-0 tw-flex tw-items-center sm:tw-hidden">
          <button
            class="tw-inline-flex tw-items-center tw-justify-center tw-p-2 tw-rounded-md tw-text-gray-400 hover:tw-text-white hover:tw-bg-gray-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-inset focus:tw-ring-white"
          >
            <ion-icon name="menu-outline" size="large"></ion-icon>
          </button>
        </div>

        <div class="tw-flex-1 tw-flex tw-items-center tw-justify-center sm:tw-items-stretch sm:tw-justify-start">
          <a @click="lexLogoClick" class="tw-flex-shrink-0 tw-flex tw-items-center">
            <img class="tw-h-8 tw-w-auto" :src="lexLogo" alt="" />
          </a>

          <div class="tw-hidden sm:tw-block sm:tw-ml-6">
            <div class="tw-flex tw-space-x-4">
              <template v-for="(item, index) in navigation">
                <a
                  v-if="item.active"
                  :key="index"
                  class="tw-bg-gray-900 tw-text-white tw-px-3 tw-py-2 tw-rounded-md tw-text-sm tw-font-medium"
                >
                  {{ item.name }}
                </a>
                <a
                  v-else
                  :key="index"
                  @click="navigationItemClick(item)"
                  class="tw-text-gray-300 hover:tw-bg-gray-700 hover:tw-text-white tw-px-3 tw-py-2 tw-rounded-md tw-text-sm tw-font-medium"
                >
                  {{ item.name }}
                </a>
              </template>
            </div>
          </div>
        </div>

        <div
          v-if="isAuthenticated === true"
          class="tw-absolute tw-inset-y-0 tw-right-0 tw-flex tw-items-center tw-pr-2 sm:tw-static sm:tw-inset-auto sm:tw-ml-6 sm:tw-pr-0"
        >
          <button
            @click="cartButtonClick"
            class="tw-flex tw-items-center tw-bg-gray-800 tw-mr-5 tw-rounded-full tw-text-gray-400 hover:tw-text-white focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-offset-gray-800 focus:tw-ring-white"
          >
            <v-badge color="green" v-if="localBill != null && localBill.orders.length > 0" :content="localBill.orders.length">
              <ion-icon name="cart" class="tw-h-6 tw-w-6"></ion-icon>
            </v-badge>
            <ion-icon name="cart" v-else class="tw-h-6 tw-w-6"></ion-icon>
          </button>

          <div @click="isUserMenu = true" class="tw-ml-3 tw-relative tw-cursor-pointer">
            <v-avatar color="primary" size="36" class="hover:tw-scale-110">{{ sortName }}</v-avatar>

            <transition>
              <div
                v-show="isUserMenu"
                class="tw-origin-top-right tw-absolute tw-right-0 tw-mt-2 px-2 tw-rounded-md tw-shadow-lg tw-py-1 tw-bg-white tw-ring-1 tw-ring-black tw-ring-opacity-5 focus:tw-outline-none"
              >
                <div class="tw-block tw-px-4 tw-py-2 tw-text-sm tw-text-purple-600">Hello, {{ user ? user.name : 'There' }}!</div>
                <div class="tw-block tw-px-4 tw-py-2 tw-text-sm tw-text-purple-600">
                  {{ user ? user.email : 'you@gmail.com' }}
                </div>
                <v-divider></v-divider>
                <template v-for="(item, index) in userMenu">
                  <a
                    :key="index"
                    @click="menuItemClick(item)"
                    class="tw-block tw-px-4 tw-py-2 tw-text-sm tw-text-gray-700 hover:tw-bg-gray-300"
                    >{{ item }}</a
                  >
                </template>
              </div>
            </transition>
          </div>
        </div>

        <div v-else>
          <nuxt-link
            to="/login"
            class="tw-text-gray-300 hover:tw-bg-gray-700 hover:tw-text-white tw-px-3 tw-py-2 tw-rounded-md tw-text-lg tw-font-medium"
          >
            Login
          </nuxt-link>
        </div>
      </div>
    </div>

    <div class="sm:tw-hidden">
      <div class="tw-px-2 tw-pt-2 tw-pb-3 tw-space-y-1">
        <template v-for="(item, index) in navigation">
          <a
            v-if="item.active"
            :key="index"
            class="tw-bg-gray-900 tw-text-white tw-block tw-px-3 tw-py-2 tw-rounded-md tw-text-base tw-font-medium"
          >
            {{ item.name }}
          </a>
          <a
            v-else
            :key="index"
            @click="navigationItemClick(item)"
            class="tw-text-gray-300 hover:tw-bg-gray-700 hover:tw-text-white tw-block tw-px-3 tw-py-2 tw-rounded-md tw-text-base tw-font-medium"
          >
            {{ item.name }}
          </a>
        </template>
      </div>
    </div>

    <tutorial-confirm ref="confirmDialog" />
  </nav>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations, mapActions, mapGetters } from 'vuex'
import { PRODUCT_MUTATIONS, PRODUCT_ACTIONS } from '@store/product'
import { AUTH_GETTERS, AUTH_ACTIONS } from '@store/auth'
import { COMMON_MUTATIONS } from '@store/common'
import { BILL_GETTERS } from '@store/bill'
import { USER_GETTERS } from '@store/user'

interface Navigation {
  name: string
  value: string
  active: boolean
}

const navigation: Navigation[] = [
  { name: 'Clothes', value: 'áo', active: false },
  { name: 'Books', value: 'sách', active: false },
  { name: 'Phones', value: 'điện thoại', active: false },
  { name: 'Laptops', value: 'máy tính', active: false },
]

const lexLogo = '/lex-logo.svg'

const userMenu = ['History', 'Logout']

export default Vue.extend({
  data() {
    return {
      navigation,
      lexLogo,
      userMenu,
      isUserMenu: false,
    }
  },
  computed: {
    ...mapGetters({
      isAuthenticated: AUTH_GETTERS.GET_IS_AUTHENTICATED,
      localBill: BILL_GETTERS.GET_LOCAL_BILL,
      user: USER_GETTERS.GET_USER,
    }),

    sortName() {
      let name = ''
      this.user?.name.split(' ').forEach((n: string) => (name += n[0]))
      return name && name.length <= 3 ? name : 'YOU'
    },
  },
  methods: {
    ...mapMutations({
      mutateQuestion: PRODUCT_MUTATIONS.MUTATE_QUESTION,
      mutateProducts: PRODUCT_MUTATIONS.MUTATE_PRODUCTS,
      mutatePage: PRODUCT_MUTATIONS.MUTATE_PAGE,
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateIsShowCart: COMMON_MUTATIONS.MUTATE_IS_SHOW_CART,
    }),

    ...mapActions({
      fetchProducts: PRODUCT_ACTIONS.FETCH_PRODUCTS,
      logout: AUTH_ACTIONS.LOGOUT,
    }),

    async navigationItemClick(current: Navigation) {
      this.mutateIsLoading(true)
      this.navigation.forEach((item) => (item.active = false))

      this.mutatePage(0)
      this.mutateProducts([])
      this.mutateQuestion(current.value)

      current.active = true

      if (this.$route.path !== '/') {
        this.$router.push('/')
      }

      await this.fetchProducts()
      this.mutateIsLoading(false)
    },

    async lexLogoClick() {
      if (this.navigation.some((item) => item.active === true)) {
        this.mutateIsLoading(true)
        this.navigation.forEach((item) => (item.active = false))

        this.mutatePage(0)
        this.mutateProducts([])
        this.mutateQuestion('')

        if (this.$route.path !== '/') {
          this.$router.push('/')
        }

        await this.fetchProducts()
        this.mutateIsLoading(false)
      }
    },

    async menuItemClick(item: string) {
      switch (item) {
        case userMenu[0]:
          this.$router.push('/history')
          this.isUserMenu = false
          break
        case userMenu[1]:
          if (await (this.$refs.confirmDialog as any).active()) {
            this.logout()
            location.reload()
          }
          break
      }
    },

    cartButtonClick() {
      this.mutateIsShowCart(true)
    },

    hideUserMenu(e: any) {
      if (!this.$el.contains(e.target)) {
        this.isUserMenu = false
      }
    },
  },
  mounted() {
    document.addEventListener('click', this.hideUserMenu)
  },
})
</script>
