import { ActionTree } from 'vuex/types'
import { UserState, USER_NAMESPACED, USER_MUTATIONS } from '@store/user'
import { User } from '@models'
import { getUserIdFromToken } from '@utils'

export interface UserAction {
  FETCH_USER: string
}

export const USER_ACTIONS: { [k: string]: string } & UserAction = {
  FETCH_USER: 'FETCH_USER',
}

const userActions: ActionTree<UserState, UserState> = {
  async [USER_ACTIONS.FETCH_USER]({ commit }, id?: string): Promise<User | undefined> {
    if (!id) {
      id = getUserIdFromToken()
    }
    if (id) {
      const user = await this.$api.user.getUser(id)
      commit(USER_MUTATIONS.MUTATE_USER, user, { root: true })
      return user
    }
  },
}

Object.keys(USER_ACTIONS).forEach((key) => (USER_ACTIONS[key] = `${USER_NAMESPACED}/${USER_ACTIONS[key]}`))

export default userActions
