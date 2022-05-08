import { State } from './state'
import { MutationTree } from 'vuex/types'

export const MUTATIONS = {
  SET_IS_AUTHENTICATED: 'setIsAuthenticated',
  SET_PRODUCTS: 'setProducts',
  SET_BILLS: 'setBills',
  SET_PAGE: 'setPage',
  SET_SIZE: 'setSize',
  SET_QUESTION: 'setQuestion',
  SET_SORT: 'setSort',
  SHOW_PRODUCT_MODAL: 'showProductModal',
  SHOW_EDIT_MODAL: 'showEditModal',
  SET_LIST_CART: 'setListCart',
}

const mutations: MutationTree<State> = {
  [MUTATIONS.SET_IS_AUTHENTICATED](state, b) {
    state.isAuthenticated = b
  },
  [MUTATIONS.SET_PRODUCTS](state, products) {
    state.products = products
  },
  [MUTATIONS.SET_BILLS](state, bills) {
    state.bills = bills
  },
  [MUTATIONS.SET_PAGE](state, page) {
    state.page = page
  },
  [MUTATIONS.SET_SIZE](state, size) {
    state.size = size
  },
  [MUTATIONS.SET_QUESTION](state, question) {
    state.question = question
  },
  [MUTATIONS.SET_SORT](state, sort) {
    state.sort = sort
  },
  [MUTATIONS.SHOW_PRODUCT_MODAL](state, b) {
    state.isProductModal = b
  },
  [MUTATIONS.SHOW_EDIT_MODAL](state, b) {
    state.isEditModal = b
  },
  [MUTATIONS.SET_LIST_CART](state, listCart) {
    state.listCart = listCart
  },
}

export default mutations
