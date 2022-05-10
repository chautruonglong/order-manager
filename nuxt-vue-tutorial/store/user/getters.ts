import { GetterTree } from 'vuex/types'
import { UserState, USER_NAMESPACED } from '@store/user'
import { User } from '@models'

export interface UserGetter {
  GET_USER: string
}

export const USER_GETTERS: { [k: string]: string } & UserGetter = {
  GET_USER: 'GET_USER',
}

const userGetters: GetterTree<UserState, UserState> = {
  [USER_GETTERS.GET_USER](state): User | null {
    return state.user
  },
}

Object.keys(USER_GETTERS).forEach((key) => (USER_GETTERS[key] = `${USER_NAMESPACED}/${USER_GETTERS[key]}`))

export default userGetters
