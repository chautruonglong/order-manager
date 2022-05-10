import { AppService, AuthService, BillService, ProductService, UserService } from '@services'
import { Plugin } from '@nuxt/types'
import { getAccessToken } from '@utils'

const apiPlugin: Plugin = ({ $axios }, inject) => {
  const axios = $axios.create()

  axios.interceptors.request.use((req) => {
    if (process.client) {
      const token = getAccessToken()
      if (token) {
        req.headers = {
          ...req.headers,
          Authorization: `Bearer ${token}`,
        }
      }
    }
    return req
  })

  const services: AppService = {
    auth: new AuthService(axios),
    bill: new BillService(axios),
    user: new UserService(axios),
    product: new ProductService(axios),
  }

  inject('api', services)
}

export default apiPlugin
