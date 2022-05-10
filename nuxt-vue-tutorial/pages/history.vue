<template>
  <div class="tw-mt-8 tw-w-3/5 tw-mx-auto">
    <ul class="tw-my-6 tw-divide-y tw-divide-gray-200">
      <template v-for="(bill, index) in historyBills">
        <li :key="index" class="tw-flex tw-py-6">
          <div class="tw-h-24 tw-w-24 tw-flex-shrink-0 tw-overflow-hidden tw-rounded-md tw-border tw-border-gray-200">
            <img :src="bill.orders[0].product.image" alt="" class="tw-h-full tw-w-full tw-object-cover tw-object-center" />
          </div>

          <div class="tw-ml-4 tw-flex tw-flex-1 tw-flex-col">
            <div>
              <div class="tw-flex tw-justify-between tw-text-base tw-font-medium tw-text-gray-900">
                <h3>
                  <a>{{ bill.orders[0].product.name }}</a>
                </h3>
                <div class="tw-flex tw-flex-col tw-justify-end tw-items-end">
                  <p class="tw-ml-4">{{ bill.total }}</p>
                  <span
                    class="tw-mt-1 tw-pl-1 tw-text-sm tw-text-gray-500 tw-border-solid tw-border-2 tw-border-indigo-100 bg-slate-200"
                    >{{ bill.orders[0].quantity }}</span
                  >
                </div>
              </div>
            </div>
            <div class="tw-flex tw-flex-1 tw-items-end tw-justify-between tw-text-sm">
              <p class="tw-text-gray-500">{{ bill.createdAt }}</p>

              <div class="tw-flex">
                <button
                  name="removeOrderItemButton"
                  type="button"
                  class="tw-font-medium tw-text-indigo-600 hover:tw-text-indigo-500"
                >
                  Remove
                </button>
              </div>
            </div>
          </div>
        </li>
      </template>
    </ul>

    <div @click="moreButtonClick" class="tw-flex tw-justify-center tw-mb-3 tw-rounded-md tw-shadow">
      <button
        class="tw-flex tw-items-center tw-px-5 tw-py-3 tw-border tw-border-transparent tw-text-base tw-font-medium tw-rounded-md tw-text-white tw-bg-indigo-600 hover:tw-bg-indigo-700"
      >
        <v-progress-circular
          v-if="isMoreClickLoading === true"
          indeterminate
          color="white"
          class="tw-h-6 tw-w-6"
        ></v-progress-circular>
        <ion-icon v-else name="arrow-down-circle-outline" class="tw-h-6 tw-w-6 tw-font-bold"></ion-icon>
        <span class="tw-ml-3">More</span>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_MUTATIONS } from '@store/common'
import { BILL_ACTIONS, BILL_GETTERS, BILL_MUTATIONS } from '@store/bill'

export default Vue.extend({
  data() {
    return {
      isMoreClickLoading: false,
    }
  },
  computed: {
    ...mapGetters({
      historyBills: BILL_GETTERS.GET_HISTORY_BILLS,
      page: BILL_GETTERS.GET_PAGE,
    }),
  },
  methods: {
    ...mapMutations({
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
      mutatePage: BILL_MUTATIONS.MUTATE_PAGE,
    }),

    ...mapActions({
      fetchBills: BILL_ACTIONS.FETCH_BILLS,
    }),
    async moreButtonClick() {
      this.isMoreClickLoading = true
      this.mutatePage(this.page + 1)
      await this.fetchBills()
      this.isMoreClickLoading = false
    },
  },
  async mounted() {
    try {
      this.mutateIsLoading(true)
      await this.fetchBills()
    } catch (e: any) {
      this.mutateSnackbar(e.message)
    } finally {
      this.mutateIsLoading(false)
    }
  },
})
</script>
