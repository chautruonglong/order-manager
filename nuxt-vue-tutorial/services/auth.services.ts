import { AccessToken, Login, UserRegister } from '@/models/auth.models'
import { User } from '@/models/user.models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'

export class AuthService {
  constructor(private readonly $axios: NuxtAxiosInstance) {}

  public async login(payload: Login): Promise<AccessToken> {
    return await this.$axios.$post('auth/login', payload)
  }

  public async refresh(): Promise<AccessToken> {
    return await this.$axios.$post('auth/refresh')
  }

  public async register(payload: UserRegister): Promise<User> {
    return await this.$axios.$post('auth/register', payload)
  }
}
