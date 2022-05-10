<template>
  <v-app>
    <v-progress-linear v-show="isLoading" indeterminate color="indigo" class="tw-z-50 tw-absolute tw-top-0"> </v-progress-linear>
    <tutorial-header v-if="isHaveHeader === true" />
    <nuxt />
    <v-snackbar
      v-model="activeSnackbar"
      :timeout="timeoutSnackbar"
      :value="true"
      tw-absolute
      centered
      right
      top
      color="deep-purple accent-4"
      elevation="24"
      class="tw-top-20 tw-right-5"
    >
      {{ messageSnackbar }}
      <template v-slot:action="{ attrs }">
        <v-btn color="blue" text v-bind="attrs" @click="activeSnackbar = false">Close</v-btn>
      </template>
    </v-snackbar>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue'
import { mapGetters, mapMutations, mapActions } from 'vuex'
import { COMMON_GETTERS, COMMON_MUTATIONS } from '@store/common'
import { USER_ACTIONS } from '@store/user'

export default Vue.extend({
  data() {
    return {
      activeSnackbar: false,
      timeoutSnackbar: 3000,
      messageSnackbar: '',
    }
  },
  computed: {
    ...mapGetters({
      isLoading: COMMON_GETTERS.GET_IS_LOADING,
      snackbar: COMMON_GETTERS.GET_SNACKBAR,
    }),

    isHaveHeader() {
      if (this.$route.path === '/login') {
        return false
      }
      return true
    },
  },
  methods: {
    ...mapMutations({
      mutateSnackbar: COMMON_MUTATIONS.MUTATE_SNACKBAR,
      mutateIsLoading: COMMON_MUTATIONS.MUTATE_IS_LOADING,
    }),

    ...mapActions({
      fetchUser: USER_ACTIONS.FETCH_USER,
    }),
  },
  async created() {
    try {
      this.mutateIsLoading(true)

      this.$store.subscribe((mutation, state) => {
        if (mutation.type === COMMON_MUTATIONS.MUTATE_SNACKBAR) {
          this.activeSnackbar = state.common.snackbar.active
          this.timeoutSnackbar = state.common.snackbar.timeout
          this.messageSnackbar = state.common.snackbar.message
        }
      })

      await this.fetchUser()
    } catch (e: any) {
      this.mutateSnackbar(e.message)
    } finally {
      this.mutateIsLoading(false)
    }
  },
})
</script>

<style>
.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>
