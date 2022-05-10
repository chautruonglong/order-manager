import { Login, AccessToken, UserRegister, User } from '@models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'

export class AuthService {
  constructor(private readonly axios: NuxtAxiosInstance) {}

  public async login(payload: Login): Promise<AccessToken> {
    return await this.axios.$post('/api/auth/login', payload)
  }

  public async refresh(): Promise<AccessToken> {
    return await this.axios.$get('/api/auth/refresh')
  }

  public async register(payload: UserRegister): Promise<User> {
    return await this.axios.$post('/api/auth/register', payload)
  }
}
