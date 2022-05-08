<template>
  <div class="relative inline-block text-left z-10">
    <button
      @click="isShow = !isShow"
      class="inline-flex justify-center items-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-indigo-500"
    >
      {{ currentOrder }}
      <ion-icon name="chevron-down-outline"></ion-icon>
    </button>

    <transition>
      <div
        v-show="isShow"
        class="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none"
      >
        <button @click="orderClick(item)" v-for="item in items" :key="item" class="text-gray-700 block px-4 py-2 text-sm">
          {{ item }}
        </button>
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
      currentOrder: this.items[1],
    }
  },
  methods: {
    orderClick(current: string) {
      this.isShow = !this.isShow
      this.currentOrder = current
      this.itemClick(current)
    },
  },
})
</script>

<style scoped>
.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>
