import { NuxtAxiosInstance } from '@nuxtjs/axios'
import { Product, ProductCreation, ProductModification } from '@/models/product.models'

export interface ProductQueries {
  page: number
  size: number
  question?: string
  sort?: string
}

export class ProductService {
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

  public async create(payload: ProductCreation): Promise<Product> {
    return await this.$axios.$post('products', payload)
  }

  public async update(payload: ProductModification): Promise<Product> {
    return await this.$axios.$put('products', payload)
  }

  public async delete(id: string): Promise<Product> {
    return await this.$axios.$delete(`products/${id}`)
  }
}
