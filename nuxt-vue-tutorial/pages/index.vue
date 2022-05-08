<template>
  <div>
    <Header />
    <ProductBoard />

    <button @click="addProductClick" class="fixed w-16 h-16 bottom-12 right-12 bg-indigo-600 rounded-full z-50 zoom">
      <ion-icon name="create-outline" class="text-3xl text-white"></ion-icon>
    </button>

    <EditProduct :isEdit="false" :product="newProduct" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Header from '@/components/Header.vue'
import ProductBoard from '@/components/ProductBoard.vue'
import EditProduct from '@/components/EditProduct.vue'

import { mapActions, mapState, mapMutations } from 'vuex'
import { ACTIONS } from '~/store/actions'
import { MUTATIONS } from '~/store/mutations'

export default Vue.extend({
  data() {
    return {
      newProduct: {
        name: '',
        description: '',
        price: 0,
        discount: 0,
        image: null,
      },
    }
  },
  components: {
    Header,
    ProductBoard,
    EditProduct,
  },
  methods: {
    ...mapActions({
      fetchProducts: ACTIONS.FETCH_PRODUCTS,
    }),
    ...mapMutations({
      showEditModal: MUTATIONS.SHOW_EDIT_MODAL,
    }),

    addProductClick() {
      this.showEditModal(true)
    },
  },
  computed: {
    ...mapState(['products']),
  },
  mounted() {
    if (!this.products || this.products.length === 0) {
      this.fetchProducts()
    }
  },
})
</script>

<style scoped>
.zoom {
  transition: transform 0.2s;
}

.zoom:hover {
  transform: scale(1.2);
}
</style>
