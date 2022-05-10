import { MutationTree } from 'vuex/types'
import { BillState, BILL_NAMESPACED } from '@store/bill'
import { BillOrder, BillCreation } from '@models'

export interface BillMutation {
  MUTATE_HISTORY_BILLS: string
  MUTATE_LOCAL_BILL: string
}

export const BILL_MUTATIONS: { [k: string]: string } & BillMutation = {
  MUTATE_HISTORY_BILLS: 'MUTATE_HISTORY_BILLS',
  MUTATE_LOCAL_BILL: 'MUTATE_LOCAL_BILL',
}

const billMutations: MutationTree<BillState> = {
  [BILL_MUTATIONS.MUTATE_HISTORY_BILLS](state, historyBills: BillOrder[]): void {
    state.historyBills = historyBills
  },

  [BILL_MUTATIONS.MUTATE_LOCAL_BILL](state, localBill: BillCreation): void {
    state.localBill = localBill
  },
}

Object.keys(BILL_MUTATIONS).forEach((key) => (BILL_MUTATIONS[key] = `${BILL_NAMESPACED}/${BILL_MUTATIONS[key]}`))

export default billMutations
