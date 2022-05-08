import { NuxtAxiosInstance } from '@nuxtjs/axios'
import { Product } from '@/models/product.models'

export interface ProductQueries {
  page: number
  size: number
  question?: string
  sort?: string
}

export default class ProductService {
  constructor(private readonly $axios: NuxtAxiosInstance) {}

  public async fetchMany({ page, size, question, sort }: ProductQueries): Promise<Product[]> {
    let queries =
      `${Number.isInteger(page) ? `page=${page}&` : ''}` +
      `${Number.isInteger(size) ? `size=${size}&` : ''}` +
      `${question ? `question=${question}&` : ''}` +
      `${sort ? `sort=${sort}` : ''}`

    if (queries.endsWith('&')) {
      queries = queries.slice(0, queries.length - 1)
    }

    return await this.$axios.$get(`products?${queries}`)
  }

  public async fetchOne(id: string): Promise<Product> {
    return await this.$axios.$get(`products/${id}`)
  }

  public async create(payload: FormData): Promise<Product> {
    return await this.$axios.$post('products', payload)
  }

  public async update({ id, payload }: { id: string; payload: FormData }): Promise<Product> {
    return await this.$axios.$put(`products/${id}`, payload)
  }

  public async delete(id: string): Promise<Product> {
    return await this.$axios.$delete(`products/${id}`)
  }
}
