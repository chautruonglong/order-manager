<template>
  <div class="tw-flex tw-py-6 tw-overflow-x-hidden">
    <div class="tw-h-24 tw-w-24 tw-flex-shrink-0 tw-overflow-hidden tw-rounded-md tw-border tw-border-gray-200">
      <img :src="order.product.image" alt="" class="tw-h-full tw-w-full tw-object-cover tw-object-center" />
    </div>

    <div class="tw-ml-4 tw-flex tw-flex-1 tw-flex-col">
      <div>
        <div class="tw-flex tw-justify-between tw-text-base tw-font-medium tw-text-gray-900">
          <h3>
            <a href="#">{{ order.product.name }}</a>
          </h3>
          <div class="tw-ml-4 tw-flex tw-flex-col tw-items-end">
            <p class="tw-m-0">
              {{ order.product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
            </p>
            <p class="tw-text-sm tw-m-0 tw-text-gray-500">-{{ order.product.discount }}%</p>
            <p class="tw-m-0">Qty {{ order.quantity }}</p>
          </div>
        </div>
      </div>
      <div class="tw-flex tw-items-center tw-justify-between tw-text-sm tw-w-full">
        <button @click="removeButtonClick" class="tw-font-medium tw-text-indigo-600 hover:tw-scale-110 hover:tw-text-indigo-500">
          Remove
        </button>
        <div class="tw-flex">
          <button @click="subButtonClick" class="hover:tw-scale-110">
            <ion-icon name="remove-circle-outline" class="tw-w-6 tw-h-6"></ion-icon>
          </button>
          <button @click="addButtonClick" class="hover:tw-scale-110">
            <ion-icon name="add-circle-outline" class="tw-w-6 tw-h-6"></ion-icon>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapActions } from 'vuex'
import { BILL_ACTIONS } from '@store/bill'

export default Vue.extend({
  props: {
    order: Object,
  },
  methods: {
    ...mapActions({
      addProductToCard: BILL_ACTIONS.ADD_PRODUCT_TO_CART,
      subProductFromCard: BILL_ACTIONS.SUB_PRODUCT_FROM_CART,
      removeProductsFromCard: BILL_ACTIONS.REMOVE_PRODUCTS_FROM_CART,
    }),
    removeButtonClick() {
      this.removeProductsFromCard(this.order.product.id)
    },
    subButtonClick() {
      this.subProductFromCard(this.order.product.id)
    },
    addButtonClick() {
      this.addProductToCard(this.order.product)
    },
  },
})
</script>
