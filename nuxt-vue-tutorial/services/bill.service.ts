import { BillOrder, BillCreation } from '@models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'

export interface BillQueries {
  page: number
  size: number
  sort?: string
}

export class BillService {
  constructor(private readonly axios: NuxtAxiosInstance) {}

  public async fetchMany({ page, size, sort }: BillQueries): Promise<BillOrder[]> {
    let queries =
      `${Number.isInteger(page) ? `page=${page}&` : ''}` +
      `${Number.isInteger(size) ? `size=${size}&` : ''}` +
      `${sort ? `sort=${sort}` : ''}`

    if (queries.endsWith('&')) {
      queries = queries.slice(0, queries.length - 1)
    }

    return await this.axios.$get(`/api/bills?${queries}`)
  }

  public async create(bill: BillCreation): Promise<any> {
    return await this.axios.$post('/api/bills', bill)
  }
}
