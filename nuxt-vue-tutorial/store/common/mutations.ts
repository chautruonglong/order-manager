import { MutationTree } from 'vuex/types'
import { CommonState, COMMON_NAMESPACED, Snackbar } from '@store/common'

export interface CommonMutation {
  MUTATE_IS_LOADING: string
  MUTATE_IS_SHOW_PRODUCT_CREATION: string
  MUTATE_IS_SHOW_PRODUCT_MODIFICATION: string
  MUTATE_IS_SHOW_PRODUCT_MODAL: string
  MUTATE_IS_SHOW_CART: string
  MUTATE_SNACKBAR: string
}

export const COMMON_MUTATIONS: { [k: string]: string } & CommonMutation = {
  MUTATE_IS_LOADING: 'MUTATE_IS_LOADING',
  MUTATE_IS_SHOW_PRODUCT_CREATION: 'MUTATE_IS_SHOW_PRODUCT_CREATION',
  MUTATE_IS_SHOW_PRODUCT_MODIFICATION: 'MUTATE_IS_SHOW_PRODUCT_MODIFICATION',
  MUTATE_IS_SHOW_PRODUCT_MODAL: 'MUTATE_IS_SHOW_PRODUCT_MODAL',
  MUTATE_IS_SHOW_CART: 'MUTATE_IS_SHOW_CART',
  MUTATE_SNACKBAR: 'MUTATE_SNACKBAR',
}

const commonMutations: MutationTree<CommonState> = {
  [COMMON_MUTATIONS.MUTATE_IS_LOADING](state, isLoading: boolean): void {
    state.isLoading = isLoading
  },

  [COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_CREATION](state, isShowProductCreation: boolean): void {
    state.isShowProductCreation = isShowProductCreation
  },

  [COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_MODIFICATION](state, isShowProductModification: boolean): void {
    state.isShowProductModification = isShowProductModification
  },

  [COMMON_MUTATIONS.MUTATE_IS_SHOW_PRODUCT_MODAL](state, isShowProductModal: boolean): void {
    state.isShowProductModal = isShowProductModal
  },

  [COMMON_MUTATIONS.MUTATE_SNACKBAR](state, message: string): void {
    state.snackbar = { active: true, timeout: 3000, message }
  },

  [COMMON_MUTATIONS.MUTATE_IS_SHOW_CART](state, isShowCart: boolean): void {
    state.isShowCart = isShowCart
  },
}

Object.keys(COMMON_MUTATIONS).forEach((key) => (COMMON_MUTATIONS[key] = `${COMMON_NAMESPACED}/${COMMON_MUTATIONS[key]}`))

export default commonMutations
