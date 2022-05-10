<template>
  <div class="flex py-6 overflow-x-hidden">
    <div class="h-24 w-24 flex-shrink-0 overflow-hidden rounded-md border border-gray-200">
      <img :src="order.product.image" alt="" class="h-full w-full object-cover object-center" />
    </div>

    <div class="ml-4 flex flex-1 flex-col">
      <div>
        <div class="flex justify-between text-base font-medium text-gray-900">
          <h3>
            <a href="#">{{ order.product.name }}</a>
          </h3>
          <div class="ml-4 flex flex-col items-end">
            <p class="m-0">
              {{ order.product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
            </p>
            <p class="text-sm m-0 text-gray-500">-{{ order.product.discount }}%</p>
            <p class="m-0">Qty {{ order.quantity }}</p>
          </div>
        </div>
      </div>
      <div class="flex items-center justify-between text-sm w-full">
        <button @click="removeButtonClick" class="font-medium text-indigo-600 hover:scale-110 hover:text-indigo-500">
          Remove
        </button>
        <div class="flex">
          <button @click="subButtonClick" class="hover:scale-110">
            <ion-icon name="remove-circle-outline" class="w-6 h-6"></ion-icon>
          </button>
          <button @click="addButtonClick" class="hover:scale-110">
            <ion-icon name="add-circle-outline" class="w-6 h-6"></ion-icon>
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
