import { ActionTree } from 'vuex'
import { Product } from '@models'
import { ProductQueries } from '@services'
import { PRODUCT_NAMESPACED, ProductState, PRODUCT_MUTATIONS } from '@store/product'

export interface ProductAction {
  FETCH_PRODUCTS: string
  FETCH_PRODUCT: string
  DELETE_PRODUCT: string
  UPDATE_PRODUCT: string
  CREATE_PRODUCT: string
  REORDER_PRODUCTS: string
}

export const PRODUCT_ACTIONS: { [key: string]: string } & ProductAction = {
  FETCH_PRODUCTS: 'FETCH_PRODUCTS',
  FETCH_PRODUCT: 'FETCH_PRODUCT',
  DELETE_PRODUCT: 'DELETE_PRODUCT',
  UPDATE_PRODUCT: 'UPDATE_PRODUCT',
  CREATE_PRODUCT: 'CREATE_PRODUCT',
  REORDER_PRODUCTS: 'REORDER_PRODUCTS',
}

const productActions: ActionTree<ProductState, ProductState> = {
  async [PRODUCT_ACTIONS.FETCH_PRODUCTS]({ commit, state }, queries: ProductQueries = { ...state }): Promise<Product[]> {
    const products = await this.$api.product.fetchMany(queries)
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCTS, [...state.products, ...products], { root: true })
    return products
  },

  async [PRODUCT_ACTIONS.FETCH_PRODUCT]({ commit }, id: string): Promise<Product> {
    const product = await this.$api.product.fetchOne(id)
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCT, product, { root: true })
    return product
  },

  async [PRODUCT_ACTIONS.DELETE_PRODUCT]({ commit, state }, id: string): Promise<Product> {
    const product = await this.$api.product.delete(id)
    const products = state.products.filter((p) => p.id !== id)
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCTS, products, { root: true })
    return product
  },

  async [PRODUCT_ACTIONS.UPDATE_PRODUCT]({ commit }, { id, product }: { id: string; product: FormData }): Promise<Product> {
    const $product = await this.$api.product.update({ id, product })
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCT, $product, { root: true })
    return $product
  },

  async [PRODUCT_ACTIONS.CREATE_PRODUCT]({ commit, state }, product: FormData): Promise<Product> {
    const $product = await this.$api.product.create(product)
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCTS, [...state.products, $product], { root: true })
    return $product
  },

  [PRODUCT_ACTIONS.REORDER_PRODUCTS]({ commit, state }): Product[] {
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

    const orderedProducts = [...state.products].sort(cmp)
    commit(PRODUCT_MUTATIONS.MUTATE_PRODUCTS, orderedProducts, { root: true })
    return orderedProducts
  },
}

Object.keys(PRODUCT_ACTIONS).forEach((key) => (PRODUCT_ACTIONS[key] = `${PRODUCT_NAMESPACED}/${PRODUCT_ACTIONS[key]}`))

export default productActions
