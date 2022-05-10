import { GetterTree } from 'vuex/types'
import { CommonState, COMMON_NAMESPACED, Snackbar } from '@store/common'

export interface CommonGetter {
  GET_IS_LOADING: string
  GET_IS_SHOW_PRODUCT_CREATION: string
  GET_IS_SHOW_PRODUCT_MODIFICATION: string
  GET_IS_SHOW_PRODUCT_MODAL: string
  GET_IS_SHOW_CART: string
  GET_SNACKBAR: string
}

export const COMMON_GETTERS: { [k: string]: string } & CommonGetter = {
  GET_IS_LOADING: 'GET_IS_LOADING',
  GET_IS_SHOW_PRODUCT_CREATION: 'GET_IS_SHOW_PRODUCT_CREATION',
  GET_IS_SHOW_PRODUCT_MODIFICATION: 'GET_IS_SHOW_PRODUCT_MODIFICATION',
  GET_IS_SHOW_PRODUCT_MODAL: 'GET_IS_SHOW_PRODUCT_MODAL',
  GET_IS_SHOW_CART: 'GET_IS_SHOW_CART',
  GET_SNACKBAR: 'GET_SNACKBAR',
}

const commonGetters: GetterTree<CommonState, CommonState> = {
  [COMMON_GETTERS.GET_IS_LOADING](state): boolean {
    return state.isLoading
  },

  [COMMON_GETTERS.GET_IS_SHOW_PRODUCT_CREATION](state): boolean {
    return state.isShowProductCreation
  },

  [COMMON_GETTERS.GET_IS_SHOW_PRODUCT_MODIFICATION](state): boolean {
    return state.isShowProductModification
  },

  [COMMON_GETTERS.GET_IS_SHOW_PRODUCT_MODAL](state): boolean {
    return state.isShowProductModal
  },

  [COMMON_GETTERS.GET_SNACKBAR](state): Snackbar {
    return state.snackbar
  },

  [COMMON_GETTERS.GET_IS_SHOW_CART](state): boolean {
    return state.isShowCart
  },
}

Object.keys(COMMON_GETTERS).forEach((key) => (COMMON_GETTERS[key] = `${COMMON_NAMESPACED}/${COMMON_GETTERS[key]}`))

export default commonGetters
