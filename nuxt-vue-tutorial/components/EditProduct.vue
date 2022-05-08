<template>
  <div v-show="isEditModal == true" class="fixed z-10 inset-0 overflow-y-auto" role="dialog" aria-modal="true">
    <div class="flex min-h-screen text-center md:block md:px-2 lg:px-4" style="font-size: 0">
      <div class="hidden fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity md:block" aria-hidden="true"></div>
      <span class="hidden md:inline-block md:align-middle md:h-screen" aria-hidden="true">&#8203;</span>

      <div class="flex text-base text-left transform transition w-full md:inline-block md:max-w-2xl md:px-4 md:my-8 md:align-middle lg:max-w-4xl">
        <div
          class="flex flex-col justify-start w-full items-center bg-white px-4 pt-14 pb-8 overflow-hidden shadow-2xl sm:px-6 sm:pt-8 md:p-6 lg:p-8"
        >
          <div class="flex items-center justify-between w-full mb-3">
            <p class="text-2xl font-extrabold text-indigo-600">{{ isEdit ? 'Update Product' : 'Create New Product' }}</p>
            <button @click="closeModal" type="button" class="text-gray-400 hover:text-gray-500">
              <svg class="h-6 w-6" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor" aria-hidden="true">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
              </svg>
            </button>
          </div>

          <div class="w-full">
            <div class="w-full mb-3">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1" for="grid-first-name-u"> Name </label>
              <input
                name="name"
                class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                type="text"
                v-model="product.name"
              />
            </div>

            <div class="flex items-center justify-between w-full mb-3">
              <div class="w-1/2 mr-5">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1" for="grid-first-price-u"> Price (VNĐ) </label>
                <input
                  name="price"
                  class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                  type="text"
                  v-model="product.price"
                />
              </div>
              <div class="w-1/2 ml-5">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1" for="grid-first-discount-u"> Discount (%) </label>
                <input
                  name="discount"
                  class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                  type="text"
                  v-model="product.discount"
                />
              </div>
            </div>

            <div class="w-full mb-3">
              <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1" for="grid-first-description-u"> Description </label>
              <textarea
                name="description"
                class="appearance-none block w-full bg-gray-200 text-gray-700 border rounded py-3 px-4 mb-1 leading-tight focus:outline-none focus:bg-white"
                placeholder="Description"
                v-model="product.description"
              ></textarea>
            </div>

            <div class="flex justify-start w-full mb-3">
              <div class="w-96">
                <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-1" for="grid-first-thumbnailImage-u"> Image </label>
                <input
                  name="image"
                  class="form-control block w-full px-3 py-1.5 text-base font-normal text-gray-700 bg-white bg-clip-padding border border-solid border-gray-300 rounded transition ease-in-out m-0 focus:text-gray-700 focus:bg-white focus:border-blue-600 focus:outline-none"
                  type="file"
                  @change="onFileChange"
                />
              </div>
            </div>

            <div class="flex justify-end w-full">
              <button
                @click="updateClick"
                class="w-full bg-indigo-600 border border-transparent rounded-md py-3 px-8 flex items-center justify-center text-base font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                <ion-icon name="arrow-up-outline" class="text-xl"></ion-icon>
                <span class="ml-5">{{ isEdit ? 'Update' : 'Post' }}</span>
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
import { mapState, mapMutations, mapActions } from 'vuex'
import { ACTIONS } from '~/store/actions'
import { MUTATIONS } from '~/store/mutations'

export default Vue.extend({
  props: {
    product: Object,
    isEdit: Boolean,
  },
  computed: {
    ...mapState(['isEditModal']),
  },
  methods: {
    ...mapMutations({
      showEditModal: MUTATIONS.SHOW_EDIT_MODAL,
    }),
    ...mapActions({
      updateProduct: ACTIONS.UPDATE_PRODUCT,
      createProduct: ACTIONS.CREATE_PRODUCT,
    }),
    closeModal() {
      this.showEditModal(false)
    },
    onFileChange(e: any) {
      var files = e.target.files || e.dataTransfer.files
      if (!files.length) return
      this.product.image = files[0]
    },
    updateClick() {
      const formData = new FormData()
      formData.append('name', this.product.name)
      formData.append('description', this.product.description)
      formData.append('price', this.product.price)
      formData.append('discount', this.product.discount)
      formData.append('image', this.product.image)

      if (this.isEdit) {
        this.updateProduct({ id: this.product.id, payload: formData })
      } else {
        this.createProduct(formData)
      }
    },
  },
})
</script>
