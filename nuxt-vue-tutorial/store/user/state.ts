import { User } from '@models'

export const USER_NAMESPACED = 'user'

export interface UserState {
  user: User | null
}

const userState: UserState = {
  user: null,
}

export default userState
