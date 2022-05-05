import { ActionTree } from 'vuex/types'
import { State } from './state'
import { ProductQueries } from '@/services/product.services'
import { Product } from '@/models/product.models'
import { MUTATIONS } from './mutations'
import { Login } from '@/models/auth.models'
import { AccessToken } from '@/models/auth.models'

export const ACTIONS = {
  FETCH_PRODUCTS: 'fetchProducts',
  FETCH_PRODUCT: 'fetchProduct',
  CREATE_PRODUCT: 'createProduct',
  DELETE_PRODUCT: 'deleteProduct',
  UPDATE_PRODUCT: 'updateProduct',
  LOGIN: 'login',
  LOGOUT: 'logout',
  REFRESH_TOKEN: 'refreshToken',
  REGISTER: 'register',
}

const actions: ActionTree<State, State> = {
  async [ACTIONS.FETCH_PRODUCTS]({ commit, state }, queries: ProductQueries | undefined) {
    let products: Product[]

    if (queries) {
      products = await this.$api.product.fetchMany(queries)
    } else {
      products = await this.$api.product.fetchMany({ ...state })
    }

    if (products && products.length > 0) {
      commit(MUTATIONS.SET_PRODUCTS, [...state.products, ...products])
    }
  },

  async [ACTIONS.DELETE_PRODUCT]({ commit, state }, id: string) {
    await this.$api.product.delete(id)
    commit(
      MUTATIONS.SET_PRODUCTS,
      state.products.filter((p) => p.id !== id)
    )
  },

  async [ACTIONS.LOGIN]({ commit }, payload: Login) {
    const data: AccessToken = await this.$api.auth.login(payload)
    localStorage.setItem('accessToken', data.accessToken)
    localStorage.setItem('refreshToken', data.refreshToken)
    commit(MUTATIONS.SET_IS_AUTHENTICATED, true)
    this.$router.push('/')
  },

  async [ACTIONS.LOGOUT]({ commit }) {
    localStorage.removeItem('accessToken')
    localStorage.removeItem('refreshToken')
    commit(MUTATIONS.SET_IS_AUTHENTICATED, false)
    this.$router.replace('/')
  },
}

export default actions
