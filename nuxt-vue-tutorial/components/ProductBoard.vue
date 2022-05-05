<template>
  <div class="bg-white">
    <div class="max-w-2xl mx-auto py-16 px-4 sm:py-24 sm:px-6 lg:max-w-7xl lg:px-8">
      <h2 class="text-2xl font-extrabold tracking-tight text-gray-900">Customers also purchased</h2>

      <div class="mt-6 grid grid-cols-1 gap-y-10 gap-x-6 sm:grid-cols-2 lg:grid-cols-4 xl:gap-x-8">
        <div @click="productClick(product)" v-for="product in products" :key="product.id" class="group relative cursor-pointer">
          <div class="w-full min-h-80 bg-gray-200 aspect-w-1 aspect-h-1 rounded-md overflow-hidden group-hover:opacity-75 lg:h-80 lg:aspect-none">
            <img :src="product.image" alt="" class="w-full h-full object-center object-cover lg:w-full lg:h-full" />
          </div>
          <div class="mt-4 flex justify-between flex-col">
            <p class="text-lg font-medium text-gray-900">{{ product.price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }) }}</p>
            <div>
              <span class="mt-1 pl-1 text-sm text-gray-500 border-solid border-2 border-indigo-100 bg-slate-200">{{ `-${product.discount}%` }}</span>
            </div>
            <h3 class="text-sm text-gray-700">
              <p>{{ product.name }}</p>
            </h3>
          </div>
        </div>
      </div>
    </div>

    <div @click="moreClick" class="flex justify-center mb-3 rounded-md shadow">
      <svg class="animate-spin h-5 w-5 mr-3" viewBox="0 0 24 24"></svg>
      <button class="px-5 py-3 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700">More</button>
    </div>

    <ProductModal :product="productDetail" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapState, mapMutations, mapActions } from 'vuex'
import { Product } from '~/models/product.models'
import { ACTIONS } from '~/store/actions'
import { MUTATIONS } from '~/store/mutations'
import ProductModal from './ProductModal.vue'

export default Vue.extend({
  data() {
    interface DataType {
      productDetail: Product | null
    }

    const d: DataType = {
      productDetail: null,
    }
    return d
  },
  components: {
    ProductModal,
  },
  computed: {
    ...mapState(['products', 'page']),
  },
  methods: {
    ...mapMutations({
      setPage: MUTATIONS.SET_PAGE,
      showProductModal: MUTATIONS.SHOW_PRODUCT_MODAL,
    }),
    ...mapActions({
      fetchProducts: ACTIONS.FETCH_PRODUCTS,
    }),

    moreClick() {
      this.setPage(this.page + 1)
      this.fetchProducts()
    },

    productClick(product: Product) {
      this.productDetail = product
      this.showProductModal(true)
    },
  },
})
</script>
