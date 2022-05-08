import { ActionTree } from 'vuex/types'
import { State } from './state'
import { ProductQueries } from '~/services/product.service'
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
  REORDER_PRODUCTS: 'reorderProducts',
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

  async [ACTIONS.CREATE_PRODUCT]({ commit, state }, { payload }: { payload: FormData }) {
    const product = await this.$api.product.create(payload)
    commit(MUTATIONS.SET_PRODUCTS, [...state.products].push(product))
  },

  async [ACTIONS.DELETE_PRODUCT]({ commit, state }, id: string) {
    await this.$api.product.delete(id)
    commit(
      MUTATIONS.SET_PRODUCTS,
      state.products.filter((p) => p.id !== id)
    )
  },

  async [ACTIONS.UPDATE_PRODUCT]({ commit, state }, { id, payload }: { id: string; payload: FormData }) {
    const product = await this.$api.product.update({ id, payload })
    commit(MUTATIONS.SET_PRODUCTS, state.products.filter((p) => p.id != id).push(product))
    this.dispatch(ACTIONS.REORDER_PRODUCTS)
  },

  async [ACTIONS.REORDER_PRODUCTS]({ commit, state }) {
    const tokens = state.sort.split('_')
    let cmp
    switch (tokens[0]) {
      case 'created':
        if (tokens.pop() === 'asc') {
          cmp = (a: Product, b: Product) => a.createdAt.valueOf() - b.createdAt.valueOf()
        } else {
          cmp = (a: Product, b: Product) => b.createdAt.valueOf() - a.createdAt.valueOf()
        }
        break
      case 'modified':
        if (tokens.pop() === 'asc') {
          cmp = (a: Product, b: Product) => a.modifiedAt.valueOf() - b.modifiedAt.valueOf()
        } else {
          cmp = (a: Product, b: Product) => b.modifiedAt.valueOf() - a.modifiedAt.valueOf()
        }
        break
      case 'name':
        if (tokens.pop() === 'asc') {
          cmp = (a: Product, b: Product) => a.name.localeCompare(b.name)
        } else {
          cmp = (a: Product, b: Product) => b.name.localeCompare(a.name)
        }
        break
    }
    commit(MUTATIONS.SET_PRODUCTS, [...state.products].sort(cmp))
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
