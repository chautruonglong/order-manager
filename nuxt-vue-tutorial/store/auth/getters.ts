import { GetterTree } from 'vuex/types'
import { AuthState, AUTH_NAMESPACED } from '@store/auth'

export interface AuthGetter {
  GET_IS_AUTHENTICATED: string
}

export const AUTH_GETTERS: { [k: string]: string } & AuthGetter = {
  GET_IS_AUTHENTICATED: 'GET_IS_AUTHENTICATED',
}

const authGetters: GetterTree<AuthState, AuthState> = {
  [AUTH_GETTERS.GET_IS_AUTHENTICATED](state): boolean {
    return state.isAuthenticated
  },
}

Object.keys(AUTH_GETTERS).forEach((key) => (AUTH_GETTERS[key] = `${AUTH_NAMESPACED}/${AUTH_GETTERS[key]}`))

export default authGetters
