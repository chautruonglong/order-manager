<template>
  <div class="bg-white">
    <div class="max-w-2xl mx-auto py-16 px-4 sm:py-24 sm:px-6 lg:max-w-7xl lg:px-8">
      <div class="flex items-center mb-12">
        <ion-icon name="filter-outline" size="large" class="mr-2"></ion-icon>
        <tutorial-combobox :items="orderItems" :itemClick="orderItemClick" class="mr-2" />
        <tutorial-combobox :items="directionItems" :itemClick="directionItemClick" />
      </div>

      <h2 class="text-2xl font-extrabold tracking-tight text-gray-900">Lex Shopee</h2>

      <div class="mt-6 grid grid-cols-1 gap-y-10 gap-x-6 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
        <template v-for="product in products">
          <tutorial-product-card :product="product" :key="product.id" />
        </template>
      </div>
    </div>

    <div @click="moreButtonClick" class="flex justify-center mb-3 rounded-md shadow">
      <button
        class="flex items-center px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
      >
        <v-progress-circular v-if="isMoreClickLoading === true" indeterminate color="white" class="h-6 w-6"></v-progress-circular>
        <ion-icon v-else name="arrow-down-circle-outline" class="h-6 w-6 font-bold"></ion-icon>
        <span class="ml-3">More</span>
      </button>
    </div>

    <tutorial-cart />
    <tutorial-product-modal />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { PRODUCT_GETTERS, PRODUCT_ACTIONS, PRODUCT_MUTATIONS } from '@store/product'
import { COMMON_MUTATIONS } from '@store/common'

const orderItems = ['Modified At', 'Created At', 'Name']
const directionItems = ['DESC', 'ASC']

export default Vue.extend({
  data() {
    return {
      isMoreClickLoading: false,
      orderItems,
      directionItems,
    }
  },
  computed: {
    ...mapGetters({
      products: PRODUCT_GETTERS.GET_PRODUCTS,
      page: PRODUCT_GETTERS.GET_PAGE,
      sort: PRODUCT_GETTERS.GET_SORT,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutatePage: PRODUCT_MUTATIONS.MUTATE_PAGE,
      mutateSort: PRODUCT_MUTATIONS.MUTATE_SORT,
    }),

    ...mapActions({
      fetchProducts: PRODUCT_ACTIONS.FETCH_PRODUCTS,
      reorderProducts: PRODUCT_ACTIONS.REORDER_PRODUCTS,
    }),

    async moreButtonClick() {
      this.isMoreClickLoading = true
      this.mutatePage(this.page + 1)
      await this.fetchProducts()
      this.isMoreClickLoading = false
    },

    orderItemClick(current: string) {
      const direction = this.sort.split('_').pop()
      const sort = current.replace(' ', '_') + `_${direction}`
      this.mutateSort(sort.toLowerCase())
      this.reorderProducts()
    },

    directionItemClick(current: string) {
      const tokens = this.sort.split('_')
      tokens.pop()
      const sort = tokens.join('_') + `_${current}`
      this.mutateSort(sort.toLowerCase())
      this.reorderProducts()
    },
  },
  async created() {
    if (!this.products || this.products.length <= 0) {
      await this.fetchProducts()
    }
    this.mutateIsLoading(false)
  },
})
</script>
