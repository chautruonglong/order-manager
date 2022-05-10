<template>
  <v-dialog v-model="dialog" persistent max-width="290">
    <v-card>
      <v-card-title class="text-h5">Action Security</v-card-title>
      <v-card-text>Are you sure you want to continue?</v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="red lighten-2" text @click="_cancelButtonClick">Cancel</v-btn>
        <v-btn color="blue darken-2" text @click="_acceptButtonClick">Ok</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  data() {
    return {
      dialog: false,
      resolvePromise: (v: unknown): void => {},
    }
  },
  methods: {
    active() {
      this.dialog = true

      return new Promise((resolve) => {
        this.resolvePromise = resolve
      })
    },
    _acceptButtonClick() {
      this.dialog = false
      this.resolvePromise(true)
    },
    _cancelButtonClick() {
      this.dialog = false
      this.resolvePromise(false)
    },
  },
})
</script>
