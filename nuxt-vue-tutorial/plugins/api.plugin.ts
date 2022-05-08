import { AuthService, BillService, ProductService, UserService } from '~/services'
import { Plugin } from '@nuxt/types'

export interface Services {
  product: ProductService
  auth: AuthService
  bill: BillService
  user: UserService
}

const apiPlugin: Plugin = ({ $axios }, inject) => {
  const axios = $axios.create()

  axios.interceptors.request.use((req) => {
    const token = localStorage.getItem('accessToken')
    if (token) {
      req.headers = {
        ...req.headers,
        Authorization: `Bearer ${token}`,
      }
    }
    return req
  })

  const services: Services = {
    product: new ProductService(axios),
    auth: new AuthService(axios),
    bill: new BillService(axios),
    user: new UserService(axios),
  }

  inject('api', services)
}

export default apiPlugin
