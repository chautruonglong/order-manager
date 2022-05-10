import { ActionTree } from 'vuex/types'
import { BillState, BILL_NAMESPACED, BILL_MUTATIONS } from '@store/bill'
import { BillCreation, Product } from '@models'
import { cloneDeep } from 'lodash'

export interface BillAction {
  ADD_PRODUCT_TO_CART: string
  SUB_PRODUCT_FROM_CART: string
  REMOVE_PRODUCTS_FROM_CART: string
}

export const BILL_ACTIONS: { [k: string]: string } & BillAction = {
  ADD_PRODUCT_TO_CART: 'ADD_PRODUCT_TO_CART',
  SUB_PRODUCT_FROM_CART: 'SUB_PRODUCT_FROM_CART',
  REMOVE_PRODUCTS_FROM_CART: 'REMOVE_PRODUCTS_FROM_CART',
}

const billActions: ActionTree<BillState, BillState> = {
  async [BILL_ACTIONS.ADD_PRODUCT_TO_CART]({ commit, state }, product: Product): Promise<BillCreation> {
    const localBill: BillCreation = cloneDeep(state.localBill ? state.localBill : { total: 0, orders: [] })
    localBill.total += product.price

    const order = localBill.orders.find((o) => o.product.id === product.id)
    if (order) {
      order.quantity += 1
    } else {
      localBill.orders.push({ quantity: 1, product })
    }

    commit(BILL_MUTATIONS.MUTATE_LOCAL_BILL, localBill, { root: true })
    return localBill
  },

  async [BILL_ACTIONS.SUB_PRODUCT_FROM_CART]({ commit, state }, id: string): Promise<BillCreation> {
    const localBill: BillCreation = cloneDeep(state.localBill ? state.localBill : { total: 0, orders: [] })

    const order = localBill.orders.find((o) => o.product.id === id)
    if (order) {
      localBill.total -= order.product.price
      order.quantity -= 1

      if (order.quantity < 1) {
        localBill.orders = localBill.orders.filter((o) => o !== order)
      }
    }

    commit(BILL_MUTATIONS.MUTATE_LOCAL_BILL, localBill, { root: true })
    return localBill
  },

  async [BILL_ACTIONS.REMOVE_PRODUCTS_FROM_CART]({ commit, state }, id: string): Promise<BillCreation> {
    const localBill: BillCreation = cloneDeep(state.localBill ? state.localBill : { total: 0, orders: [] })

    const order = localBill.orders.find((o) => o.product.id === id)
    if (order) {
      localBill.total -= order.product.price * order.quantity
      localBill.orders = localBill.orders.filter((o) => o !== order)
    }

    commit(BILL_MUTATIONS.MUTATE_LOCAL_BILL, localBill, { root: true })
    return localBill
  },
}

Object.keys(BILL_ACTIONS).forEach((key) => (BILL_ACTIONS[key] = `${BILL_NAMESPACED}/${BILL_ACTIONS[key]}`))

export default billActions
