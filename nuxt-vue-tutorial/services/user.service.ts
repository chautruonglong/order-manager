import { User, UserModification } from '@models'
import { NuxtAxiosInstance } from '@nuxtjs/axios'

export class UserService {
  constructor(private readonly axios: NuxtAxiosInstance) {}

  public async getUser(id: string): Promise<User> {
    return await this.axios.$get(`/api/users/${id}`)
  }

  public async deleteUser(id: string): Promise<any> {
    return await this.axios.$delete(`/api/users/${id}`)
  }

  public async updateUser({ id, user }: { id: string; user: UserModification }): Promise<User> {
    return await this.axios.$put(`/api/users/${id}`, user)
  }
}
