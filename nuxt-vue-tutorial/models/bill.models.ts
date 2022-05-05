import { OrderCreation, OrderProduct } from './order.models'

export interface BillCreation {
  total: number
  orders: OrderCreation[]
}

export interface BillOrder {
  id: string
  total: number
  orders: OrderProduct[]
  createdAt: Date
  modifiedAt: Date
}
