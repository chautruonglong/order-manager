import { Services } from '~/plugins/api.plugin'

declare module 'vuex/types/index' {
  interface Store<S> {
    $api: Services
  }
}
