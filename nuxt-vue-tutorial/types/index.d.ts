import { Services } from '~/plugins/api.client'

declare module 'vuex/types/index' {
  interface Store<S> {
    $api: Services
  }
}
