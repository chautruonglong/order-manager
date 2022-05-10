import { BillOrder, BillCreation } from '@models'

export const BILL_NAMESPACED = 'bill'

export interface BillState {
  historyBills: BillOrder[]
  localBill: BillCreation | null
}

const billState: BillState = {
  historyBills: [],
  localBill: null,
}

export default () => billState
