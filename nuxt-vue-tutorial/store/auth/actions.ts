import { ActionTree } from 'vuex/types'
import { AuthState, AUTH_NAMESPACED, AUTH_MUTATIONS } from '@store/auth'
import { AccessToken, Login, User, UserRegister } from '@models'
import { setAccessToken, setRefreshToken, clearTokens } from '@utils'

export interface AuthAction {
  LOGIN: string
  LOGOUT: string
  REGISTER: string
  REFRESH: string
}

export const AUTH_ACTIONS: { [k: string]: string } & AuthAction = {
  LOGIN: 'LOGIN',
  LOGOUT: 'LOGOUT',
  REGISTER: 'REGISTER',
  REFRESH: 'REFRESH',
}

const AuthActions: ActionTree<AuthState, AuthState> = {
  async [AUTH_ACTIONS.LOGIN]({ commit }, { email, password }: Login): Promise<AccessToken> {
    const accessTokens = await this.$api.auth.login({ email, password })
    setAccessToken(accessTokens.accessToken)
    setRefreshToken(accessTokens.refreshToken)
    commit(AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED, true, { root: true })
    return accessTokens
  },

  async [AUTH_ACTIONS.LOGOUT]({ commit }): Promise<void> {
    clearTokens()
    commit(AUTH_MUTATIONS.MUTATE_IS_AUTHENTICATED, false, { root: true })
  },

  async [AUTH_ACTIONS.REGISTER](_, user: UserRegister): Promise<User> {
    const $user = await this.$api.auth.register(user)
    return $user
  },

  async [AUTH_ACTIONS.REFRESH](): Promise<AccessToken> {
    const accessTokens = await this.$api.auth.refresh()
    setAccessToken(accessTokens.accessToken)
    setRefreshToken(accessTokens.refreshToken)
    return accessTokens
  },
}

Object.keys(AUTH_ACTIONS).forEach((key) => (AUTH_ACTIONS[key] = `${AUTH_NAMESPACED}/${AUTH_ACTIONS[key]}`))

export default AuthActions
