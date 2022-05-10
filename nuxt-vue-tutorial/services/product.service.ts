import { Product } from '@models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'

export interface ProductQueries {
  page: number
  size: number
  question?: string
  sort?: string
}

export class ProductService {
  constructor(private readonly axios: NuxtAxiosInstance) {}

  public async fetchMany({ page, size, question, sort }: ProductQueries): Promise<Product[]> {
    let queries =
      `${sort ? `sort=${sort}&` : ''}` +
      `${question ? `question=${question}&` : ''}` +
      `${Number.isInteger(page) ? `page=${page}&` : ''}` +
      `${Number.isInteger(size) ? `size=${size}` : ''}`

    if (queries.endsWith('&')) {
      queries = queries.slice(0, queries.length - 1)
    }

    return await this.axios.$get(`/api/products?${queries}`)
  }

  public async fetchOne(id: string): Promise<Product> {
    return await this.axios.$get(`/api/products/${id}`)
  }

  public async delete(id: string): Promise<Product> {
    return await this.axios.$delete(`/api/products/${id}`)
  }

  public async create(product: FormData): Promise<Product> {
    return await this.axios.$post('/api/products', product)
  }

  public async update({ id, product }: { id: string; product: FormData }): Promise<Product> {
    return await this.axios.$put(`/api/products/${id}`, product)
  }
}
