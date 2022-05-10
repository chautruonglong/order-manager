<template>
  <div>
    <tutorial-products />
    <tutorial-product-creation />

    <button
      @click="addProductButtonClick"
      class="fixed w-16 h-16 bottom-12 right-12 bg-indigo-600 rounded-full z-40 hover:scale-110"
    >
      <ion-icon name="create-outline" class="text-3xl text-white"></ion-icon>
    </button>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapMutations } from 'vuex'
import { COMMON_MUTATIONS } from '@store/common'

export default Vue.extend({
  methods: {
    ...mapMutations({
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateIsShowProductCreation: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_CREATION,
    }),

    addProductButtonClick() {
      this.mutateIsShowProductCreation(true)
    },
  },
  mounted() {
    this.mutateIsLoading(false)
  },
})
</script>
