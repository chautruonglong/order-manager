<template>
  <div @click="buttonClick" class="group relative shadow-inner cursor-pointer">
    <div
      class="w-full min-h-80 bg-gray-200 aspect-w-1 aspect-h-1 rounded-md overflow-hidden group-hover:opacity-75 lg:h-80 lg:aspect-none"
    >
      <img :src="product.image" alt="" class="w-full h-full object-center object-cover lg:w-full lg:h-full" />
    </div>
    <div class="mt-4 flex flex-col justify-between">
      <div>
        <span class="text-lg font-medium text-gray-900">
          {{ product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
        </span>
      </div>

      <div>
        <span class="mt-1 text-sm text-gray-500 border-solid border-2 border-indigo-100 bg-slate-200"
          >-{{ product.discount }}%</span
        >
      </div>

      <div>
        <h3 class="text-sm text-gray-700">
          {{ product.name }}
        </h3>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations } from 'vuex'
import { COMMON_MUTATIONS } from '@store/common'
import { PRODUCT_MUTATIONS } from '@store/product'

export default Vue.extend({
  props: {
    product: Object,
  },
  methods: {
    ...mapMutations({
      mutateIsShowProductModal: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_MODAL,
      mutateProductModal: PRODUCT_MUTATIONS.MUTATE_PRODUCT_MODAL,
    }),
    buttonClick() {
      this.mutateProductModal({ ...this.product })
      this.mutateIsShowProductModal(true)
    },
  },
})
</script>
