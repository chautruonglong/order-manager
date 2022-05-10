<template>
  <transition>
    <div v-show="isShowCart" @click. class="relative z-50" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
      <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></div>
      <div class="fixed inset-0 overflow-hidden">
        <div class="absolute inset-0 overflow-hidden">
          <div class="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
            <div class="pointer-events-auto w-screen max-w-md">
              <div class="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                <div class="flex-1 py-6 px-4 sm:px-6">
                  <div class="flex items-start justify-between">
                    <h2 class="text-lg font-medium text-gray-900" id="slide-over-title">Shopping cart</h2>
                    <div class="ml-3 flex h-7 items-center">
                      <button @click="closeButtonClick" class="-m-2 p-2 text-gray-400 hover:text-gray-500">
                        <ion-icon name="close" size="large"></ion-icon>
                      </button>
                    </div>
                  </div>

                  <div class="mt-8">
                    <div class="flow-root">
                      <ul role="list" class="-my-6 divide-y divide-gray-200 overflow-auto">
                        <template v-if="localBill != null">
                          <li v-for="(order, index) in localBill.orders" :key="index">
                            <tutorial-cart-item :order="order" />
                          </li>
                        </template>
                      </ul>
                    </div>
                  </div>
                </div>

                <div class="border-t border-gray-200 py-6 px-4 sm:px-6">
                  <div class="flex justify-between text-base font-medium text-gray-900">
                    <p>Total</p>
                    <p v-if="localBill != null">
                      {{ localBill.total.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
                    </p>
                  </div>
                  <div class="mt-6">
                    <a
                      href="#"
                      class="flex items-center justify-center rounded-md border border-transparent bg-indigo-600 px-6 py-3 text-base font-medium text-white shadow-sm hover:bg-indigo-700"
                      >Order</a
                    >
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { BILL_GETTERS, BILL_ACTIONS } from '@store/bill'

export default Vue.extend({
  data() {
    return {
      active: false,
    }
  },
  computed: {
    ...mapGetters({
      isShowCart: COMMON_GETTERS.GET_IS_SHOW_CART,
      localBill: BILL_GETTERS.GET_LOCAL_BILL,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsShowCart: COMMON_MUTATIONS.MUTATE_IS_SHOW_CART,
    }),

    ...mapActions({
      removeProductFromCart: BILL_ACTIONS.REMOVE_PRODUCT_FROM_CART,
    }),

    closeButtonClick() {
      this.mutateIsShowCart(false)
    },
  },
})
</script>

<style scoped>
.slide-fade-enter-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-leave-active {
  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
}
.slide-fade-enter,
.slide-fade-leave-to {
  transform: translateX(100%);
  opacity: 0;
}
</style>
