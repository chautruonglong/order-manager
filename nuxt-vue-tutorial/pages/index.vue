<template>
  <div>
    <Header />
    <ProductBoard />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Header from '@/components/Header.vue'
import ProductBoard from '@/components/ProductBoard.vue'

import { mapActions } from 'vuex'
import { ACTIONS } from '~/store/actions'

export default Vue.extend({
  components: {
    Header,
    ProductBoard,
  },
  methods: {
    ...mapActions({
      fetchProducts: ACTIONS.FETCH_PRODUCTS,
    }),
  },
  created() {
    this.fetchProducts()
  },
})
</script>
