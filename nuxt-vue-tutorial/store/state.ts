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
  isEditModal: boolean
  listCart: Product[]
}

const state: State = {
  isAuthenticated: false,
  products: [],
  bills: [],
  page: 0,
  size: 40,
  question: '',
  sort: 'modified_at_desc',
  isProductModal: false,
  isEditModal: false,
  listCart: [],
}

export default () => state
