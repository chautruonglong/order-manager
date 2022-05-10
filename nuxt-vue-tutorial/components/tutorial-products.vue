<template>
  <div class="tw-bg-white">
    <div class="tw-max-w-2xl tw-mx-auto tw-py-16 tw-px-4 sm:tw-py-24 sm:tw-px-6 lg:tw-max-w-7xl lg:tw-px-8">
      <div class="tw-flex tw-items-center tw-mb-12">
        <ion-icon name="filter-outline" size="large" class="tw-mr-2"></ion-icon>
        <tutorial-combobox :items="orderItems" :itemClick="orderItemClick" class="tw-mr-2" />
        <tutorial-combobox :items="directionItems" :itemClick="directionItemClick" />
      </div>

      <h2 class="tw-text-2xl tw-font-extrabold tw-tracking-tight tw-text-gray-900">Lex Shopee</h2>

      <div class="tw-mt-6 tw-grid tw-grid-cols-1 tw-gap-y-10 tw-gap-x-6 sm:tw-grid-cols-2 lg:tw-grid-cols-4 xl:tw-gap-x-8">
        <template v-for="product in products">
          <tutorial-product-card :product="product" :key="product.id" />
        </template>
      </div>
    </div>

    <div @click="moreButtonClick" class="tw-flex tw-justify-center tw-mb-3 tw-rounded-md tw-shadow">
      <button
        class="tw-flex tw-items-center tw-px-5 tw-py-3 tw-border tw-border-transparent tw-text-base tw-font-medium tw-rounded-md tw-text-white tw-bg-indigo-600 hover:tw-bg-indigo-700"
      >
        <v-progress-circular
          v-if="isMoreClickLoading === true"
          indeterminate
          color="white"
          class="tw-h-6 tw-w-6"
        ></v-progress-circular>
        <ion-icon v-else name="arrow-down-circle-outline" class="tw-h-6 tw-w-6 tw-font-bold"></ion-icon>
        <span class="tw-ml-3">More</span>
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
