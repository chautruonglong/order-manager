<template>
  <nav class="bg-gray-800">
    <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
      <div class="relative flex items-center justify-between h-20">
        <div class="absolute inset-y-0 left-0 flex items-center sm:hidden">
          <button
            class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:text-white hover:bg-gray-700 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
          >
            <svg
              class="block h-6 w-6"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke-width="2"
              stroke="currentColor"
              aria-hidden="true"
            >
              <path stroke-linecap="round" stroke-linejoin="round" d="M4 6h16M4 12h16M4 18h16" />
            </svg>
          </button>
        </div>
        <div class="flex-1 flex items-center justify-center sm:items-stretch sm:justify-start">
          <div class="flex-shrink-0 flex items-center">
            <img class="hidden md:block h-8 w-auto" src="/lex_logo.svg" alt="Workflow" />
          </div>

          <div class="hidden sm:block sm:ml-6">
            <div class="flex space-x-4">
              <div v-for="nav in navigation" :key="nav.name" @click="navClick(nav)">
                <button v-if="nav.current" class="bg-gray-900 text-white px-3 py-2 rounded-md text-sm font-medium">{{ nav.name }}</button>
                <button v-else class="text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium">
                  {{ nav.name }}
                </button>
              </div>
            </div>
          </div>

          <input
            @keypress.enter="enterOnSearch"
            v-model="searchInput"
            class="appearance-none rounded-none relative block w-72 ml-5 px-3 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
            placeholder="Search"
          />
        </div>
        <div class="ml-3 relative">
          <button
            v-if="isAuthenticated == true"
            @click="logoutClick"
            class="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
          >
            Logout
          </button>
          <button
            v-else
            @click="loginClick"
            class="text-gray-300 hover:bg-gray-700 hover:text-white block px-3 py-2 rounded-md text-base font-medium"
          >
            Login
          </button>
        </div>
      </div>
    </div>

    <div class="sm:hidden" id="mobile-menu">
      <div class="px-2 pt-2 pb-3 space-y-1">
        <div v-for="nav in navigation" :key="nav.name" @click="navClick(nav)" class="flex flex-col justify-start">
          <button v-if="nav.current" class="inline-block bg-gray-900 text-white inline px-3 py-2 rounded-md text-base font-medium">
            {{ nav.name }}
          </button>
          <button v-else class="text-gray-300 hover:bg-gray-700 hover:text-white inline px-3 py-2 rounded-md text-base font-medium">
            {{ nav.name }}
          </button>
        </div>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations, mapActions, mapState } from 'vuex'
import { MUTATIONS } from '@/store/mutations'
import { ACTIONS } from '@/store/actions'

const navigation = [
  { name: 'Phone', value: 'điện thoại', current: true },
  { name: 'Clothes', value: 'áo', current: false },
  { name: 'Book', value: 'sách', current: false },
  { name: 'Laptop', value: 'laptop', current: false },
]

export default Vue.extend({
  data() {
    return {
      navigation,
      searchInput: '',
    }
  },
  computed: {
    ...mapState(['isAuthenticated']),
  },
  methods: {
    ...mapMutations({
      setQuestion: MUTATIONS.SET_QUESTION,
      setProducts: MUTATIONS.SET_PRODUCTS,
      setPage: MUTATIONS.SET_PAGE,
    }),
    ...mapActions({
      fetchProducts: ACTIONS.FETCH_PRODUCTS,
      logout: ACTIONS.LOGOUT,
    }),

    navClick(currentNav: any) {
      if (!currentNav.current) {
        navigation.forEach((nav) => (nav.current = false))
        currentNav.current = true

        this.setQuestion(currentNav.value)
        this.setProducts([])
        this.setPage(0)
        this.fetchProducts()
      }
    },

    enterOnSearch() {
      this.setQuestion(this.searchInput)
      this.setProducts([])
      this.setPage(0)
      this.fetchProducts()
    },

    loginClick() {
      this.$router.push('/login')
    },

    logoutClick() {
      this.logout()
    },
  },
})
</script>
