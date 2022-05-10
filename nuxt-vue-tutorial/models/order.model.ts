import { Product } from '@models'

export interface Order {
  id: string
  quantity: number
  productId: string
  userId: string
  billId: string
  createdAt: Date
  modifiedAt: Date
}

export interface OrderCreation {
  quantity: number
  productId: string
}

export interface OrderProduct {
  id: string
  quantity: number
  product: Product
}
