<template>
  <div @click="buttonClick" class="group tw-relative tw-shadow-inner tw-cursor-pointer">
    <div
      class="tw-w-full min-h-80 tw-bg-gray-200 aspect-w-1 aspect-h-1 tw-rounded-md tw-overflow-hidden group-hover:tw-opacity-75 lg:tw-h-80 lg:aspect-none"
    >
      <img :src="product.image" alt="" class="tw-w-full tw-h-full tw-object-center tw-object-cover lg:tw-w-full lg:tw-h-full" />
    </div>
    <div class="tw-mt-4 tw-flex tw-flex-col tw-justify-between">
      <div>
        <span class="tw-text-lg tw-font-medium tw-text-gray-900">
          {{ product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}
        </span>
      </div>

      <div>
        <span class="tw-mt-1 tw-text-sm tw-text-gray-500 tw-border-solid tw-border-2 tw-border-indigo-100 bg-slate-200"
          >-{{ product.discount }}%</span
        >
      </div>

      <div>
        <h3 class="tw-text-sm tw-text-gray-700">
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
