<template>
  <div class="relative inline-block text-left z-10">
    <a
      @click="isShow = !isShow"
      class="inline-flex justify-center items-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-indigo-500"
    >
      {{ currentOrder }}
      <ion-icon name="chevron-down-outline"></ion-icon>
    </a>

    <transition>
      <div
        v-show="isShow"
        class="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none"
      >
        <a
          @click="orderClick(item)"
          v-for="item in items"
          :key="item"
          class="text-gray-700 block px-4 py-2 text-sm hover:bg-gray-300"
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
