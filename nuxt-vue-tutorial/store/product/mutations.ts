import { Product } from '@models'
import { MutationTree } from 'vuex/types'
import { ProductState, PRODUCT_NAMESPACED } from '@store/product'

export interface ProductMutation {
  MUTATE_PRODUCTS: string
  MUTATE_PRODUCT: string
  MUTATE_PAGE: string
  MUTATE_SIZE: string
  MUTATE_SORT: string
  MUTATE_QUESTION: string
  MUTATE_PRODUCT_MODAL: string
  MUTATE_ORDER_PRODUCTS: string
}

export const PRODUCT_MUTATIONS: { [key: string]: string } & ProductMutation = {
  MUTATE_PRODUCTS: 'MUTATE_PRODUCTS',
  MUTATE_PRODUCT: 'MUTATE_PRODUCT',
  MUTATE_PAGE: 'MUTATE_PAGE',
  MUTATE_SIZE: 'MUTATE_SIZE',
  MUTATE_SORT: 'MUTATE_SORT',
  MUTATE_QUESTION: 'MUTATE_QUESTION',
  MUTATE_PRODUCT_MODAL: 'MUTATE_PRODUCT_MODAL',
  MUTATE_ORDER_PRODUCTS: 'MUTATE_ORDER_PRODUCTS',
}

const productMutations: MutationTree<ProductState> = {
  [PRODUCT_MUTATIONS.MUTATE_PRODUCTS](state, products: Product[]): void {
    state.products = products
  },

  [PRODUCT_MUTATIONS.MUTATE_PRODUCT](state, product: Product): void {
    const $product = state.products.find((p) => p.id === product.id)
    if ($product) {
      Object.assign($product, product)
    }
  },

  [PRODUCT_MUTATIONS.MUTATE_PAGE](state, page: number): void {
    state.page = page
  },

  [PRODUCT_MUTATIONS.MUTATE_SIZE](state, size: number): void {
    state.size = size
  },

  [PRODUCT_MUTATIONS.MUTATE_SORT](state, sort: string): void {
    state.sort = sort
  },

  [PRODUCT_MUTATIONS.MUTATE_QUESTION](state, question: string): void {
    state.question = question
  },

  [PRODUCT_MUTATIONS.MUTATE_PRODUCT_MODAL](state, product: Product): void {
    state.productModal = product
  },

  [PRODUCT_MUTATIONS.MUTATE_ORDER_PRODUCTS](state, products: Product[]): void {
    state.orderProducts = products
  },
}

Object.keys(PRODUCT_MUTATIONS).forEach((key) => (PRODUCT_MUTATIONS[key] = `${PRODUCT_NAMESPACED}/${PRODUCT_MUTATIONS[key]}`))

export default productMutations
