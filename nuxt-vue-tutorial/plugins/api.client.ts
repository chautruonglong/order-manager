import { ProductService } from '@/services/product.services'
import { AuthService } from '@/services/auth.services'
import { BillService } from '@/services/bill.services'
import { Plugin } from '@nuxt/types'

export interface Services {
  product: ProductService
  auth: AuthService
  bill: BillService
}

const apiPlugin: Plugin = ({ $axios }, inject) => {
  // $axios.onError((error) => {})

  if (process.client) {
    const token = localStorage.getItem('accessToken')

    if (token) {
      $axios.setHeader('Authorization', `Bearer ${token}`)
    }
  }

  const services: Services = {
    product: new ProductService($axios),
    auth: new AuthService($axios),
    bill: new BillService($axios),
  }

  inject('api', services)
}

export default apiPlugin
