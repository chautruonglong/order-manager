import { MutationTree } from 'vuex/types'
import { UserState, USER_NAMESPACED } from '@store/user'
import { User } from '@models'

export interface UserMutation {
  MUTATE_USER: string
}

export const USER_MUTATIONS: { [k: string]: string } & UserMutation = {
  MUTATE_USER: 'MUTATE_USER',
}

const userMutations: MutationTree<UserState> = {
  [USER_MUTATIONS.MUTATE_USER](state, user: User): void {
    state.user = user
  },
}

Object.keys(USER_MUTATIONS).forEach((key) => (USER_MUTATIONS[key] = `${USER_NAMESPACED}/${USER_MUTATIONS[key]}`))

export default userMutations
