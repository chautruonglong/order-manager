<template>
  <transition>
    <div v-show="isShowCart" class="relative z-50" aria-labelledby="slide-over-title" role="dialog" aria-modal="true">
      <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"></div>
      <div class="fixed inset-0 overflow-hidden">
        <div class="absolute inset-0 overflow-hidden">
          <div class="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10">
            <div class="pointer-events-auto w-screen max-w-md">
              <div class="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                <div class="flex-1 overflow-y-auto py-6 px-4 sm:px-6">
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
                      <ul role="list" class="-my-6 divide-y divide-gray-200">
                        <template v-for="(product, index) in orderProducts">
                          <li :key="index" class="flex py-6">
                            <div class="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
                              <img :src="product.image" alt="" class="h-full w-full object-cover object-center" />
                            </div>

                            <div class="ml-4 flex flex-1 flex-col">
                              <div>
                                <div class="flex justify-between text-base font-medium text-gray-900">
                                  <h3>
                                    <a href="#">{{ product.name }}</a>
                                  </h3>
                                  <p class="ml-4">
                                    {{ product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
                                  </p>
                                </div>
                                <p class="mt-1 text-sm text-gray-500">-{{ product.discount }}%</p>
                              </div>
                              <div class="flex flex-1 items-end justify-between text-sm">
                                <p class="text-gray-500">Qty 1</p>

                                <div class="flex">
                                  <button type="button" class="font-medium text-indigo-600 hover:text-indigo-500">Remove</button>
                                </div>
                              </div>
                            </div>
                          </li>
                        </template>
                      </ul>
                    </div>
                  </div>
                </div>

                <div class="border-t border-gray-200 py-6 px-4 sm:px-6">
                  <div class="flex justify-between text-base font-medium text-gray-900">
                    <p>Subtotal</p>
                    <p>{{ sumBill.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}</p>
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
import { mapGetters, mapMutations } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { PRODUCT_GETTERS } from '@store/product'
import { Product } from '@models'

export default Vue.extend({
  data() {
    return {
      active: false,
    }
  },
  computed: {
    ...mapGetters({
      isShowCart: COMMON_GETTERS.GET_IS_SHOW_CART,
      orderProducts: PRODUCT_GETTERS.GET_ORDER_PRODUCTS,
    }),

    sumBill() {
      let sum = 0
      this.orderProducts.forEach((p: Product) => (sum += p.price))
      return sum
    },
  },
  methods: {
    ...mapMutations({
      mutateIsShowCart: COMMON_MUTATIONS.MUTATE_IS_SHOW_CART,
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
