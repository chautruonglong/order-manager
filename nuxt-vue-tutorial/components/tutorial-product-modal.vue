<template>
  <div
    v-if="productModal != null"
    v-show="isShowProductModal === true"
    class="fixed z-10 inset-0 overflow-y-auto h-full"
    role="dialog"
    aria-modal="true"
  >
    <div class="flex min-h-screen text-center md:block md:px-2 lg:px-4" style="font-size: 0">
      <div class="hidden fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity md:block" aria-hidden="true"></div>
      <span class="hidden md:inline-block md:align-middle md:h-screen" aria-hidden="true">&#8203;</span>

      <div
        class="flex text-base text-left transform transition w-full md:inline-block md:max-w-2xl md:px-4 md:my-8 md:align-middle lg:max-w-4xl"
      >
        <div
          class="w-full relative flex flex-col items-center bg-white p-10 overflow-hidden shadow-2xl sm:px-6 sm:pt-8 md:p-6 lg:p-8"
        >
          <button @click="closeButtonClick" class="flex justify-end w-full mb-5text-gray-400 hover:text-gray-500">
            <ion-icon name="close" size="large"></ion-icon>
          </button>
          <div class="w-full grid grid-cols-1 gap-y-8 gap-x-6 items-start sm:grid-cols-12 lg:gap-x-8">
            <div class="aspect-w-2 aspect-h-3 rounded-lg bg-gray-100 overflow-hidden sm:col-span-4 lg:col-span-5">
              <img :src="productModal.image" alt="" class="object-center object-cover" />
            </div>
            <div class="sm:col-span-8 lg:col-span-7 flex flex-col justify-between h-full">
              <h2 class="text-2xl font-extrabold text-gray-900">{{ productModal.name }}</h2>
              <div class="my-7">
                <p class="text-2xl text-gray-900">
                  {{ productModal.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
                </p>
                <span class="mt-1 pl-1 text-sm text-gray-500 border-solid border-2 border-indigo-100 bg-slate-200"
                  >-{{ productModal.discount }}%</span
                >
              </div>
              <p class="w-full text-sm text-gray-500">{{ productModal.description }}</p>
              <div class="flex items-align justify-between mt-5 w-full">
                <button
                  @click="deleteButtonClick"
                  class="bg-red-600 border border-transparent rounded-md py-3 px-5 flex items-center justify-center text-base font-medium text-white hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
                >
                  <ion-icon name="trash-outline" class="text-xl"></ion-icon>
                  <span class="ml-5">Delete</span>
                </button>
                <button
                  @click="editButtonClick"
                  class="bg-indigo-600 border border-transparent rounded-md py-3 px-5 flex items-center justify-center text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  <ion-icon name="create-outline" class="text-xl"></ion-icon>
                  <span class="ml-5">Edit</span>
                </button>
                <button
                  @click="orderButtonClick"
                  class="bg-indigo-600 border border-transparent rounded-md py-3 px-5 flex items-center justify-center text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  <ion-icon name="cart-outline" class="text-xl"></ion-icon>
                  <span class="ml-5">Order</span>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <tutorial-product-modification :product="{ ...productModal }" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { PRODUCT_GETTERS, PRODUCT_ACTIONS } from '@store/product'
import { BILL_ACTIONS } from '@store/bill'

export default Vue.extend({
  computed: {
    ...mapGetters({
      isShowProductModal: COMMON_GETTERS.GET_IS_SHOW_PRODUCT_MODAL,
      productModal: PRODUCT_GETTERS.GET_PRODUCT_MODAL,
      orderProducts: PRODUCT_GETTERS.GET_ORDER_PRODUCTS,
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
  },
})
</script>
