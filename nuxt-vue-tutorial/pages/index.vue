<template>
  <div>
    <tutorial-products />
    <tutorial-product-creation />

    <button
      v-if="isAuthenticated"
      @click="addProductButtonClick"
      class="tw-fixed tw-w-16 tw-h-16 tw-bottom-12 tw-right-12 tw-bg-indigo-600 tw-rounded-full tw-z-40 hover:tw-scale-110"
    >
      <ion-icon name="create-outline" class="tw-text-3xl tw-text-white"></ion-icon>
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_MUTATIONS } from '@store/common'
import { AUTH_GETTERS } from '@store/auth'
import { PRODUCT_GETTERS, PRODUCT_ACTIONS } from '@store/product'

export default Vue.extend({
  computed: {
    ...mapGetters({
      isAuthenticated: AUTH_GETTERS.GET_IS_AUTHENTICATED,
      products: PRODUCT_GETTERS.GET_PRODUCTS,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateIsShowProductCreation: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_CREATION,
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
    }),

    ...mapActions({
      fetchProducts: PRODUCT_ACTIONS.FETCH_PRODUCTS,
    }),

    addProductButtonClick() {
      this.mutateIsShowProductCreation(true)
    },
  },
  async created() {
    try {
      this.mutateIsLoading(true)
      if (!this.products || this.products.length <= 0) {
        await this.fetchProducts()
      }
    } catch (e: any) {
      this.mutateSnackbar(e.message)
    } finally {
      this.mutateIsLoading(false)
    }
  },
})
</script>
