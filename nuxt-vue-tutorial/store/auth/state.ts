export const AUTH_NAMESPACED = 'auth'

export interface AuthState {
  isAuthenticated: boolean
}

const authState: AuthState = {
  isAuthenticated: false,
}

export default () => authState
