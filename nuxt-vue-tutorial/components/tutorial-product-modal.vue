<template>
  <div
    v-if="productModal != null"
    v-show="isShowProductModal === true"
    class="tw-fixed tw-z-10 tw-inset-0 tw-overflow-y-auto tw-h-full"
    role="dialog"
    aria-modal="true"
  >
    <div class="tw-flex tw-min-h-screen tw-text-center md:tw-block md:tw-px-2 lg:tw-px-4" style="font-size: 0">
      <div
        class="tw-hidden tw-fixed tw-inset-0 tw-bg-gray-500 tw-bg-opacity-75 tw-transition-opacity md:tw-block"
        aria-hidden="true"
      ></div>
      <span class="tw-hidden md:tw-inline-block md:tw-align-middle md:tw-h-screen" aria-hidden="true">&#8203;</span>

      <div
        class="tw-flex tw-text-base tw-text-left tw-transform tw-transition tw-w-full md:tw-inline-block md:tw-max-w-2xl md:tw-px-4 md:tw-my-8 md:tw-align-middle lg:tw-max-w-4xl"
      >
        <div
          class="tw-w-full tw-relative tw-flex tw-flex-col tw-items-center tw-rounded-3xl tw-bg-white tw-p-10 tw-overflow-hidden tw-shadow-2xl sm:tw-px-6 sm:tw-pt-8 md:tw-p-6 lg:tw-p-8"
        >
          <button @click="closeButtonClick" class="tw-flex tw-justify-end tw-w-full tw-mb-5text-gray-400 hover:tw-text-gray-500">
            <ion-icon name="close" size="large"></ion-icon>
          </button>
          <div class="tw-w-full tw-grid tw-grid-cols-1 tw-gap-y-8 tw-gap-x-6 tw-items-start sm:tw-grid-cols-12 lg:tw-gap-x-8">
            <div class="aspect-w-2 aspect-h-3 tw-bg-gray-100 tw-overflow-hidden sm:tw-col-span-4 lg:tw-col-span-5">
              <img :src="productModal.image" alt="" class="tw-object-center tw-object-cover" />
            </div>
            <div class="sm:tw-col-span-8 lg:tw-col-span-7 tw-flex tw-flex-col tw-justify-between tw-h-full">
              <h2 class="tw-text-2xl tw-font-extrabold tw-text-gray-900">{{ productModal.name }}</h2>
              <div class="tw-my-7">
                <p class="tw-text-2xl tw-text-gray-900">
                  {{ productModal.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
                </p>
                <span
                  class="tw-mt-1 tw-pl-1 tw-text-sm tw-text-gray-500 tw-border-solid tw-border-2 tw-border-indigo-100 bg-slate-200"
                  >-{{ productModal.discount }}%</span
                >
              </div>
              <p class="tw-w-full tw-text-sm tw-text-gray-500">{{ productModal.description }}</p>
              <div v-if="isAuthenticated" class="tw-flex tw-justify-between tw-mt-5 tw-w-full">
                <button
                  @click="deleteButtonClick"
                  class="tw-bg-red-600 tw-border tw-border-transparent tw-rounded-md tw-py-3 tw-px-5 tw-flex tw-items-center tw-justify-center tw-text-base tw-font-medium tw-text-white hover:tw-bg-red-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-red-500"
                >
                  <ion-icon name="trash-outline" class="tw-text-xl"></ion-icon>
                  <span class="tw-ml-5">Delete</span>
                </button>
                <button
                  @click="editButtonClick"
                  class="tw-bg-indigo-600 tw-border tw-border-transparent tw-rounded-md tw-py-3 tw-px-5 tw-flex tw-items-center tw-justify-center tw-text-base tw-font-medium tw-text-white hover:tw-bg-indigo-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-indigo-500"
                >
                  <ion-icon name="create-outline" class="tw-text-xl"></ion-icon>
                  <span class="tw-ml-5">Edit</span>
                </button>
                <button
                  @click="orderButtonClick"
                  class="tw-bg-indigo-600 tw-border tw-border-transparent tw-rounded-md tw-py-3 tw-px-5 tw-flex tw-items-center tw-justify-center tw-text-base tw-font-medium tw-text-white hover:tw-bg-indigo-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-indigo-500"
                >
                  <ion-icon name="cart-outline" class="tw-text-xl"></ion-icon>
                  <span class="tw-ml-5">Order</span>
                </button>
              </div>

              <div v-else class="tw-flex tw-justify-end tw-mt-5 tw-w-full">
                <button
                  @click="loginFirstClick"
                  class="tw-bg-indigo-600 tw-border tw-border-transparent tw-rounded-md tw-py-3 tw-px-5 tw-flex tw-items-center tw-justify-center tw-text-base tw-font-medium tw-text-white hover:tw-bg-indigo-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-indigo-500"
                >
                  <span class="tw-mr-5">Login First</span>
                  <ion-icon name="arrow-forward-sharp" class="tw-w-6 tw-h-6"></ion-icon>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <tutorial-confirm ref="confirmDialog" />
    <tutorial-product-modification :product="{ ...productModal }" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { PRODUCT_GETTERS, PRODUCT_ACTIONS } from '@store/product'
import { BILL_ACTIONS } from '@store/bill'
import { AUTH_GETTERS } from '@store/auth'

export default Vue.extend({
  computed: {
    ...mapGetters({
      isShowProductModal: COMMON_GETTERS.GET_IS_SHOW_PRODUCT_MODAL,
      productModal: PRODUCT_GETTERS.GET_PRODUCT_MODAL,
      orderProducts: PRODUCT_GETTERS.GET_ORDER_PRODUCTS,
      isAuthenticated: AUTH_GETTERS.GET_IS_AUTHENTICATED,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsShowProductModal: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_MODAL,
      mutateIsShowProductModification: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_MODIFICATION,
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
    }),

    ...mapActions({
      deleteProduct: PRODUCT_ACTIONS.DELETE_PRODUCT,
      addProductToCart: BILL_ACTIONS.ADD_PRODUCT_TO_CART,
    }),

    closeButtonClick() {
      this.mutateIsShowProductModal(false)
    },

    async deleteButtonClick() {
      if (await (this.$refs.confirmDialog as any).active()) {
        try {
          this.mutateIsLoading(true)
          await this.deleteProduct(this.productModal.id)
          this.mutateSnackbar('Delete successfully')
        } catch (e: any) {
          this.mutateSnackbar(e.message)
        } finally {
          this.mutateIsShowProductModal(false)
          this.mutateIsLoading(false)
        }
      }
    },

    editButtonClick() {
      this.mutateIsShowProductModification(true)
    },

    async orderButtonClick() {
      try {
        await this.addProductToCart(this.productModal)
        this.mutateSnackbar('Add a product to cart')
      } catch (e: any) {
        this.mutateSnackbar(e.message)
      } finally {
        this.mutateIsShowProductModal(false)
      }
    },

    loginFirstClick() {
      this.$router.push('/login')
    },
  },
})
</script>
