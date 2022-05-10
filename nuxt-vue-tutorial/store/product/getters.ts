import { GetterTree } from 'vuex/types'
import { ProductState, PRODUCT_NAMESPACED } from '@store/product'
import { Product } from '@models'

export interface ProductGetter {
  GET_PRODUCTS: string
  GET_PRODUCT: string
  GET_PAGE: string
  GET_SIZE: string
  GET_SORT: string
  GET_QUESTION: string
  GET_PRODUCT_MODAL: string
  GET_ORDER_PRODUCTS: string
}

export const PRODUCT_GETTERS: { [k: string]: string } & ProductGetter = {
  GET_PRODUCTS: 'GET_PRODUCTS',
  GET_PRODUCT: 'GET_PRODUCT',
  GET_PAGE: 'GET_PAGE',
  GET_SIZE: 'GET_SIZE',
  GET_SORT: 'GET_SORT',
  GET_QUESTION: 'GET_QUESTION',
  GET_PRODUCT_MODAL: 'GET_PRODUCT_MODAL',
  GET_ORDER_PRODUCTS: 'GET_ORDER_PRODUCTS',
}

const productGetters: GetterTree<ProductState, ProductState> = {
  [PRODUCT_GETTERS.GET_PRODUCTS](state): Product[] {
    return state.products
  },

  [PRODUCT_GETTERS.GET_PRODUCT](state, id: string): Product | undefined {
    return state.products.find((p) => p.id === id)
  },

  [PRODUCT_GETTERS.GET_PAGE](state): number {
    return state.page
  },

  [PRODUCT_GETTERS.GET_SIZE](state): number {
    return state.size
  },

  [PRODUCT_GETTERS.GET_SORT](state): string {
    return state.sort
  },

  [PRODUCT_GETTERS.GET_QUESTION](state): string {
    return state.question
  },

  [PRODUCT_GETTERS.GET_PRODUCT_MODAL](state): Product | null {
    return state.productModal
  },

  [PRODUCT_GETTERS.GET_ORDER_PRODUCTS](state): Product[] {
    return state.orderProducts
  },
}

Object.keys(PRODUCT_GETTERS).forEach((key) => (PRODUCT_GETTERS[key] = `${PRODUCT_NAMESPACED}/${PRODUCT_GETTERS[key]}`))

export default productGetters
