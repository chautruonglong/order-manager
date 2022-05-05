import { Product } from '~/models/product.models'
import { BillOrder } from '~/models/bill.models'

export interface State {
  isAuthenticated: boolean
  products: Product[]
  bills: BillOrder[]
  page: number
  size: number
  question: string
  sort: string
  isProductModal: boolean
}

const state: State = {
  isAuthenticated: false,
  products: [],
  bills: [],
  page: 0,
  size: 40,
  question: '',
  sort: 'modifiedAtDesc',
  isProductModal: false,
}

export default () => state
