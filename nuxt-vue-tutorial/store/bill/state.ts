import { BillOrder, BillCreation } from '@models'

export const BILL_NAMESPACED = 'bill'

export interface BillState {
  historyBills: BillOrder[]
  localBill: BillCreation | null
  page: number
  size: number
}

const billState: BillState = {
  historyBills: [],
  localBill: null,
  page: 0,
  size: 30,
}

export default () => billState
