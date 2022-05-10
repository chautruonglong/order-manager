import { MutationTree } from 'vuex/types'
import { AuthState, AUTH_NAMESPACED } from '@store/auth'

export interface AuthMutation {
  MUTATE_IS_AUTHENTICATED: string
}

export const AUTH_MUTATIONS: { [k: string]: string } & AuthMutation = {
  MUTATE_IS_AUTHENTICATED: 'MUTATE_IS_AUTHENTICATED',
}

const authMutations: MutationTree<AuthState> = {
  [AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED](state, isAuthenticated: boolean): void {
    state.isAuthenticated = isAuthenticated
  },
}

Object.keys(AUTH_MUTATIONS).forEach((key) => (AUTH_MUTATIONS[key] = `${AUTH_NAMESPACED}/${AUTH_MUTATIONS[key]}`))

export default authMutations
