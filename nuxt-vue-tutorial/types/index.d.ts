import { AppService } from '@services'

declare module 'vuex/types/index' {
  interface Store<S> {
    $api: AppService
  }
}
