import { Product } from '@models'

export const PRODUCT_NAMESPACED = 'product'

export interface ProductState {
  products: Product[]
  page: number
  size: number
  sort: string
  question: string
  productModal: Product | null
}

const productState: ProductState = {
  products: [],
  page: 0,
  size: 30,
  sort: 'modified_at_desc',
  question: '',
  productModal: null,
}

export default () => productState
