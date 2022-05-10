<template>
  <div class="tw-relative tw-inline-block tw-text-left tw-z-10">
    <a
      @click="isShow = !isShow"
      class="tw-inline-flex tw-justify-center tw-items-center tw-w-full tw-rounded-md tw-border tw-border-gray-300 tw-shadow-sm tw-px-4 tw-py-2 tw-bg-white tw-text-sm tw-font-medium tw-text-gray-700 hover:tw-bg-gray-50 focus:tw-outline-none focus:tw-ring-2 focus:tw-ring-offset-2 focus:tw-ring-offset-gray-100 focus:tw-ring-indigo-500"
    >
      {{ currentOrder }}
      <ion-icon name="chevron-down-outline"></ion-icon>
    </a>

    <transition>
      <div
        v-show="isShow"
        class="tw-origin-top-right tw-absolute tw-right-0 tw-mt-2 tw-w-56 tw-rounded-md tw-shadow-lg tw-bg-white tw-ring-1 tw-ring-black tw-ring-opacity-5 focus:tw-outline-none"
      >
        <a
          @click="orderClick(item)"
          v-for="item in items"
          :key="item"
          class="tw-text-gray-700 tw-block tw-px-4 tw-py-2 tw-text-sm hover:tw-bg-gray-300"
        >
          {{ item }}
        </a>
      </div>
    </transition>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  props: {
    items: Array,
    itemClick: Function,
  },
  data() {
    return {
      isShow: false,
      currentOrder: this.items[0],
    }
  },
  methods: {
    orderClick(current: string) {
      this.isShow = !this.isShow
      this.currentOrder = current
      this.itemClick(current)
    },

    hideMenu(e: any) {
      if (!this.$el.contains(e.target)) {
        this.isShow = false
      }
    },
  },
  mounted() {
    document.addEventListener('click', this.hideMenu)
  },
})
</script>
