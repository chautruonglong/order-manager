<template>
  <transition>
    <div
      v-show="isShowCart"
      @click.
      class="tw-relative tw-z-50"
      aria-labelledby="slide-over-title"
      role="dialog"
      aria-modal="true"
    >
      <div class="tw-fixed tw-inset-0 tw-bg-gray-500 tw-bg-opacity-75 tw-transition-opacity"></div>
      <div class="tw-fixed tw-inset-0 tw-overflow-hidden">
        <div class="tw-absolute tw-inset-0 tw-overflow-hidden">
          <div class="tw-pointer-events-none tw-fixed tw-inset-y-0 tw-right-0 tw-flex tw-max-w-full tw-pl-10">
            <div class="tw-pointer-events-auto tw-w-screen tw-max-w-md">
              <div class="tw-flex tw-h-full tw-flex-col tw-overflow-y-scroll tw-bg-white tw-shadow-xl">
                <div class="tw-flex-1 tw-py-6 tw-px-4 sm:tw-px-6">
                  <div class="tw-flex tw-items-start tw-justify-between">
                    <h2 class="tw-text-lg tw-font-medium tw-text-gray-900" id="slide-over-title">Shopping cart</h2>
                    <div class="tw-ml-3 tw-flex tw-h-7 tw-items-center">
                      <button @click="closeButtonClick" class="tw--m-2 tw-p-2 tw-text-gray-400 hover:tw-text-gray-500">
                        <ion-icon name="close" size="large"></ion-icon>
                      </button>
                    </div>
                  </div>

                  <div class="tw-mt-8">
                    <div class="tw-flow-root">
                      <ul role="list" class="tw--my-6 tw-divide-y tw-divide-gray-200 tw-overflow-auto">
                        <template v-if="localBill != null">
                          <li v-for="(order, index) in localBill.orders" :key="index">
                            <tutorial-cart-item :order="order" />
                          </li>
                        </template>
                      </ul>
                    </div>
                  </div>
                </div>

                <div class="tw-border-t tw-border-gray-200 tw-py-6 tw-px-4 sm:tw-px-6">
                  <div class="tw-flex tw-justify-between tw-text-base tw-font-medium tw-text-gray-900">
                    <p>Total</p>
                    <p v-if="localBill != null">
                      {{ localBill.total.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
                    </p>
                  </div>
                  <div class="tw-mt-6">
                    <a
                      @click="orderButtonClick"
                      class="tw-flex tw-items-center tw-justify-center tw-rounded-md tw-border tw-border-transparent tw-bg-indigo-600 tw-px-6 tw-py-3 tw-text-base tw-font-medium tw-text-white tw-shadow-sm hover:tw-bg-indigo-700"
                      >Order</a
                    >
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <tutorial-confirm ref="confirmDialog" />
    </div>
  </transition>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { BILL_GETTERS, BILL_ACTIONS, BILL_MUTATIONS } from '@store/bill'

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
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateLocalBill: BILL_MUTATIONS.MUTATE_LOCAL_BILL,
    }),

    ...mapActions({
      removeProductFromCart: BILL_ACTIONS.REMOVE_PRODUCT_FROM_CART,
      createBill: BILL_ACTIONS.CREATE_BILL,
    }),

    closeButtonClick() {
      this.mutateIsShowCart(false)
    },

    async orderButtonClick() {
      if (await (this.$refs.confirmDialog as any).active()) {
        try {
          this.mutateIsLoading(true)
          await this.createBill(this.localBill)
          this.mutateSnackbar('Order successfully')
          this.mutateLocalBill(null)
        } catch (e: any) {
          this.mutateSnackbar(e.message)
        } finally {
          this.mutateIsLoading(false)
        }
      }
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
