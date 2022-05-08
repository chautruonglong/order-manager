import { NuxtAxiosInstance } from '@nuxtjs/axios'
import { BillCreation, BillOrder } from '@/models/bill.models'

export interface BillQueries {
  page: number
  size: number
}

export default class BillService {
  constructor(private readonly $axios: NuxtAxiosInstance) {}

  public async fetchMany({ page, size }: BillQueries): Promise<BillOrder> {
    let queries = `${Number.isInteger(page) ? `page=${page}&` : ''}` + `${Number.isInteger(size) ? `size=${size}&` : ''}`

    if (queries.endsWith('&')) {
      queries = queries.slice(0, queries.length - 1)
    }

    return await this.$axios.$get(`bills?${queries}`)
  }

  public async create(payload: BillCreation): Promise<any> {
    return await this.$axios.$post('bills', payload)
  }
}
