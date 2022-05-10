<template>
  <div
    v-show="isShowProductCreation === true"
    class="tw-fixed tw-z-10 tw-inset-0 tw-overflow-y-auto"
    role="dialog"
    aria-modal="true"
  >
    <div class="tw-flex tw-min-h-screen tw-text-center md:tw-block md:tw-px-2 lg:tw-px-4" style="font-size: 0">
      <div
        class="tw-hidden tw-fixed tw-inset-0 tw-bg-gray-500 tw-bg-opacity-75 tw-transition-opacity md:tw-block"
        aria-hidden="true"
      ></div>
      <span class="tw-hidden md:tw-inline-block md:tw-align-middle md:tw-h-screen" aria-hidden="true">&#8203;</span>

      <div
        class="tw-flex tw-text-base tw-text-left tw-transform tw-transition tw-w-full md:tw-inline-block md:tw-max-w-2xl md:tw-px-4 md:tw-my-8 md:tw-align-middle lg:tw-max-w-4xl"
      >
        <div
          class="tw-flex tw-flex-col tw-justify-start tw-w-full tw-items-center tw-rounded-3xl tw-bg-white tw-px-4 tw-pt-14 tw-pb-8 tw-overflow-hidden tw-shadow-2xl sm:tw-px-6 sm:tw-pt-8 md:tw-p-6 lg:tw-p-8"
        >
          <div class="tw-flex tw-items-center tw-justify-between tw-w-full tw-mb-3">
            <p class="tw-text-2xl tw-font-extrabold tw-text-indigo-600">Create New Product</p>
            <button @click="closeButtonClick" type="button" class="tw-text-gray-400 hover:tw-text-gray-500">
              <ion-icon name="close" size="large"></ion-icon>
            </button>
          </div>

          <div class="tw-w-full">
            <div class="tw-w-full tw-mb-3">
              <label class="tw-block tw-uppercase tw-tracking-wide tw-text-gray-700 tw-text-xs tw-font-bold tw-mb-1">
                Name
              </label>
              <input
                class="tw-appearance-none tw-block tw-w-full tw-bg-gray-200 tw-text-gray-700 tw-border tw-rounded tw-py-3 tw-px-4 tw-mb-1 tw-leading-tight focus:tw-outline-none focus:tw-bg-white"
                type="text"
                v-model="product.name"
              />
            </div>

            <div class="tw-flex tw-items-center tw-justify-between tw-w-full tw-mb-3">
              <div class="tw-w-1/2 tw-mr-5">
                <label class="tw-block tw-uppercase tw-tracking-wide tw-text-gray-700 tw-text-xs tw-font-bold tw-mb-1">
                  Price (VNĐ)
                </label>
                <input
                  class="tw-appearance-none tw-block tw-w-full tw-bg-gray-200 tw-text-gray-700 tw-border tw-rounded tw-py-3 tw-px-4 tw-mb-1 tw-leading-tight focus:tw-outline-none focus:tw-bg-white"
                  type="text"
                  v-model="product.price"
                />
              </div>
              <div class="tw-w-1/2 tw-ml-5">
                <label class="tw-block tw-uppercase tw-tracking-wide tw-text-gray-700 tw-text-xs tw-font-bold tw-mb-1">
                  Discount (%)
                </label>
                <input
                  class="tw-appearance-none tw-block tw-w-full tw-bg-gray-200 tw-text-gray-700 tw-border tw-rounded tw-py-3 tw-px-4 tw-mb-1 tw-leading-tight focus:tw-outline-none focus:tw-bg-white"
                  type="text"
                  v-model="product.discount"
                />
              </div>
            </div>

            <div class="tw-w-full tw-mb-3">
              <label class="tw-block tw-uppercase tw-tracking-wide tw-text-gray-700 tw-text-xs tw-font-bold tw-mb-1">
                Description
              </label>
              <textarea
                class="tw-appearance-none tw-block tw-w-full tw-bg-gray-200 tw-text-gray-700 tw-border tw-rounded tw-py-3 tw-px-4 tw-mb-1 tw-leading-tight focus:tw-outline-none focus:tw-bg-white"
                placeholder="Description"
                v-model="product.description"
              ></textarea>
            </div>

            <div class="tw-flex tw-justify-start tw-w-full tw-mb-3">
              <div class="tw-w-96">
                <label class="tw-block tw-uppercase tw-tracking-wide tw-text-gray-700 tw-text-xs tw-font-bold tw-mb-1">
                  Image
                </label>
                <input
                  ref="fileImageCreation"
                  class="form-control tw-block tw-w-full tw-px-3 tw-py-1.5 tw-text-base tw-font-normal tw-text-gray-700 tw-bg-white tw-bg-clip-padding tw-border tw-border-solid tw-border-gray-300 tw-rounded tw-transition tw-ease-in-out tw-m-0 focus:tw-text-gray-700 focus:tw-bg-white focus:tw-border-blue-600 focus:tw-outline-none"
                  type="file"
                  @change="onFileChange"
                />
              </div>
            </div>

            <div class="tw-flex tw-justify-end tw-w-full">
              <button
                @click="postButtonClick"
                class="tw-w-full tw-bg-indigo-600 tw-border tw-border-transparent tw-rounded-md tw-py-3 tw-px-8 tw-flex tw-items-center tw-justify-center tw-text-base tw-font-medium tw-text-white hover:tw-bg-indigo-700 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-indigo-500"
              >
                <ion-icon name="arrow-up-outline" class="tw-text-xl"></ion-icon>
                <span class="tw-ml-5">Post</span>
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

        const input = this.$refs.fileImageCreation as HTMLInputElement
        input.value = ''
      }
    },
  },
})
</script>
