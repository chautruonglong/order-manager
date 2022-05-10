<template>
  <nav class="bg-gray-800">
    <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
      <div class="relative flex items-center justify-between h-16">
        <div class="absolute inset-y-0 left-0 flex items-center sm:hidden">
          <button
            class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
          >
            <ion-icon name="menu-outline" size="large"></ion-icon>
          </button>
        </div>

        <div class="flex-1 flex items-center justify-start">
          <a @click="lexLogoClick" class="hidden flex-shrink-0 sm:flex items-center">
            <img class="h-8 w-auto" :src="lexLogo" alt="" />
          </a>

          <a @click="lexLogoClick" class="flex mx-auto flex-shrink-0 sm:hidden items-center">
            <img class="h-8 w-auto" :src="lexLogo" alt="" />
          </a>

          <div class="hidden sm:block sm:ml-6">
            <div class="flex space-x-4">
              <template v-for="(item, index) in navigation">
                <a v-if="item.active" :key="index" class="bg-gray-900 text-white px-3 py-2 rounded-md text-sm font-medium">
                  {{ item.name }}
                </a>
                <a
                  v-else
                  :key="index"
                  @click="navigationItemClick(item)"
                  class="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"
                >
                  {{ item.name }}
                </a>
              </template>
            </div>
          </div>
        </div>

        <div
          v-if="isAuthenticated === true"
          class="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0"
        >
          <button
            @click="bellButtonClick"
            class="flex items-center bg-gray-800 p-1 rounded-full text-gray-400 hover:text-white focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-800 focus:ring-white"
          >
            <ion-icon name="notifications-sharp" size="small"></ion-icon>
          </button>

          <div @click="isUserMenu = true" class="ml-3 relative cursor-pointer">
            <v-avatar color="primary" size="36" class="hover:scale-110">LC</v-avatar>

            <transition>
              <div
                v-show="isUserMenu"
                class="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none"
              >
                <template v-for="(item, index) in userMenu">
                  <a :key="index" @click="menuItemClick(item)" class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-300">{{
                    item
                  }}</a>
                </template>
              </div>
            </transition>
          </div>
        </div>

        <div v-else>
          <nuxt-link to="/login" class="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-lg font-medium"
            >Login</nuxt-link
          >
        </div>
      </div>
    </div>

    <div class="sm:hidden">
      <div class="px-2 pt-2 pb-3 space-y-1">
        <template v-for="(item, index) in navigation">
          <a v-if="item.active" :key="index" class="bg-gray-900 text-white block px-3 py-2 rounded-md text-base font-medium">
            {{ item.name }}
          </a>
          <a
            v-else
            :key="index"
            @click="navigationItemClick(item)"
            class="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
          >
            {{ item.name }}
          </a>
        </template>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations, mapActions, mapGetters } from 'vuex'
import { PRODUCT_MUTATIONS, PRODUCT_ACTIONS } from '@store/product'
import { AUTH_GETTERS, AUTH_ACTIONS } from '@store/auth'
import { COMMON_MUTATIONS } from '@store/common'

interface Navigation {
  name: string
  value: string
  active: boolean
}

const navigation: Navigation[] = [
  { name: 'Clothes', value: 'quần áo', active: false },
  { name: 'Books', value: 'sách', active: false },
  { name: 'Phones', value: 'điện thoại', active: false },
  { name: 'laptops', value: 'máy tính', active: false },
]

const lexLogo = '/lex-logo.svg'

const userMenu = ['Profile', 'History', 'Logout']

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
    }),
  },
  methods: {
    ...mapMutations({
      mutateQuestion: PRODUCT_MUTATIONS.MUTATE_QUESTION,
      mutateProducts: PRODUCT_MUTATIONS.MUTATE_PRODUCTS,
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
      this.mutateQuestion(current.value)
      this.mutateProducts([])
      await this.fetchProducts()
      this.mutateIsLoading(false)
      current.active = true
    },

    async lexLogoClick() {
      this.mutateIsLoading(true)
      this.mutateQuestion('')
      this.mutateProducts([])
      await this.fetchProducts()
      this.mutateIsLoading(false)
    },

    hideUserMenu(e: any) {
      if (!this.$el.contains(e.target)) {
        this.isUserMenu = false
      }
    },

    menuItemClick(item: string) {
      switch (item) {
        case userMenu[2]:
          this.logout()
          break
      }
    },

    bellButtonClick() {
      this.mutateIsShowCart(true)
    },
  },
  mounted() {
    document.addEventListener('click', this.hideUserMenu)
  },
})
</script>
