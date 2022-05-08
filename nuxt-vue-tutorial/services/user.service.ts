import { UserModification } from './../models/user.models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'
import { User } from '~/models/user.models'

export default class UserServices {
  constructor(private readonly $axios: NuxtAxiosInstance) {}

  public async getUser(id: string): Promise<User> {
    return await this.$axios.$get(`users/${id}`)
  }

  public async updateUser(id: string, user: UserModification): Promise<User> {
    return await this.$axios.$put(`users/${id}`, user)
  }

  public async deleteUser(id: string): Promise<any> {
    return await this.$axios.$delete(`users/${id}`)
  }
}
