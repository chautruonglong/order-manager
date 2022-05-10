import { GetterTree } from 'vuex/types'
import { BillState, BILL_NAMESPACED } from '@store/bill'
import { BillOrder, BillCreation } from '@models'

export interface BillGetter {
  GET_HISTORY_BILLS: string
  GET_LOCAL_BILL: string
}

export const BILL_GETTERS: { [k: string]: string } & BillGetter = {
  GET_HISTORY_BILLS: 'GET_HISTORY_BILLS',
  GET_LOCAL_BILL: 'GET_LOCAL_BILL',
}

const billGetters: GetterTree<BillState, BillState> = {
  [BILL_GETTERS.GET_HISTORY_BILLS](state): BillOrder[] {
    return state.historyBills
  },

  [BILL_GETTERS.GET_LOCAL_BILL](state): BillCreation | null {
    return state.localBill
  },
}

Object.keys(BILL_GETTERS).forEach((key) => (BILL_GETTERS[key] = `${BILL_NAMESPACED}/${BILL_GETTERS[key]}`))

export default billGetters
