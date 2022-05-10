<template>
  <div v-show="isShowProductCreation === true" class="fixed z-10 inset-0 overflow-y-auto" role="dialog" aria-modal="true">
    <div class="flex min-h-screen text-center md:block md:px-2 lg:px-4" style="font-size: 0">
      <div class="hidden fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity md:block" aria-hidden="true"></div>
      <span class="hidden md:inline-block md:align-middle md:h-screen" aria-hidden="true">&#8203;</span>

      <div
        class="flex text-base text-left transform transition w-full md:inline-block md:max-w-2xl md:px-4 md:my-8 md:align-middle lg:max-w-4xl"
      >
        <div
          class="flex flex-col justify-start w-full items-center bg-white px-4 pt-14 pb-8 overflow-hidden shadow-2xl sm:px-6 sm:pt-8 md:p-6 lg:p-8"
        >
          <div class="flex items-center justify-between w-full mb-3">
            <p class="text-2xl font-extrabold text-indigo-600">Create New Product</p>
            <button @click="closeButtonClick" type="button" class="text-gray-400 hover:text-gray-500">
              <ion-icon name="close" size="large"></ion-icon>
            </button>
          </div>

          <div class="w-full">
            <div class="w-full mb-3">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1"> Name </label>
              <input
                class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                type="text"
                v-model="product.name"
              />
            </div>

            <div class="flex items-center justify-between w-full mb-3">
              <div class="w-1/2 mr-5">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1"> Price (VNĐ) </label>
                <input
                  class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                  type="text"
                  v-model="product.price"
                />
              </div>
              <div class="w-1/2 ml-5">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1"> Discount (%) </label>
                <input
                  class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                  type="text"
                  v-model="product.discount"
                />
              </div>
            </div>

            <div class="w-full mb-3">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1"> Description </label>
              <textarea
                class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                placeholder="Description"
                v-model="product.description"
              ></textarea>
            </div>

            <div class="flex justify-start w-full mb-3">
              <div class="w-96">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1"> Image </label>
                <input
                  class="form-control block w-full px-3 py-1.5 text-base font-normal text-gray-700 bg-white bg-clip-padding border border-solid border-gray-300 rounded transition ease-in-out m-0 focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
                  type="file"
                  @change="onFileChange"
                />
              </div>
            </div>

            <div class="flex justify-end w-full">
              <button
                @click="postButtonClick"
                class="w-full bg-indigo-600 border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                <ion-icon name="arrow-up-outline" class="text-xl"></ion-icon>
                <span class="ml-5">Post</span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { PRODUCT_ACTIONS } from '@store/product'
import { ProductCreation } from '@models'

let product: ProductCreation = {
  name: '',
  description: '',
  image: '',
  price: 0,
  discount: 0,
}

export default Vue.extend({
  data() {
    return {
      product,
    }
  },
  computed: {
    ...mapGetters({
      isShowProductCreation: COMMON_GETTERS.GET_IS_SHOW_PRODUCT_CREATION,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsShowProductCreation: COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_CREATION,
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
    }),

    ...mapActions({
      createProduct: PRODUCT_ACTIONS.CREATE_PRODUCT,
    }),

    closeButtonClick() {
      this.mutateIsShowProductCreation(false)
    },

    onFileChange(e: any) {
      var files = e.target.files || e.dataTransfer.files
      if (!files.length) return
      this.product.image = files[0]
    },

    async postButtonClick() {
      try {
        this.mutateIsLoading(true)
        const formData = new FormData()
        formData.append('name', this.product.name)
        formData.append('description', this.product.description)
        formData.append('price', this.product.price.toString())
        formData.append('discount', this.product.discount.toString())
        formData.append('image', this.product.image)
        await this.createProduct(formData)
        this.mutateSnackbar('Post new product successfully')
      } catch (e: any) {
        this.mutateSnackbar(e.message)
      } finally {
        this.mutateIsLoading(false)
        this.mutateIsShowProductCreation(false)
      }
    },
  },
})
</script>
